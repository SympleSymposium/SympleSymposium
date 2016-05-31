namespace ConferenceApp.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';
    }


    export class SecretController {
        public secrets = [1];

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                //this.secrets = results.data;
                console.log("test");
            });
           
        }
    }
     
    export class ConfManageController {
        public conferences;
        public firstConference;

        constructor($http: ng.IHttpService) {
            $http.get('/api/conferences/manage')
                .then((response) => {
                    this.conferences = response.data;
                    this.firstConference = [this.conferences[0]];
                    console.log(response.data);
                })
                .catch((response) => {
                    console.log(response.data);
                });
            

        }
    }
    export class ConfAddController {
        public conference;
        public showDelete = false;      //to hide edit when add is true

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService) {
        }

        public SubmitConference() {
            //console.log(this.conference);
            this.$http.post('/api/conferences', this.conference)
                .then((response) => {
                    this.$state.go("confManage");
                })
        }
    }
    


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
