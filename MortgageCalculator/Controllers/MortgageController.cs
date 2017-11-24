using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MortgageCalculator.Models;
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
        private readonly string _errorMessage = "There was an error when processing your request. Please try again";
        private readonly string _emailSent = "Email sent successfully";

        public MortgageController(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get the history of the calculation entries
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult GetHistoryList()
        {
            var history = _mortgageService.GetHistory();
            if (history == null)
            {
                return new CustomJson(new CustomJsonModel
                {
                    Success = false,
                    Message = _errorMessage
                }, JsonRequestBehavior.AllowGet);
            }
            return new CustomJson(new CustomJsonModel
            {
                Success = true,
                History = history
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Save a transaction in the DB.
        /// </summary>
        /// <param name="entry">Object with the mortgage info.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCalculationEntry(MortgageEntryViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var success = _mortgageService.SaveCalculationEntry(entry);
                if (success)
                {
                    return new CustomJson(new CustomJsonModel
                    {
                        Success = true,
                    }, JsonRequestBehavior.DenyGet);
                }
            }
            return new CustomJson(new CustomJsonModel
            {
                Success = false,
                Message = _errorMessage
            }, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// Send Email to a targe user.
        /// </summary>
        /// <param name="mortgageEntry">Object with the mortgage info.</param>
        /// <param name="email">The target email</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendMail(MortgageEntryViewModel mortgageEntry, string email)
        {
            var success = false;
            if (ModelState.IsValid)
            {
                success = _mortgageService.SendEmail(mortgageEntry, email);
            }
            return new CustomJson(new CustomJsonModel
            {
                Success = success,
                Message = success ? _emailSent : _errorMessage
            }, JsonRequestBehavior.DenyGet);
        }
    }
}