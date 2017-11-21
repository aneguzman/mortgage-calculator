mortgageCalculatorApp.factory('mortgageService', function($http, $q) {
    return {

        /**
        * Gets the mortgage monthly payment
        * @param {number} interestRate - The interest rate
        * @param {number} numberOfPeriods - the number of periods
        * @param {number} mortgageAmount - the mortgage amount
        * @returns {number} - The monthly payment
        */
        getMonthlyPayment: function(interestRate, numberOfPeriods, mortgageAmount) {
            var mortgageAmountif = Math.pow(1 + interestRate, numberOfPeriods);
            var monthlyMortgagePayment = - interestRate * mortgageAmount * mortgageAmountif / (mortgageAmountif - 1);
            return monthlyMortgagePayment;
        },

        /**
        * Gets the mortgage history list
        * @returns {Promise<Array>} - The list of mortgage calculations
        */
        getHistoryList: function () {
            var deferred = $q.defer();
            $http({
                method: "GET",
                url: "/Mortgage/GetHistoryList",
                cache: false
            })
            .success(function (data) {
                return deferred.resolve(data);
            });
            return deferred.promise;
        },
    };
});
