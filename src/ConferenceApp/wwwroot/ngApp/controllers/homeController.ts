namespace ConferenceApp.Controllers {

    export class HomeController {

        constructor(private toolbarService: ConferenceApp.Services.ToolbarService,
            private accountService: ConferenceApp.Services.AccountService,
            public $state: ng.ui.IStateService) {

            toolbarService.hideToolbar = true;
            console.log(accountService.isLoggedIn());

            //Redirect to manage conference page if user is logged in
            if (accountService.isLoggedIn() != null) {
                this.$state.go("conferenceManage");
            }


        }
    }

}