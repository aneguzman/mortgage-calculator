using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageCalculator.Models.Entities;

namespace MortgageCalculator.Models
{
    public class CustomJsonModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public IEnumerable<MortgageEntry> History { get; set; }
    }
}