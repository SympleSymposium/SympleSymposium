namespace ConferenceApp.Controllers {

    export class RoomDisplayController {
        public rooms;
         
        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService) {
            //console.log($stateParams['id']);
            // $http.get('/api/rooms/' + $stateParams['id'])
            $http.get(`/api/rooms/manage/${$stateParams['id']}`)
                //$http.get(`/api/rooms/${this.rooms.conferenceId}`)
                .then((response) => {
                    this.rooms = response.data;
                    //console.log('we are in the roomdisplay constructor');
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public DeleteRoom(id) {
            console.log(id);
            this.$http.delete(`/api/rooms/${id}`)
                .then((response) => {
                    this.$state.go("confManage");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }


    }
}