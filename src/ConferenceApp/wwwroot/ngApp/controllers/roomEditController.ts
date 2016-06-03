namespace ConferenceApp.Controllers {

    export class RoomEditController {
        public room;
        public editView = true;

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {
            console.log($stateParams['id']);
            // $http.get('/api/rooms/' + $stateParams['id'])
            $http.get(`/api/rooms/${$stateParams['id']}`)
                //$http.get(`/api/rooms/${this.rooms.conferenceId}`)
                .then((response) => {
                    this.room = response.data;
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public EditRoom() {

            let editedRoom = this.room;

            this.$http.post('/api/rooms/' + editedRoom.id, editedRoom)
                .then((response) => {
                    this.$state.go("conferenceManage");
                    //console.log("successful post");
                });
        }
    }

}