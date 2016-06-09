namespace ConferenceApp.Controllers {

    export class RoomEditController {
        public room;
        public title = "Edit Room"
        public icon = "edit";
        public theme = "primary";
        public showDelete = true;     //to hide add form when edit is true

        public UpdateRoom() {

            this.$http.post('/api/rooms/' + this.room.id, this.room)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                });
        }

        public cancel() {
            this.$state.go('displayRooms', {id: this.room.conferenceId});
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
            public $state: ng.ui.IStateService,
            public breadcrumbService: ConferenceApp.Services.BreadcrumbService
        ) {

            $http.get(`/api/rooms/${$stateParams['id']}`)
                .then((response) => {
                    this.room = response.data;
                    console.log(this.room);
                    breadcrumbService.breadcrumbs = [{
                            text: 'My Conferences',
                            link: 'conferenceManage'
                        },{
                            text: this.room.conferenceName,
                            link: `schedule({id: ${this.room.conferenceId} })`
                        },{
                            text: 'Rooms',
                            link: `displayRooms({ id: ${this.room.conferenceId} })`
                        },{
                            text: this.room.name
                        }
                    ];
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }
    }

}