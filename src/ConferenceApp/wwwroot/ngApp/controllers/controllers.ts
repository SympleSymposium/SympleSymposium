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

        constructor($http: ng.IHttpService) {
            $http.get('/api/conferences/manage')
                .then((response) => {
                    this.conferences = response.data;
                })
                .catch((response) => {
                    console.log(response.data);
                });
            console.log(this.conferences);

        }
    }
    export class ConfAddController {
        public newConference;

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService) {
        }

        public AddConference() {
            console.log(this.newConference);
            this.$http.post('/api/conferences', this.newConference)
                .then((response) => {
                    this.$state.go("confManage");
                })
        }
    }


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
