namespace ConferenceApp.Controllers {

    export class RoomEditController {
        public room;
        public editView = true;
        public showDelete = true;     //to hide add form when edit is true

        public EditRoom() {

            this.$http.post('/api/rooms/' + this.room.id, this.room)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                });
        }

        public DeleteRoom() {
            console.log(this.room);
            this.$http.delete(`/api/rooms/${this.room.id}`)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }


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
    }

}