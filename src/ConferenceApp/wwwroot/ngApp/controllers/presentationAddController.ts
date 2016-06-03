namespace ConferenceApp.Controllers {
    export class PresentationAddController {
        public presentation;
        public addView = true;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the presentationAdd constructor');
        }

        public AddPresentation() {
            console.log(this.presentation);
            this.presentation.conferenceId = parseInt(this.$stateParams['id']);
            console.log(this.presentation);
            this.$http.post('/api/presentations', this.presentation)
                .then((response) => {
                    console.log("middle of post in AddPresentation ");
                    this.$state.go("conferenceManage");
                });
            console.log("End of Add Presentation in controller.ts");
        }

    }
}