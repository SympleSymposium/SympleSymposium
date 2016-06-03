namespace ConferenceApp.Controllers {

    export class RoomAddController {
        public room;
        public addView = true;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the roomadd constructor');
        }

        public AddRoom() {
            console.log(this.room);
            this.room.conferenceId = parseInt(this.$stateParams['id']);
            console.log(this.room);
            this.$http.post('/api/rooms', this.room)
                .then((response) => {
                    this.$state.go("confManage");
                })
        }

    }

}