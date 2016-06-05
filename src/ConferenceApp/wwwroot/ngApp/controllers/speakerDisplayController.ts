namespace ConferenceApp.Controllers {

    export class SpeakerDisplayController {
        public speakers;

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

        public DeleteSpeaker(id) {
            console.log(id);
            console.log("In delete method.");
            this.$http.delete(`/api/speakers/${id}`)
                .then((response) => {
                    this.GetSpeakers();
                    console.log("In delete method after GetSpeakers.");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }


    }
}