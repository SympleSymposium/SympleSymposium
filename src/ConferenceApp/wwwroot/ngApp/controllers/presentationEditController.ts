namespace ConferenceApp.Controllers {
    export class PresentationEditController {
        public presentation;
        public editView = true;

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {
            console.log($stateParams['id']);
            // $http.get('/api/presentations/' + $stateParams['id'])
            $http.get(`/api/presentations/${$stateParams['id']}`)
                //$http.get(`/api/presentations/${this.presentations.conferenceId}`)
                .then((response) => {
                    this.presentation = response.data;
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public EditPresentation() {

            let editedPresentation = this.presentation;

            console.log(JSON.stringify(editedPresentation));

            this.$http.post('/api/presentations/' + editedPresentation.id, JSON.stringify(editedPresentation))
                .then((response) => {
                    this.$state.go("conferenceManage");
                    //console.log("successful post");
                });
        }

    }
}