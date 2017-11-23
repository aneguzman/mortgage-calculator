mortgageCalculatorApp.controller('LogoutController', function ($rootScope, $scope, $location, $cookies) {

    /**
     * Release user info and redirect to calculator page.
     */
    localStorage.removeItem('loggedUser');
    $rootScope.user = null;
    $location.path('/');
});