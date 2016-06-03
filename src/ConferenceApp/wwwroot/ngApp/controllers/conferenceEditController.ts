namespace ConferenceApp.Controllers {

    export class ConferenceEditController {
        public showDelete = true;     //to hide add form when edit is true
        public conference; 
         
        public SubmitConference() {

            let editedConf = this.conference;

            console.log(editedConf);

            this.$http.post('/api/conferences/' + editedConf.id, JSON.stringify(editedConf))
                .then((response) => {
                    this.$state.go("conferenceManage");
                    //console.log("successful post");
                });
        }


        public DeleteConference() {         
            console.log(this.conference.id);  
            this.$http.delete(`/api/conferences/${this.conference.id}`)
                .then((response) => {
                    this.$state.go("conferenceManage");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            $stateParams: ng.ui.IStateParamsService, private accountService: ConferenceApp.Services.AccountService) {

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