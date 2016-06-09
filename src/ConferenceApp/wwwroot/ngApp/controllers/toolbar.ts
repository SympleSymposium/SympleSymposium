namespace ConferenceApp.Controllers {

    export class ToolbarController {

        public get breadcrumbs() {
            return this.breadcrumbService.breadcrumbs;
        }

        public get lastBreadcrumb() {
            return this.breadcrumbService.breadcrumbs[this.breadcrumbService.breadcrumbs.length - 1];
        }

        constructor(private breadcrumbService: ConferenceApp.Services.BreadcrumbService) {}
    }
    angular.module("ConferenceApp").controller("ToolbarController", ToolbarController);
}

namespace ConferenceApp.Services {

    export class BreadcrumbService {

        public breadcrumbs = [];
    }
    angular.module('ConferenceApp').service('breadcrumbService', BreadcrumbService);
}