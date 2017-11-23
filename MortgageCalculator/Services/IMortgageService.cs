using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.ViewModels;

namespace MortgageCalculator.Services
{
    public interface IMortgageService
    {
        IEnumerable<MortgageEntry> GetHistory();
        bool SaveCalculationEntry(MortgageEntry entry);
        bool SendEmail(MortgageEntryViewModel mortgageEntry, string email);
    }
}
