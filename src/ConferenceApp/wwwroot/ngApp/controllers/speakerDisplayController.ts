namespace ConferenceApp.Controllers {

    export class SpeakerDisplayController {
        public speakers;

        private GetSpeakers() {
            console.log("In GetSpeakers method");
            this.$http.get(`/api/speakers/manage/${this.$stateParams['id']}`)
                .then((response) => {
                    this.speakers = response.data;
                    console.log(this.speakers)
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

            accountService.toolbarTitle = "Manage Speakers";

            this.GetSpeakers();
        }

        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Would you like to delete this speaker?')
                .textContent('This speaker will be deleted if you press the "Yes" button.')
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
                    this.$http.delete(`/api/speakers/${id}`)
                        .then((response) => {
                            this.GetSpeakers();
                            console.log("In delete method after GetSpeakers.");
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });
                });

        }


    }
}