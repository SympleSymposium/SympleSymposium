namespace ConferenceApp.Controllers {

    export class SpeakerAddController {
        public speaker;
        public title = "Add Speaker"
        public icon = "add_circle";
        public theme = "accent";
        public showDelete = false;  
       

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the speakerAdd constructor');
        }

        public cancel() {
            let conferenceId = parseInt(this.$stateParams['id']);
            this.$state.go('displaySpeakers', { id: conferenceId });
        }


        //NEED TO GET NO PHOTO TO SHOW UP!
        public UpdateSpeaker() {
            //console.log(this.speaker);
            if (!this.speaker.imageUrl) {
                this.speaker.imageUrl = "ngapp/images/"
            }
            this.speaker.conferenceId = parseInt(this.$stateParams['id']);
            //console.log(this.speaker);
            this.$http.post('/api/speakers', this.speaker)
                .then((response) => {
                    console.log(this.speaker);
                    this.$state.go("displaySpeakers", { id: this.speaker.conferenceId });
                })
        }

    }

}