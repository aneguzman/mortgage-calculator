using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;
using MortgageCalculator.Models.ViewModels;
using MortgageCalculator.Services;
using RazorEngine;
using RazorEngine.Templating;

namespace MortgageCalculator.Controllers
{
    public class MortgageController : Controller
    {
        private readonly IMortgageService _mortgageService;
        private readonly string _errorMessage = "There was an erro when processing your request. Please try again";
        private readonly string _emailSent = "Email sent successfully";

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
        public ActionResult SendMail(MortgageEntryViewModel mortgageEntry, string email)
        {
            var success = _mortgageService.SendEmail(mortgageEntry, email);
            return new CustomJson(new
            {
                success = success,
                message = success ? _emailSent : _errorMessage
            }, JsonRequestBehavior.DenyGet);
        }
    }
}