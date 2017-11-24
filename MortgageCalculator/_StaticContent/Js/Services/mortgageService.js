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
        * @returns {Promise<Object>} - Data object with the list of calculation entries
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

        /**
        * Saves the calculation entry 
        * @returns {Promise<Object>} - Data object received from the server
        */
        saveCalculationEntry: function (data) {
            var deferred = $q.defer();
            $http({
                    method: "POST",
                    url: "/Mortgage/AddCalculationEntry",
                    data: data,
                    cache: false
                })
                .success(function (data) {
                    return deferred.resolve(data);
                });
            return deferred.promise;
        },

        /**
        * Send email to a target user 
        * @returns {Promise<Object>} - Data object received from the server
        */
        sendMail: function (data) {
            var deferred = $q.defer();
            $http({
                    method: "POST",
                    url: "/Mortgage/SendMail",
                    data: data,
                    cache: false
                })
                .success(function (data) {
                    return deferred.resolve(data);
                });
            return deferred.promise;
        },
    };
});
