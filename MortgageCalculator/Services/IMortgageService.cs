using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MortgageCalculator.Models.Entities;

namespace MortgageCalculator.Services
{
    public interface IMortgageService
    {
        IEnumerable<MortgageEntry> GetHistory();
        bool SaveCalculationEntry(MortgageEntry entry);
    }
}
