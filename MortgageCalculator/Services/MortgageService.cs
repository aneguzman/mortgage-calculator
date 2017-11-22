using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;

namespace MortgageCalculator.Services
{
    public class MortgageService : IMortgageService
    {
        private readonly IRepository _repository;

        public MortgageService(IRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<MortgageEntry> GetHistory()
        {
            var filters = new Dictionary<string, string>();
            try
            {
                var mortgageHistory = _repository.WhereAllEq<MortgageEntry>(filters);
                return mortgageHistory;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool SaveCalculationEntry(MortgageEntry entry)
        {
            try
            {
                entry.Created = DateTime.Now;
                _repository.Save(entry);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}