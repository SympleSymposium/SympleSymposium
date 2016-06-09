namespace ConferenceApp.Controllers {

    export class RoomAddController {
        public room: any = {}; 
        public title = "Add Room"
        public icon = "add_circle";
        public theme = "accent";
        public showDelete = false;//to hide edit when add is true
            


        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            public $stateParams: ng.ui.IStateParamsService) {
            console.log('we are in the roomadd constructor');
            this.room.conferenceId = parseInt(this.$stateParams['id'])
            console.log(this.room.conferenceId);
        }

        public cancel() {
            this.$state.go('displayRooms', { id: this.room.conferenceId });
        }

        public UpdateRoom() {
            //console.log(this.room);
            this.room.conferenceId = parseInt(this.$stateParams['id']);
            //console.log(this.room);
            this.$http.post('/api/rooms', this.room)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                })
        }

    }

}