namespace ConferenceApp.Controllers {

    export class SpeakerEditController {
        public speaker;
        public title = "Edit Speaker"
        public icon = "edit";
        public theme = "md-primary";
        public showDelete = true;     //to hide add form when edit is true

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService,
            public $mdDialog: ng.material.IDialogService) {

            $http.get(`/api/speakers/${$stateParams['id']}`)
                .then((response) => {
                    this.speaker = response.data;
                    console.log(this.speaker);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }

        public cancel() {
            this.$state.go('displaySpeakers', { id: this.speaker.conferenceId });
        }

        public UpdateSpeaker() {

            let editedSpeaker = this.speaker;
            console.log("We are in the EditSpeaker method");
            console.log(editedSpeaker.id);
            this.$http.post('/api/speakers/' + editedSpeaker.id, editedSpeaker)
                .then((response) => {
                    console.log(editedSpeaker.id);
                    console.log(editedSpeaker);
                    this.$state.go("displaySpeakers", { id: this.speaker.conferenceId });
                });
        }

        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Are you sure you want to delete this speaker?')
                //.textContent('This speaker will be deleted if you press the "Yes" button.')
                //.template('/ngApp/views/presentationConfirmDeleteModal.html')
                //.ariaLabel('Lucky day')
                //.targetEvent()
                .ok('Yes')
                .cancel('Cancel');
            return this.$mdDialog.show(confirm)
        }

        public DeleteSpeaker(id) {
            console.log(id);
            console.log("In delete method.");
            //Added delete confirmation modal. The method returns a promise.
            this.ConfirmDelete()
                .then(() => {
                    this.$http.delete(`/api/speakers/${this.speaker.id}`)
                        .then((response) => {
                            this.$state.go("displaySpeakers", { id: this.speaker.conferenceId });
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });
                });
        }
    }


}