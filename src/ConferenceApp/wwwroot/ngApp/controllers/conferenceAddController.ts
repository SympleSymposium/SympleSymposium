namespace ConferenceApp.Controllers {

    export class ConferenceAddController {
        public conference;
        public title = "Add Conference"
        public icon = "add_circle";
        public theme = "md-accent";
        public showDelete = false;//to hide edit when add is true

        public UpdateConference() {
            //console.log(this.conference);
            if (!this.conference.imageUrl) {
                this.conference.imageUrl = "ngapp/images/wb.png";
            }
            this.$http.post('/api/conferences', this.conference)
                .then((response) => {
                    this.$state.go("conferenceManage");
                })
        }

        public cancel() {
            this.$state.go("conferenceManage");
        }


        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Add New Conference";

            //Hide BackButton
            toolbarService.hideBackButton = true;

        }
    }

}