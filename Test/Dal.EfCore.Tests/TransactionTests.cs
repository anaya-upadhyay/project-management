﻿using System;
using System.Linq;
using Dal.EfCore.Tests.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Dal;
using ProjectManagement.Dal.EfCore;
using ProjectManagement.Domain;

namespace Dal.EfCore.Tests
{
    [TestClass]
    public class TransactionTests : BaseTest
    {
        private DonorAggregate donor;

        [TestInitialize]
        public void Initialize()
        {
            donor = new DonorAggregate("name");
        }

        [TestMethod]
        public void Should_NotSave_When_Rollback()
        {
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (ITransaction tx = uow.BeginTransaction())
                {
                    var repo = uow.CreateRepository();
                    repo.Add(donor);
                    tx.Rollback();
                }
            }
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<DonorAggregate>().FirstOrDefault(x => x.Id == donor.Id);
                expected.Should().BeNull();
            }
        }

        [TestMethod]
        public void Should_Save_When_Commit()
        {
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                using (ITransaction tx = uow.BeginTransaction())
                {
                    var repo = uow.CreateRepository();
                    repo.Add(donor);
                    tx.Commit();
                }
            }
            using (IUnitOfWork uow = new UnitOfWork(GetSession()))
            {
                var repo = uow.CreateRepository();
                var expected = repo.Get<DonorAggregate>().FirstOrDefault(x => x.Id == donor.Id);
                expected.Should().NotBeNull();
            }
        }
    }
}
