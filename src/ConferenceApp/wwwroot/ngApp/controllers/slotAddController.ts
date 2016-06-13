namespace ConferenceApp.Controllers {

    export class SlotAddController {
        public slot;
        public newSlot;
        public speakers;
        public presentations;
        public rooms;
        public dayDisabled = false;
        public day;
        public conferenceId;
        public title = "Add Slot"
        public icon = "add_circle";
        public theme = "md-accent";
        public showDelete = false; 

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService,
            private dayService: ConferenceApp.Services.DayService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Add Slot";

            //Hide BackButton
            toolbarService.hideBackButton = true;

            this.conferenceId = $stateParams['id'];
            console.log("ConferenceId: " + this.conferenceId);

            this.day = dayService.slotDay;

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

        public cancel() {
            this.$state.go('schedule', {
                id: this.conferenceId
            });
        }

        public UpdateSlot() {

            //console.log("day: " + this.day);
            //console.log("startTime: " + moment(this.slot.startTime).format("hh:mm:ss A"));
            //console.log("endTime: " + moment(this.slot.endTime).format("hh:mm:ss A"));

            this.newSlot = {
                presentationId: this.slot.presentation.id,
                speakerId: this.slot.speaker.id,
                roomId: this.slot.room.id,
                startTime: this.day + " " + moment(this.slot.startTime).format("hh:mm:ss A"),
                endTime: this.day + " " + moment(this.slot.endTime).format("hh:mm:ss A")
            };
            //console.log(this.slot);
            console.log("New Slot: ");
            console.log(this.newSlot);

            //this.slot.conferenceId = parseInt(this.$stateParams['id']);

            this.$http.post('/api/slots', this.newSlot)
                .then((response) => {
                    this.$state.go("schedule", { id: this.conferenceId });
                })
        }

    }

}