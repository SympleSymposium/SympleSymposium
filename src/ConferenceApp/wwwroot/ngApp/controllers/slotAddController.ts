namespace ConferenceApp.Controllers {

    export class SlotAddController {
        public newSlot;
        public conference;
        public addView = true;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService) {

            accountService.toolbarTitle = "Presentation Schedule";

            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    this.conference = response.data;
                    console.log(this.conference);
                })
        }

        public AddSlot() {

            this.newSlot.conferenceId = parseInt(this.$stateParams['id']);

            this.$http.post('/api/slots', this.newSlot)
                .then((response) => {
                    this.$state.go("schedule", { id: this.newSlot.conferenceId });
                })
        }

    }

}