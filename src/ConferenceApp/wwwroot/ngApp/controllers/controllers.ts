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


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
