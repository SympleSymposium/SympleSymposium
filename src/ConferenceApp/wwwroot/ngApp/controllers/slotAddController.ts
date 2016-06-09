namespace ConferenceApp.Controllers {

    export class SlotAddController {
        public newSlot;
        public speakers;
        public presentations;
        public rooms;
        public addView = true;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService) {
            console.log('add');
            accountService.toolbarTitle = "Add Slot";

            $http.get('/api/speakers/manage/' + $stateParams['id'])
                .then((response) => {
                    this.speakers = response.data;
                    console.log(this.speakers);
                })

            $http.get('/api/presentations/manage/' + $stateParams['id'])
                .then((response) => {
                    this.presentations = response.data;
                })

            $http.get('/api/rooms/manage/' + $stateParams['id'])
                .then((response) => {
                    this.rooms = response.data;
                })
        }

        public AddSlot() {

            console.log(this.newSlot);
            console.log(moment(this.newSlot.startTime));
            console.log(moment(this.newSlot.startTime).utc);

            this.newSlot.conferenceId = parseInt(this.$stateParams['id']);

            this.$http.post('/api/slots', this.newSlot)
                .then((response) => {
                    this.$state.go("schedule", { id: this.newSlot.conferenceId });
                })
        }

    }

}