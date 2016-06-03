namespace ConferenceApp.Controllers {

    export class RoomEditController {
        public room;
        public editView = true;

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {
       
            $http.get(`/api/rooms/${$stateParams['id']}`)
                .then((response) => {
                    this.room = response.data;
                    console.log(this.room);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public EditRoom() {

            let editedRoom = this.room;

            this.$http.post('/api/rooms/' + editedRoom.id, editedRoom)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                });
        }
    }

}