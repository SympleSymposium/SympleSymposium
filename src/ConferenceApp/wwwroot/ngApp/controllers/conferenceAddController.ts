namespace ConferenceApp.Controllers {

    export class ConferenceAddController {
        public conference;
        public showDelete = false;      //to hide edit when add is true

        public SubmitConference() {
            //console.log(this.conference);
            if (!this.conference.imageUrl) {
                this.conference.imageUrl = 'http://vector.me/files/images/3/8/382633/white_board_silhouette_preview';
            }
            this.$http.post('/api/conferences', this.conference)
                .then((response) => {
                    this.$state.go("confManage");
                })
        }

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            private accountService: ConferenceApp.Services.AccountService) {

            accountService.toolbarTitle = "Add New Conference";

        }
    }

}