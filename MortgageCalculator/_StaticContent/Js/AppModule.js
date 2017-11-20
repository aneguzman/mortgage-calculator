var mortgageCalculatorApp = angular.module('mortgageCalculatorApp',
    [
        'ngRoute',
        'ngCookies',
    ]);

mortgageCalculatorApp.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/_StaticContent/Partials/MortgageCalculator/MortgageCalculator.html',
            controller: 'MortgageCalculatorController',
            reloadOnSearch: false
        })
        .otherwise({
            templateUrl: '/_StaticContent/Partials/MortgageCalculator/MortgageCalculator.html',
            controller: 'MortgageCalculatorController',
            reloadOnSearch: false
        });
});
