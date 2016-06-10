namespace ConferenceApp.Controllers {

    export class SlotAddController {
        public slot;
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
            console.log(`StateParams Id = $stateParams['id']`);
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
            this.newSlot = {
                presentationId: this.slot.presentation.id,
                speakerId: this.slot.speaker.id,
                roomId: this.slot.room.id,
                startTime: this.slot.startTime,
                endTime: this.slot.endTime
            };
            console.log(this.slot);
            console.log(this.newSlot);

            this.slot.conferenceId = parseInt(this.$stateParams['id']);

            this.$http.post('/api/slots', this.newSlot)
                .then((response) => {
                    this.$state.go("schedule", { id: this.slot.conferenceId });
                })
        }

    }

}