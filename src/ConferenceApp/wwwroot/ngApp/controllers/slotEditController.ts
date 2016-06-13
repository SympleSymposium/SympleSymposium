namespace ConferenceApp.Controllers {

    export class SlotEditController {
        public newSlot;
        public slot;
        public speakers;
        public presentations;
        public rooms;
        public showDelete = true;
        public dayDisabled = true;
        public day;
        public conferenceId;
        public title = "Edit Slot"
        public icon = "edit";
        public theme = "md-primary";
        
        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService,
            public $mdDialog: ng.material.IDialogService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {
            //console.log('edit');

            accountService.toolbarTitle = "Edit Slot";

            //Hide BackButton
            toolbarService.hideBackButton = true;

            //this $http.get is to get the specific slot 
            $http.get(`/api/slots/edit/${$stateParams['id']}`)
                .then((response) => {
                    this.slot = response.data;
                    this.slot.startTime = moment(this.slot.startTime).toDate();
                    this.slot.endTime = moment(this.slot.endTime).toDate();
                    this.day = moment(this.slot.startTime).format("M/D/YYYY");
                    this.conferenceId = this.slot.presentation.conferenceId;


                    //these $http.gets are to build the dropdown list for speakers, presentations, and rooms
                    $http.get('/api/speakers/manage/' + this.slot.presentation.conferenceId)
                        .then((response) => {
                            this.speakers = response.data;
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

        public cancel() {
            this.$state.go('schedule', {
                id: this.conferenceId
            });
        }

        public UpdateSlot() {

            let editedSlot = {
                id: this.slot.id,

                //Start time and end time must be formatted like this to deal with local times
                startTime: moment(this.slot.startTime).format("M/D/YYYY h:mm:ss A"),
                endTime: moment(this.slot.endTime).format("M/D/YYYY h:mm:ss A"),
                
                presentationId: this.slot.presentation.id,
                speakerId: this.slot.speaker.id,
                roomId: this.slot.room.id
            }

            this.$http.post('/api/slots/' + editedSlot.id, editedSlot)
                .then((response) => {
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