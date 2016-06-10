namespace ConferenceApp.Controllers {

    export class ScheduleController {
        public conference;
        public firstDay;
        public lastDay;
        public conferenceDays = [];
        public currentDay;
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

        //change currently viewed day
        public moveDay(moveNum: number) {
            this.currentDay = moment(new Date(this.currentDay)).add(moveNum, 'day').format("M/D/YYYY");
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
            private accountService: ConferenceApp.Services.AccountService) {
            accountService.toolbarTitle = "Presentation Schedule";

            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    console.log(response.data);
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

                    //Sets the initial day shown in the schedule to the first day of the conference
                    this.currentDay = this.conferenceDays[0];

                    //Calculates the layout for the schedule
                    this.conference.rooms.forEach((room) => {
                        room.slots.forEach((slot) => {
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