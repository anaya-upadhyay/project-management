using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal.EfCore;

namespace Dal.EfCore.Tests.Core
{
    [TestCategory("Dal.Ef")]
    [TestClass]
    public abstract class BaseTest
    {
        private DatabaseContext context;

        /// <summary>
        /// Get a new Session
        /// </summary>
        /// <returns></returns>
        protected DatabaseContext GetSession()
        {
            context = new DatabaseContext();
            // context.Database.Migrate();
            return context;
        }
    }
}
