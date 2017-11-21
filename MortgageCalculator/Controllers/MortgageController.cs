using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MortgageCalculator.Models.Entities;
using MortgageCalculator.Models.Repositories;

namespace MortgageCalculator.Controllers
{
    public class MortgageController : Controller
    {
        private readonly IRepository _repository;

        public MortgageController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetHistoryList()
        {
            var history = _repository.WhereAllEq<MortgageHistory>(new Dictionary<string, string>());
            return new CustomJson(new
            {
                history
            }, JsonRequestBehavior.AllowGet);
        }
    }
}