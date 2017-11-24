describe('mortgageService', function () {
    beforeEach(
        module('ngRoute','ngCookies')
    );

    var $http, $q;

    beforeEach(function () {
        $http = {};
        $q = {};
    });

    beforeEach(angular.mock.inject(function (_$provider_) {
        $factory = _$provider_;
        $mortgageFactory = $factory('mortgageService', { $http: $http, $q: $q});
    }));

    describe('getMonthlyPayment', function () {
        var interestRate, numberOfPeriods, mortgageAmount;
        beforeEach(function () {
            interestRate = 9.4;
            numberOfPeriods = 25;
            mortgageAmount = 500000;
        });

        it('should calculate the monthly payment', function () {
            var monthlyPayment = $factory.getMonthlyPayment(interestRate, numberOfPeriods, mortgageAmount);
            expect(monthlyPayment.toBe(4333.78));
        });
    });
})