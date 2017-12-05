using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace ProjectManagement.Dal.Nhb.Tests.Core
{
    [TestCategory("Dal.Nhb")]
    [TestClass]
    public abstract class BaseTest
    {
        protected readonly ISessionFactory sessionFactory;

        /// <summary>
        /// Construct the test class and initialize the Session Factory
        /// </summary>
        protected BaseTest()
        {
            sessionFactory = SessionFactory.GetSessionFactory();
        }

        /// <summary>
        /// Get a new Session
        /// </summary>
        /// <returns></returns>
        protected ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}