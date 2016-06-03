namespace ConferenceApp {

    angular.module('ConferenceApp', ['ui.router', 'ngResource', 'ngMaterial']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => { 
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controllerAs: 'controller'
            })
            .state('conferenceManage', {
                url: '/conferences/manage',
                templateUrl: '/ngApp/views/conferenceManage.html',                
                controller: ConferenceApp.Controllers.ConferenceManageController,
                controllerAs: 'controller'
            }) 
            .state('schedule', {
                url: '/schedule/:id',
                templateUrl: '/ngApp/views/schedule.html',
                controller: ConferenceApp.Controllers.ScheduleController,
                controllerAs: 'controller'
            })
            .state('conferenceEdit', {
                url: '/conferences/edit/:id', 
                templateUrl: '/ngApp/views/conferenceAddEdit.html',
                controller: ConferenceApp.Controllers.ConferenceEditController,
                controllerAs: 'controller'
            }) 
            .state('conferenceAdd', {
                url: '/conferences/add',
                templateUrl: '/ngApp/views/conferenceAddEdit.html',
                controller: ConferenceApp.Controllers.ConferenceAddController,
                controllerAs: 'controller'
            })    
            .state('displayRooms', {
                url: '/rooms/:id',
                templateUrl: '/ngApp/views/roomDisplay.html',
                controller: ConferenceApp.Controllers.RoomDisplayController,
                controllerAs: 'controller'
            })    
            .state('roomEdit', {
                url: '/rooms/edit/:id',
                templateUrl: '/ngApp/views/roomAddEdit.html',
                controller: ConferenceApp.Controllers.RoomEditController,
                controllerAs: 'controller'
            }) 
            .state('roomAdd', { 
                url: '/rooms/add/:id',
                templateUrl: '/ngApp/views/roomAddEdit.html',
                controller: ConferenceApp.Controllers.RoomAddController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: ConferenceApp.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: ConferenceApp.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: ConferenceApp.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            })
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });


    angular.module('ConferenceApp').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('ConferenceApp').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });



}
