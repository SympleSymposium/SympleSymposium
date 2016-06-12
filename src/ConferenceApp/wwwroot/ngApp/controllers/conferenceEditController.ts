namespace ConferenceApp.Controllers {

    export class ConferenceEditController {
        public conference;
        public title = "Edit Conference"
        public icon = "edit";
        public theme = "md-primary";
        public showDelete = true;     //to hide add form when edit is true 

        public UpdateConference() {

            let editedConf = this.conference;

            console.log(editedConf);

            this.$http.post('/api/conferences/' + editedConf.id, editedConf)
                .then((response) => {
                    this.$state.go("conferenceManage");
                    //console.log("successful post");
                });
        }

        public cancel() {
            this.$state.go("conferenceManage");
        }
        private ConfirmDelete() {
            var confirm = this.$mdDialog.confirm()
                .title('Are you sure you want to delete this conference?')
                //.textContent('This presentation will be deleted if you press the "Yes" button.')
                //.template('/ngApp/views/presentationConfirmDeleteModal.html')
                //.ariaLabel('Lucky day')
                //.targetEvent()
                .ok('Yes')
                .cancel('Cancel');
            return this.$mdDialog.show(confirm)
        }
        

        public DeleteConference() {
            console.log(this.conference.id);
            //Delete confirmation modal. The method returns a promise.
            this.ConfirmDelete()
                .then(() => {
                    this.$http.delete(`/api/conferences/${this.conference.id}`)
                        .then((response) => {
                            this.$state.go("conferenceManage");
                        })
                        .catch((response) => {
                            console.log(response.data);
                        });
                });

        }

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            $stateParams: ng.ui.IStateParamsService,
            private accountService: ConferenceApp.Services.AccountService,
            public $mdDialog: ng.material.IDialogService) {

            accountService.toolbarTitle = "Edit Conference Details";

            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    this.conference = response.data;
                    this.conference.startDate = new Date(this.conference.startDate);
                    this.conference.endDate = new Date(this.conference.endDate);
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }
    }
}