namespace ConferenceApp.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';
    }


    export class SecretController {
        public secrets = [1];

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                //this.secrets = results.data;
                console.log("test");
            });

        }
    }

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

            console.log(JSON.stringify(editedRoom));

            this.$http.post('/api/rooms/' + editedRoom.id, JSON.stringify(editedRoom))
                .then((response) => {
                    this.$state.go("confManage");
                    //console.log("successful post");
                });
        }
        
    }

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


    export class ConfManageController {
        public conferences;
        public firstConference;

        constructor($http: ng.IHttpService) {
            $http.get('/api/conferences/manage')
                .then((response) => {
                    this.conferences = response.data;
                    this.firstConference = [this.conferences[0]];
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });


        }
    }

    export class ConfAddController {
        public conference;
        public showDelete = false;      //to hide edit when add is true

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService) {
        }

        public SubmitConference() {
            console.log(this.conference);
            this.$http.post('/api/conferences', this.conference)
                .then((response) => {
                    this.$state.go("confManage");
                })
        }
    }



    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
