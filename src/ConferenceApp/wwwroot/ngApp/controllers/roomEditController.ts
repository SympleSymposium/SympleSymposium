namespace ConferenceApp.Controllers {

    export class RoomEditController {
        public room;
        public title = "Edit Room"
        public icon = "edit";
        public theme = "md-primary";
        public showDelete = true;     //to hide add form when edit is true

        public UpdateRoom() {

            this.$http.post('/api/rooms/' + this.room.id, this.room)
                .then((response) => {
                    this.$state.go("displayRooms", { id: this.room.conferenceId });
                });
        }

        public cancel() {
            this.$state.go('displayRooms', { id: this.room.conferenceId });
        }

        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Are you sure you want to delete this room?')
                //.textContent('This room will be deleted if you press the "Yes" button.')
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
                    this.$http.delete(`/api/rooms/${this.room.id}`)
                        .then((response) => {
                            this.$state.go("displayRooms", { id: this.room.conferenceId });
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });

                });
        }


        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService,
            public breadcrumbService: ConferenceApp.Services.BreadcrumbService,
            public $mdDialog: ng.material.IDialogService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            //Hide BackButton
            toolbarService.hideBackButton = true;

            $http.get(`/api/rooms/${$stateParams['id']}`)
                .then((response) => {
                    this.room = response.data;
                    console.log(this.room);
                    breadcrumbService.breadcrumbs = [{
                        text: 'My Conferences',
                        link: 'conferenceManage'
                    }, {
                            text: this.room.conferenceName,
                            link: `schedule({id: ${this.room.conferenceId} })`
                        }, {
                            text: 'Rooms',
                            link: `displayRooms({ id: ${this.room.conferenceId} })`
                        }, {
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