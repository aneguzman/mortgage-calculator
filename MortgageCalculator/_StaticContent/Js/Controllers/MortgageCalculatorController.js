mortgageCalculatorApp.controller('MortgageCalculatorController', function ($scope, mortgageService) {

    /**
     * Calculates the monthly payment
     * @param {Object} mortgageInfo - Object with mortgage data
     */
    $scope.calculateMonthlyPayment = function (mortgageInfo) {
        var interestRate = (mortgageInfo.interestRate / 100) / 12;
        var amount = mortgageInfo.amount;
        var amortization = mortgageInfo.amortization * 12;
        var monthlyPayment = mortgageService.getMonthlyPayment(interestRate, amortization, amount);
        $scope.monthlyPayment = Math.abs(monthlyPayment).toFixed(2);
    }   

    /**
     * INIT
     */
    $scope.mortgageInfo = {};
    $scope.monthlyPayment = 0;

});