// <summary>
// <copyright file="UserServiceTest.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Test.Service.User
{
    using AutoMapper;
    using Axity.Users.DataAccess.User;
    using Axity.Users.Entities.Context;
    using Axity.Users.Services.User;
    using Axity.Users.Services.User.Impl;
    using Axity.Users.Test.Util;
    using NUnit.Framework;

    /// <summary>
    /// UserService test.
    /// </summary>
    [TestFixture]
    public class UserServiceTest
    {
        private IMapper mapper;
        private DatabaseContext databaseContext;
        private IUserService modelService;
        private IUserDao modelDao;

        /// <summary>
        /// Setup.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            this.mapper = TestUtil.CreateAutomapper();
            this.databaseContext = TestUtil.DataBaseContext("users");
            this.databaseContext.Users.Add(new Entities.Model.User.UserModel
            {
                Age = 18,
                Id = 1,
                Name = "axity",
                Status = true,
                SurName = "axity",
            });
            this.databaseContext.SaveChanges();
            this.modelDao = new UserDao(this.databaseContext);
            this.modelService = new UserService(this.modelDao, this.mapper);
        }

        /// <summary>
        /// Paginator test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "get paginator user")]
        [Category("users")]
        public async Task PaginatorTest()
        {
            var result = await this.modelService.Paginator(10, 1);
            Assert.That(result.Rows[0].Name, Is.EqualTo("axity"));
            Assert.That(result.TotalRow, Is.EqualTo(1));
        }

        /// <summary>
        /// Create test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "create  user")]
        [Category("users")]
        public async Task Create()
        {
            var dto = new Dtos.User.UserDto
            {
                Age = 18,
                Name = "axity 2",
                Status = true,
                SurName = "user",
            };

            var result = await this.modelService.Create(dto);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Age, Is.EqualTo(18));
            Assert.That(result.Status, Is.True);
        }

        /// <summary>
        /// Create test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "create  user")]
        [Category("users")]
        public async Task Upadte()
        {
            var dto = new Dtos.User.UserDto
            {
                Age = 18,
                Name = "axity 2",
                Status = true,
                Id = 1,
                SurName = "user",
            };

            var result = await this.modelService.Update(dto);
            Assert.That(result.Name, Is.EqualTo(dto.Name));
            Assert.That(result.Age, Is.EqualTo(18));
            Assert.That(result.Status, Is.True);
        }

        /// <summary>
        /// get by id test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "get by id  user")]
        [Category("users")]
        public async Task GetById()
        {
            var result = await this.modelService.GetById(1);
            Assert.That(result.Name, Is.EqualTo("axity"));
            Assert.That(result.Age, Is.EqualTo(18));
            Assert.That(result.Status, Is.True);
        }

        /// <summary>
        /// delete test.
        /// </summary>
        [Test(Author = "axitymx", Description = "get by id  user")]
        [Category("users")]
        public void Delete()
        {
            Assert.DoesNotThrowAsync(async () => await this.modelService.Delete(2));
        }
    }
}
