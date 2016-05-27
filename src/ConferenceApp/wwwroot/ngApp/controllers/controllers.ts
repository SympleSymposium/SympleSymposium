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

    export class ManagedConferenceController {
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
    export class AddConferenceController {
        public newConference;
        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService) {
        }

        public AddConference() {
            console.log("Post");
            this.$http.post('/api/conferences', this.newConference)
                .then((response) => {
                    this.$state.go("manageConferences");
                })
        }
    }

    
    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
