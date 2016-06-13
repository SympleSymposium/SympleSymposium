namespace ConferenceApp.Controllers {
    export class PresentationEditController {
        public presentation;
        public title = "Edit Presentation"
        public icon = "edit";
        public theme = "md-primary";
        public showDelete = true;     //to hide add form when edit is true

        constructor(public $http: ng.IHttpService,
            public $stateParams: ng.ui.IStateParamsService,
            public $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService,
            public $mdDialog: ng.material.IDialogService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {
            console.log($stateParams['id']);

            //Hide BackButton
            toolbarService.hideBackButton = true;

            // $http.get('/api/presentations/' + $stateParams['id'])
            $http.get(`/api/presentations/${$stateParams['id']}`)
                //$http.get(`/api/presentations/${this.presentations.conferenceId}`)
                .then((response) => {
                    this.presentation = response.data;
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
        }
        public cancel() {
            this.$state.go('displayPresentations', { id: this.presentation.conferenceId });
        }

        public UpdatePresentation() {

            let editedPresentation = this.presentation;

            console.log((editedPresentation));

            this.$http.post('/api/presentations/' + editedPresentation.id, editedPresentation)
                .then((response) => {
                    this.$state.go("displayPresentations", { id: this.presentation.conferenceId });
                    //this.$state.go("displayPresentations");
                    //console.log("successful post");
                });
        }
        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Are you sure you want to delete this presentation?')
                //.textContent('This presentation will be deleted if you press the "Yes" button.')
                //.template('/ngApp/views/presentationConfirmDeleteModal.html')
                //.ariaLabel('Lucky day')
                //.targetEvent()
                .ok('Yes')
                .cancel('Cancel');
            return this.$mdDialog.show(confirm)
        }
        public DeletePresentation(id) {
            console.log(id);
            //Delete confirmation modal. The method returns a promise.
            this.ConfirmDelete()
                .then(() => {
                    this.$http.delete(`/api/presentations/${id}`)
                        .then((response) => {
                            //this.$state.go("displayPresentations", { id: this.presentation.id });
                            console.log(response.data);
                            this.$state.go("displayPresentations", { id: this.presentation.conferenceId });
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });

                });
        }
    }
}