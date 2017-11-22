mortgageCalculatorApp.controller('MortgageCalculatorController', function ($scope, mortgageService) {

    /**
     * Calculates the monthly payment
     * @param {Object} mortgageInfo - Object with mortgage data
     */
    $scope.calculateMonthlyPayment = function (mortgageInfo) {
        var monthlyInterestRate = (mortgageInfo.interestRate / 100) / 12;
        var numberOfPeriods = mortgageInfo.amortization * 12;
        var monthlyPayment = mortgageService.getMonthlyPayment(monthlyInterestRate, numberOfPeriods, mortgageInfo.amount);
        $scope.monthlyPayment = Math.abs(monthlyPayment).toFixed(2);

        //save the calculation
        mortgageInfo.monthlyPayment = $scope.monthlyPayment;
        $scope.calculationIsCompleted = true;
        saveCalculationEntry(mortgageInfo);
    }

    $scope.sendEmail = function (mortgageEntry, email) {
        mortgageEntry.monthlyPayment = $scope.monthlyPayment;
        var data = {
            mortgageEntry,
            email
        }
        mortgageService.sendMail(data)
            .then(function (data) {
                    if (data.success) {
                        alert(data.meassage);
                    } else {
                        alert(data.message);
                    }
                    $scope.calculationIsCompleted = false;

                },
                function () { //error
                    alert('There was an error processing your request. Please try again.');
                });
    }

    function saveCalculationEntry(data)
    {
        mortgageService.saveCalculationEntry(data)
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
    $scope.mortgageInfo = {
        paymentFrequency: 'Monthly',
    };
    $scope.monthlyPayment = 0;
    $scope.calculationIsCompleted = false;

});