namespace ConferenceApp.Controllers {

    export class PresentationDisplayController {
        public presentation;
        public themeAdd = "accent";
        public themeEdit = "primary";
        public conferenceId;

        private UpdatePresentations() {
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
            private accountService: ConferenceApp.Services.AccountService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {
            //console.log($stateParams['id']);

            accountService.toolbarTitle = "Manage Presentations";
            this.conferenceId = $stateParams['id'];

            toolbarService.goBack = () => {
                console.log("tried");
                console.log("Confererence Id: " + this.presentation.conferenceId);
                console.log("Confererence Id: " + this.presentation.id);
                //this.$state.go("conferenceManage");
                this.$state.go("schedule", { id: this.conferenceId })
            };

            this.UpdatePresentations();
        }

        public DeletePresentation(id) {
            console.log(id);

            this.$http.delete(`/api/presentations/${id}`)
                .then((response) => {
                    //this.$state.go("displayPresentations", { id: this.presentation.id });
                    console.log(response.data);
                    this.UpdatePresentations();
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

    }


}
