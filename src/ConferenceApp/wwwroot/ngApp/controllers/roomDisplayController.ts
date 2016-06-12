namespace ConferenceApp.Controllers {

    export class RoomDisplayController {
        public rooms;
        public title = "Rooms"
        public icon = "panorama_vertical";
        public theme = "md-primary";
        public themeAdd = "md-accent";
        public themeEdit = "md-primary";

        private GetRooms() {
            this.$http.get(`/api/rooms/manage/${
                this.$stateParams['id']}`)
                .then((response) => {
                    this.rooms = response.data;
                })

                .catch((response) => {
                    console.log(response.data);
                });
        }

        constructor(private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService) {

            accountService.toolbarTitle = "Manage Rooms";

            this.GetRooms();
        }

        
    }
}