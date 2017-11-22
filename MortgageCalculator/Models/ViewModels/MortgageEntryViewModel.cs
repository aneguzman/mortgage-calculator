using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MortgageCalculator.Models.Entities;

namespace MortgageCalculator.Models.ViewModels
{
    public class MortgageEntryViewModel
    {
        public long Id { get; set; }

        [Required]
        [Range(1, 9999999)]
        public double Amount { get; set; }

        [Required]
        [Range(1, 30)]
        public int Amortization { get; set; }
        public string PaymentFrequency { get; set; }

        [Required]
        [Range(0.5, 25)]
        public double InterestRate { get; set; }

        [Required]
        public double MonthlyPayment { get; set; }
        public DateTime Created { get; set; }

        public static implicit operator MortgageEntryViewModel(MortgageEntry entry)
        {
            return new MortgageEntryViewModel
            {
                Id               = entry.Id,
                Amount           = entry.Amount,
                Amortization     = entry.Amortization,
                PaymentFrequency = entry.PaymentFrequency,
                InterestRate     = entry.InterestRate,
                MonthlyPayment   = entry.MonthlyPayment,
                Created          = entry.Created
            };
        }

        public static implicit operator MortgageEntry(MortgageEntryViewModel vm)
        {
            return new MortgageEntry
            {
                Id               = vm.Id,
                Amount           = vm.Amount,
                Amortization     = vm.Amortization,
                PaymentFrequency = vm.PaymentFrequency,
                InterestRate     = vm.InterestRate,
                MonthlyPayment   = vm.MonthlyPayment,
                Created          = vm.Created
            };
        }
    }
}