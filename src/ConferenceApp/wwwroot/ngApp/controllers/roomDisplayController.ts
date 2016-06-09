namespace ConferenceApp.Controllers {

    export class RoomDisplayController {
        public rooms;

        private GetRooms() {
            this.$http.get(`/api/rooms/manage/${this.$stateParams['id']}`)
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
            public $mdDialog: ng.material.IDialogService) {

            accountService.toolbarTitle = "Manage Rooms";

            this.GetRooms();
        }

        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Would you like to delete this room?')
                .textContent('This room will be deleted if you press the "Yes" button.')
                //.template('/ngApp/views/presentationConfirmDeleteModal.html')
                //.ariaLabel('Lucky day')
                //.targetEvent()
                .ok('Yes')
                .cancel('Cancel');
            return this.$mdDialog.show(confirm)
        }

        public DeleteRoom(id) {
            //Added delete confirmation modal. The method returns a promise.
            this.ConfirmDelete()
                .then(() => {
                    this.$http.delete(`/api/rooms/${id}`)
                        .then((response) => {
                            this.GetRooms();
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });

                });
        }


    }
}