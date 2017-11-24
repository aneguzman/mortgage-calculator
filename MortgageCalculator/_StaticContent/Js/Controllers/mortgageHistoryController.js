mortgageCalculatorApp.controller('MortgageHistoryController', function ($scope, mortgageService) {

    /**
     * Get the list of mortgage calculation history
     */
    $scope.loadHistory = function () {
        mortgageService.getHistoryList()
            .then(function (data) {
                if (data.success) {
                    $scope.mortgageHistory = data.history;
                } else {
                    alert(data.message);
                }
            },
            function () { //error
                alert('There was an error processing your request. Please try again.');
            });
    } 

    /**
     * INIT
     */
    $scope.mortgageHistory = [];
    $scope.loadHistory();

});