namespace ConferenceApp.Controllers {

    export class SpeakerDisplayController {
        public speakers;
        public themeAdd = "accent";
        public themeEdit = "primary";
        public conferenceId;

        private GetSpeakers() {
            console.log("In GetSpeakers method");
            this.$http.get(`/api/speakers/manage/${this.$stateParams['id']}`)
                .then((response) => {
                    this.speakers = response.data;
                    console.log(this.speakers)
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        constructor(private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Manage Speakers";
            this.conferenceId = $stateParams['id'];

            toolbarService.goBack = () => {
                console.log("tried");
                //this.$state.go("conferenceManage");
                this.$state.go("schedule", { id: this.conferenceId })
            };

            this.GetSpeakers();
        }


    }
}