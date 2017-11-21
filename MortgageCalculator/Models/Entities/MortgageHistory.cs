using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MortgageCalculator.Models.Entities
{
    public class MortgageHistory
    {
        public virtual long Id { get; set; }
        public virtual double Amount { get; set; }
        public virtual int Amortization { get; set; }
        public virtual string PaymentFrequency { get; set; }
        public virtual double InterestRate { get; set; }
        public virtual double MonthlyPayment { get; set; }
        public virtual DateTime Created { get; set; }
    }
}