namespace ConferenceApp.Controllers {

    export class ConferenceManageController {
        public conferences;
        public firstConference;
        public toolbarTitle;
        public theme = "md-accent";
        public themeEdit = "md-primary";
        public themeSched = "md-warn";

        constructor($http: ng.IHttpService,
            private accountService: ConferenceApp.Services.AccountService,
            private toolbarService: ConferenceApp.Services.ToolbarService) {

            accountService.toolbarTitle = "Manage Conferences";

            //Hide BackButton
            toolbarService.hideBackButton = true;

            //Show Toolbar
            toolbarService.hideToolbar = false;

            $http.get('/api/conferences/manage')
                .then((response) => {
                    this.conferences = response.data;
                    this.firstConference = [this.conferences[0]];
                    //console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });


        }
    }

}