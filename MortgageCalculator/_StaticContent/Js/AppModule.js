var mortgageCalculatorApp = angular.module('mortgageCalculatorApp',
    [
        'ngRoute',
        'ngCookies',
    ]);

mortgageCalculatorApp.run(function($rootScope, $window) {
    $rootScope.user = $window.localStorage.getItem('loggedUser') || null;
});

mortgageCalculatorApp.config(function ($routeProvider, $httpProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/_StaticContent/Partials/MortgageCalculator/MortgageCalculator.html',
            controller: 'MortgageCalculatorController',
            reloadOnSearch: false
        })
        .when('/History', {
            templateUrl: '/_StaticContent/Partials/MortgageCalculator/History.html',
            controller: 'MortgageHistoryController',
            reloadOnSearch: false
        })
        .when('/Login', {
            templateUrl: '/_StaticContent/Partials/Auth/Login.html',
            controller: 'LoginController',
            reloadOnSearch: false
        })
        .when('/Register', {
            templateUrl: '/_StaticContent/Partials/Auth/Register.html',
            controller: 'RegisterController',
            reloadOnSearch: false
        })
        .when('/Logout', {
            templateUrl: '/Account/Logout',
            controller: 'LogoutController',
            disableCache: true
        })
        .otherwise({
            templateUrl: '/_StaticContent/Partials/MortgageCalculator/MortgageCalculator.html',
            controller: 'MortgageCalculatorController',
            reloadOnSearch: false
        });
    $httpProvider.interceptors.push('authHttpResponseMiddleware');
});
