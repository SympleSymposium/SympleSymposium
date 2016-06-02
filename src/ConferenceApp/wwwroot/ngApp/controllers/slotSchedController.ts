namespace ConferenceApp.Controllers {

    export class SlotSchedController {
        public slots;
        public rooms;
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

        constructor(private $http: ng.IHttpService, $stateParams: ng.ui.IStateParamsService, private $state: ng.ui.IStateService) {

            $http.get('/api/slots/' + $stateParams['id'])
                .then((response) => {
                    this.slots = response.data;

                    //Calculates the layout for the schedule
                    this.slots.forEach((item) => {
                        let startMinute = moment(item.startTime).hour() * 60 + moment(item.startTime).minute();
                        let endMinute = moment(item.endTime).hour() * 60 + moment(item.endTime).minute();
                        item.top = (startMinute / 60 - 8) / 10 * 100;
                        item.height = ((endMinute - startMinute) / 60 / 10) * 100;

                        //used to filter slots by day
                        item.day = moment(item.startTime).format("M/D/YYYY");
                    });
                    console.log(this.slots);

                })
                .catch((response) => {
                    console.log(response.data);
                });

            $http.get('/api/rooms/' + $stateParams['id'])
                .then((response) => {
                    this.rooms = response.data;
                    //console.log(this.rooms);
                })
                .catch((response) => {
                    console.log(response.data);
                });

            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    this.conference = response.data;
                    this.firstDay = moment(this.conference.startDate);
                    this.lastDay = moment(this.conference.endDate);

                    let d = this.firstDay.clone();
                    let i = 0;
                    while (d <= this.lastDay) {
                        this.conferenceDays[i] = d.format("M/D/YYYY");
                        d.add(1, 'days');
                        i++;
                    }
                    //console.log(this.conferenceDays);
                    this.currentDay = this.conferenceDays[0];
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }
    }
}