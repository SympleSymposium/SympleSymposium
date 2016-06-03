namespace ConferenceApp.Controllers {

    export class RoomDisplayController {
        public rooms;
         
        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService) {

            accountService.toolbarTitle = "Manage Rooms";

            //console.log($stateParams['id']);
            // $http.get('/api/rooms/' + $stateParams['id'])

            console.log('we are in the roomdisplay constructor');
            $http.get(`/api/rooms/manage/${$stateParams['id']}`)
                //$http.get(`/api/rooms/${this.rooms.conferenceId}`)
                .then((response) => {
                    this.rooms = response.data;
                    
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public DeleteRoom(id) {
            this.$http.delete(`/api/rooms/${id}`)
                .then((response) => {
                    this.$state.go("conferenceManage");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }


    }
}