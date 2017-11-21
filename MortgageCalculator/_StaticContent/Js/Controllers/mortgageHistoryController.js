mortgageCalculatorApp.controller('MortgageHistoryController', function ($scope, mortgageService) {

    $scope.loadHistory = function () {
        mortgageService.getHistoryList()
            .then(function (data) {
                if (data) {
                    console.log(data.history);
                    $scope.mortgageHistory = data.history;
//                    if (data.status != "success") {
//                    } else {
//                        console.log(data.history);
//                        $scope.mortgageHistory = data.history;
//                    }
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