namespace ConferenceApp.Controllers {

    export class PresentationDisplayController {
        public presentation;

        private GetPresentations() {
            this.$http.get(`/api/presentations/manage/${this.$stateParams['id']}`)
                //$http.get(`/api/rooms/${this.rooms.conferenceId}`)
                .then((response) => {
                    this.presentation = response.data;
                    console.log('we are in the roomdisplay constructor');
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService) {
            //console.log($stateParams['id']);
            
            accountService.toolbarTitle = "Manage Presentations";

            this.GetPresentations();
        }

        public DeletePresentation(id) {
            console.log(id);
            this.$http.delete(`/api/presentations/${id}`)
                .then((response) => {
                    //this.$state.go("displayPresentations", { id: this.presentation.id });
                    console.log(response.data);
                    this.GetPresentations();
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }


    }
}