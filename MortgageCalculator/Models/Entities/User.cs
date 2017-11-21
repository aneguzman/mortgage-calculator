using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MortgageCalculator.Models.Entities
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }
}