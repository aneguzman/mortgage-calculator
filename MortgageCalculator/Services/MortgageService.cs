using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
using MortgageCalculator.Models.ViewModels;
using RazorEngine.Templating;

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

        public bool SendEmail(MortgageEntryViewModel mortgageEntry, string email)
        {
            var templateUrl = Path.Combine(HttpContext.Current.Server.MapPath("~/EmailTemplates"), "MortgageCalculationResults.cshtml");
            var templateService = new TemplateService();
            var emailBody = templateService.Parse(System.IO.File.ReadAllText(templateUrl), mortgageEntry, null, null);

            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.Subject = "Your Mortgage Calculation results!";
            message.Body = emailBody;
            message.IsBodyHtml = true;
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}