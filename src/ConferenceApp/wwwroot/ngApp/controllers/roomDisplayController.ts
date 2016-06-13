namespace ConferenceApp.Controllers {

    export class RoomDisplayController {
        public rooms;
        public title = "Rooms"
        public icon = "panorama_vertical";
        public theme = "md-primary";
        public themeAdd = "md-accent";
        public themeEdit = "md-primary";
        public conferenceId;

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
            private accountService: ConferenceApp.Services.AccountService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Manage Rooms";
            this.conferenceId = $stateParams['id'];

            toolbarService.goBack = () => {
                console.log("tried");
                //this.$state.go("conferenceManage");
                this.$state.go("schedule", { id: this.conferenceId })
            };

            this.GetRooms();
        }

        
    }
}