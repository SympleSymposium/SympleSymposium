namespace ConferenceApp.Controllers {

    export class PresentationDisplayController {
        public presentation;

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {
            //console.log($stateParams['id']);
            // $http.get('/api/rooms/' + $stateParams['id'])
            $http.get(`/api/presentations/manage/${$stateParams['id']}`)
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

        public DeletePresentation(id) {
            console.log(id);
            this.$http.delete(`/api/presentations/${id}`)
                .then((response) => {
                    this.$state.go("conferenceManage");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }


    }
}