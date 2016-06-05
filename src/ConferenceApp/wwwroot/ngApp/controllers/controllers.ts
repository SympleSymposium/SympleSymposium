namespace ConferenceApp.Controllers {

    //export class PresentationDisplayController {
    //    public presentation;

    //    constructor(public $http: ng.IHttpService,
    //        public $stateParams: ng.ui.IStateParamsService,
    //        public $state: ng.ui.IStateService) {
    //        //console.log($stateParams['id']);
    //        // $http.get('/api/rooms/' + $stateParams['id'])
    //        $http.get(`/api/presentations/manage/${$stateParams['id']}`)
    //            //$http.get(`/api/rooms/${this.rooms.conferenceId}`)
    //            .then((response) => {
    //                this.presentation = response.data;
    //                console.log('we are in the roomdisplay constructor');
    //                console.log(response.data);
    //            })
    //            .catch((response) => {
    //                console.log(response.data);
    //            });
    //    }

    //    public DeletePresentation(id) {
    //        console.log(id);
    //        this.$http.delete(`/api/presentations/${id}`)
    //            .then((response) => {
    //                this.$state.go("conferenceManage");
    //            })
    //            .catch((response) => {
    //                console.log(response.data);
    //            });

    //    }


    //}

    //export class PresentationEditController {
    //    public presentation;
    //    public editView = true;

    //    constructor(public $http: ng.IHttpService,
    //        public $stateParams: ng.ui.IStateParamsService,
    //        public $state: ng.ui.IStateService) {
    //        console.log($stateParams['id']);
    //        // $http.get('/api/presentations/' + $stateParams['id'])
    //        $http.get(`/api/presentations/${$stateParams['id']}`)
    //            //$http.get(`/api/presentations/${this.presentations.conferenceId}`)
    //            .then((response) => {
    //                this.presentation = response.data;
    //                console.log(response.data);
    //            })
    //            .catch((response) => {
    //                console.log(response.data);
    //            });
    //    }

    //    public EditPresentation() {

    //        let editedPresentation = this.presentation;

    //        console.log(JSON.stringify(editedPresentation));

    //        this.$http.post('/api/presentations/' + editedPresentation.id, JSON.stringify(editedPresentation))
    //            .then((response) => {
    //                this.$state.go("conferenceManage");
    //                //console.log("successful post");
    //            });
    //    }

    //}

    //export class PresentationAddController {
    //    public presentation;
    //    public addView = true;

    //    constructor(private $http: ng.IHttpService,
    //        private $state: ng.ui.IStateService,
    //        public $stateParams: ng.ui.IStateParamsService) {
    //        console.log('we are in the presentationAdd constructor');
    //    }

    //    public AddPresentation() {
    //        console.log(this.presentation);
    //        this.presentation.conferenceId = parseInt(this.$stateParams['id']);
    //        console.log(this.presentation);
    //        this.$http.post('/api/presentations', this.presentation)
    //            .then((response) => {
    //                console.log("middle of post in AddPresentation ");
    //                this.$state.go("conferenceManage");
    //            });
    //        console.log("End of Add Presentation in controller.ts");
    //    }

    //}

}
