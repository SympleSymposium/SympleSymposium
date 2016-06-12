namespace ConferenceApp.Controllers {
    export class PresentationAddController {
        public presentation;
        public title = "Add Presentation"
        public icon = "add_circle";
        public theme = "md-accent";
        public showDelete = false;//to hide edit when add is true


        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the presentationAdd constructor');
        }

        public cancel() {
            this.$state.go('displayPresentations', { id: this.presentation.conferenceId });
        }

        public UpdatePresentation() {
            //console.log(this.presentation);
            if (!this.presentation.imageUrl) {
                this.presentation.imageUrl = "ngApp/images/Pod.png";
            }
            this.presentation.conferenceId = parseInt(this.$stateParams['id']);
            console.log(this.presentation);
            this.$http.post('/api/presentations', this.presentation)
                .then((response) => {
                    console.log("middle of post in AddPresentation ");
                    this.$state.go("displayPresentations", { id: this.presentation.conferenceId });
                });
            console.log("End of Add Presentation in controller.ts");
        }

    }
}