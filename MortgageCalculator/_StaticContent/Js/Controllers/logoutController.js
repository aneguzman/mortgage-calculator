mortgageCalculatorApp.controller('LogoutController', function ($rootScope, $scope, $location, $templateCache) {

    /**
     * Release user info and redirect to calculator page.
     */
    $templateCache.removeAll();
    localStorage.removeItem('loggedUser');
    $rootScope.user = null;
    $location.path('/');
});