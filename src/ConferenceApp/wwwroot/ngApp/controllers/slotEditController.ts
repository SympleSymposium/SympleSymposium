namespace ConferenceApp.Controllers {

    export class SlotEditController {
        public newSlot;
        public slot;
        public speakers;
        public presentations;
        public rooms;
        public editView = true;
        public showDelete = true;
        
        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService,
            public $mdDialog: ng.material.IDialogService) {
            console.log('edit');

            accountService.toolbarTitle = "Edit Slot";

            //this $http.get is to get the specific slot 
            $http.get(`/api/slots/edit/${$stateParams['id']}`)
                .then((response) => {
                    this.slot = response.data;
                    this.slot.startTime = moment(this.slot.startTime).toDate();
                    this.slot.endTime = moment(this.slot.endTime).toDate();
                    console.log(this.slot);

                    //these $http.gets are to build the dropdown list for speakers, presentations, and rooms
                    $http.get('/api/speakers/manage/' + this.slot.presentation.conferenceId)
                        .then((response) => {
                            this.speakers = response.data;
                            //console.log("speakers");
                            //console.log(this.speakers);
                        })

                    $http.get('/api/presentations/manage/' + this.slot.presentation.conferenceId)
                        .then((response) => {
                            this.presentations = response.data;
                        })

                    $http.get('/api/rooms/manage/' + this.slot.presentation.conferenceId)
                        .then((response) => {
                            this.rooms = response.data;
                        })
                })
                .catch((response) => {
                    console.log(response.data);
                    console.log(this.$stateParams['id']);
                });
        }

        public EditSlot() {

            let editedSlot = {
                id: this.slot.id,
                endTime: this.slot.endTime,
                startTime: this.slot.startTime,
                presentationId: this.slot.presentation.id,
                speakerId: this.slot.speaker.id,
                roomId: this.slot.room.id
            }

            this.$http.post('/api/slots/' + editedSlot.id, editedSlot)
                .then((response) => {
                    //console.log(editedSlot);
                    this.$state.go("schedule", { id: this.slot.room.conferenceId });
                })
        }

        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Are you sure you want to delete this slot?')
                //.textContent('This slot will be deleted if you press the "Yes" button.')
                //.template('/ngApp/views/presentationConfirmDeleteModal.html')
                //.ariaLabel('Lucky day')
                //.targetEvent()
                .ok('Yes')
                .cancel('Cancel');
            return this.$mdDialog.show(confirm)
        }

        public DeleteSlot() {
            //Added delete confirmation modal. The method returns a promise.
            this.ConfirmDelete()
                .then(() => {
                    console.log("slot id = " + this.slot.id);
                    this.$http.delete(`/api/slots/${this.slot.id}`)
                        .then((response) => {
                            console.log(response.data);
                            this.$state.go("schedule", { id: this.slot.room.conferenceId });
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });

                });
        }


    }

}