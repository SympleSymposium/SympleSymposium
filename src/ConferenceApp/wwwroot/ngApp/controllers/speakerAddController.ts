namespace ConferenceApp.Controllers {

    export class SpeakerAddController {
        public speaker;
        public addView = true;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the speakerAdd constructor');
        }

        public AddSpeaker() {
            console.log(this.speaker);
            this.speaker.conferenceId = parseInt(this.$stateParams['id']);
            console.log(this.speaker);
            this.$http.post('/api/speakers', this.speaker)
                .then((response) => {
                    console.log(this.speaker);
                    this.$state.go("displaySpeakers", { id: this.speaker.conferenceId });
                })
        }

    }

}