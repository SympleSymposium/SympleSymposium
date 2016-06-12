namespace ConferenceApp.Controllers {

    export class SpeakerDisplayController {
        public speakers;
        public themeAdd = "md-accent";
        public themeEdit = "md-primary";

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
            private accountService: ConferenceApp.Services.AccountService) {

            accountService.toolbarTitle = "Manage Speakers";

            this.GetSpeakers();
        }


    }
}