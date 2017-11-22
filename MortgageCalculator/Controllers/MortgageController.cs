using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
using MortgageCalculator.Models.ViewModels;
using MortgageCalculator.Services;

namespace MortgageCalculator.Controllers
{
    public class MortgageController : Controller
    {
        private readonly IMortgageService _mortgageService;
        private readonly string _errorMessage = "There was an erro when processing your request. Please try again";

        public MortgageController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetHistoryList()
        {
            var history = _mortgageService.GetHistory();
            if (history == null)
            {
                return new CustomJson(new
                {
                    success = false,
                    message = _errorMessage
                }, JsonRequestBehavior.AllowGet);
            }
            return new CustomJson(new
            {
                success = true,
                history
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddCalculationEntry(MortgageEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var success = _mortgageService.SaveCalculationEntry(entry);
                if (success)
                {
                    return new CustomJson(new
                    {
                        success = true,
                    }, JsonRequestBehavior.DenyGet);
                }
            }
            return new CustomJson(new
            {
                success = false,
                message = _errorMessage
            }, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public async Task<ActionResult> SendMail(MortgageEntryViewModel mortgageEntry, string email)
        {
            var body = "<h3>Mortgage Calculator results:</h3>" +
                       $"Amount: {mortgageEntry.Amount} <br/> Amortization: {mortgageEntry.Amortization} years<br/>" +
                       $"Payment frequency: {mortgageEntry.PaymentFrequency}<br/> Interest rate:{mortgageEntry.InterestRate}<br/>" +
                       $"Monthly payment: {mortgageEntry.MonthlyPayment}";
                       ;
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.Subject = "Your email subject";
            message.Body = body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
            return new CustomJson(new
            {
                success = true,
                message = "Email sent successfully"
            }, JsonRequestBehavior.DenyGet);
        }
    }
}