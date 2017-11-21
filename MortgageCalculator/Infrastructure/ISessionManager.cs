using NHibernate;

namespace MortgageCalculator.Infrastructure
{
    public interface ISessionManager
    {
        ISession CurrentSession { get; set; }
    }
}