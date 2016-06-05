namespace ConferenceApp.Controllers {

    export class SpeakerEditController {
        public speaker;
        public editView = true;

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {

            $http.get(`/api/speakers/${$stateParams['id']}`)
                .then((response) => {
                    this.speaker = response.data;
                    console.log(this.speaker);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public EditSpeaker() {

            let editedSpeaker = this.speaker;
            console.log("We are in the EditSpeaker method");
            console.log(editedSpeaker.id);
            this.$http.post('/api/speakers/' + editedSpeaker.id, editedSpeaker)
                .then((response) => {
                    console.log(editedSpeaker.id);
                    console.log(editedSpeaker);
                    this.$state.go("displaySpeakers", { id: this.speaker.conferenceId });
                });
        }
    }

}