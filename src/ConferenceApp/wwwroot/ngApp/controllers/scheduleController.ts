﻿namespace ConferenceApp.Controllers {

    export class ScheduleController {
        public conference;
        public themeAdd = "md-accent";
        public themeEdit = "md-primary";
        public firstDay;
        public lastDay;
        public conferenceDays = [];
        public currentDay;
        public selectedIndex;
        public timeMarkers =
        ["8:00 AM",
            "9:00 AM",
            "10:00 AM",
            "11:00 AM",
            "12:00 PM",
            "1:00 PM",
            "2:00 PM",
            "3:00 PM",
            "4:00 PM",
            "5:00 PM",];

        public displayIndex() {
            console.log(this.selectedIndex);
            this.currentDay = this.conferenceDays[this.selectedIndex];
            //Save current day to service so that slotAddController will be able to access it
            this.dayService.slotDay = this.conferenceDays[this.selectedIndex];
        }

        //change currently viewed day
        public moveDay(moveNum: number) {
            this.currentDay = moment(new Date(this.currentDay)).add(moveNum, 'day').format("M/D/YYYY");

            //Save current day to service so that slotAddController will be able to access it
            this.dayService.slotDay = this.currentDay;
        }

        //logic for disabling back button
        public backDisabled() {
            let currentDay = this.currentDay;
            let compareDay = moment(this.firstDay).format("M/D/YYYY");

            if (currentDay == compareDay) {
                return true;
            } else {
                return false;
            }
        }

        //logic for disabling forward button
        public forwardDisabled() {
            let currentDay = this.currentDay;
            let compareDay = moment(this.lastDay).format("M/D/YYYY");

            if (currentDay == compareDay) {
                return true;
            } else {
                return false;
            }
        }

        

        constructor(private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $state: ng.ui.IStateService,
            public $window: ng.IWindowService,
            private accountService: ConferenceApp.Services.AccountService,
            private dayService: ConferenceApp.Services.DayService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Presentation Schedule";

            //Hide BackButton
            toolbarService.hideBackButton = false;


            toolbarService.goBack = () => {
                console.log("tried");
                this.$state.go("conferenceManage");
            };
            
            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    //console.log(response.data);
                    this.conference = response.data;


                    //Creates list of days in the conference
                    this.firstDay = moment(this.conference.startDate);
                    this.lastDay = moment(this.conference.endDate);

                    let d = this.firstDay.clone();
                    let i = 0;
                    while (d <= this.lastDay) {
                        this.conferenceDays[i] = d.format("M/D/YYYY");
                        d.add(1, 'days');
                        i++;
                    }

                    if (dayService.slotDay) {
                        //If currentDay is already set, jump to this day
                        this.selectedIndex = this.conferenceDays.indexOf(dayService.slotDay);
                        if (this.selectedIndex == -1) {
                            this.selectedIndex = 0;
                        }

                    } else {

                        //Sets the initial day shown in the schedule to the first day of the conference
                        this.currentDay = this.conferenceDays[0];

                        //Save current day to service so that slotAddController will be able to access it
                        dayService.slotDay = this.currentDay;
                        this.selectedIndex = this.conferenceDays.indexOf(dayService.slotDay);
                    }

                    

                    //Calculates the layout for the schedule
                    this.conference.rooms.forEach((room) => {
                        room.slots.forEach((slot) => {
                            //console.log(moment.utc(slot.startTime).format());
                            let startMinute = moment(slot.startTime).hour() * 60 + moment(slot.startTime).minute();
                            let endMinute = moment(slot.endTime).hour() * 60 + moment(slot.endTime).minute();
                            slot.top = (startMinute / 60 - 8) / 10 * 100;
                            slot.height = ((endMinute - startMinute) / 60 / 10) * 100;

                            //used to filter slots by day
                            slot.day = moment(slot.startTime).format("M/D/YYYY");

                            //Format start and endtime for display
                            slot.startTimeDisplay = moment(slot.startTime).format("h:mm A");
                            slot.endTimeDisplay = moment(slot.endTime).format("h:mm A");

                        })
                    });



                    //console.log(this.conference);
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }
    }
}