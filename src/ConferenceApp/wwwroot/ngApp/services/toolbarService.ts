namespace ConferenceApp.Services {

    export class ToolbarService {

        public hideToolbar = true;

        public goBack() {
            console.log("original");
        };

    }

    angular.module('ConferenceApp').service('toolbarService', ToolbarService);
}
