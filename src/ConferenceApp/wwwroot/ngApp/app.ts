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
                controller: ConferenceApp.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('confManage', {
                url: '/conferences/manage',
                templateUrl: '/ngApp/views/confManage.html',                
                controller: ConferenceApp.Controllers.ConfManageController,
                controllerAs: 'controller'
            }) 
            .state('confEdit', {
                url: '/conferences/edit/:id', 
                templateUrl: '/ngApp/views/confEdit.html',
                controller: ConferenceApp.Controllers.ConfEditController,
                controllerAs: 'controller'
            }) 
            .state('confAdd', {
                url: '/conferences/add',
                templateUrl: '/ngApp/views/confEdit.html',
                controller: ConferenceApp.Controllers.ConfAddController,
                controllerAs: 'controller'
            })    
            .state('displayRooms', {
                url: '/rooms/:id',
                templateUrl: '/ngApp/views/displayRooms.html',
                controller: ConferenceApp.Controllers.RoomDisplayController,
                controllerAs: 'controller'
            })    
            .state('roomEdit', {
                url: '/rooms/edit/:id',
                templateUrl: '/ngApp/views/roomEdit.html',
                controller: ConferenceApp.Controllers.RoomEditController,
                controllerAs: 'controller'
            }) 
            .state('roomAdd', { 
                url: '/rooms/add/:id',
                templateUrl: '/ngApp/views/roomEdit.html',
                controller: ConferenceApp.Controllers.RoomAddController,
                controllerAs: 'controller'
            })               
            .state('secret', {
                url: '/secret',
                templateUrl: '/ngApp/views/secret.html',
                controller: ConferenceApp.Controllers.SecretController,
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
            .state('about', {
                url: '/about',
                templateUrl: '/ngApp/views/about.html',
                controller: ConferenceApp.Controllers.AboutController,
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
