// <summary>
// <copyright file="UserFacadeTest.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Test.Facade.User
{
    using Axity.Users.Dtos.Common;
    using Axity.Users.Dtos.User;
    using Axity.Users.Facade.User;
    using Axity.Users.Facade.User.Impl;
    using Axity.Users.Services.User;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// User Facade Test.
    /// </summary>
    [TestFixture]
    public class UserFacadeTest
    {
        private readonly UserDto create = new UserDto
        {
            Age = 18,
            Name = "axity 2",
            Status = true,
            SurName = "user",
        };

        private readonly UserDto update = new UserDto
        {
            Age = 18,
            Name = "axity 2",
            Status = true,
            Id = 1,
            SurName = "user",
        };

        private Mock<IUserService> mockService;
        private IUserFacade modelFacade;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mockService = new Mock<IUserService>();
            this.mockService.Setup(x => x.Create(It.IsAny<UserDto>())).ReturnsAsync(this.create);
            this.mockService.Setup(x => x.Update(It.IsAny<UserDto>())).ReturnsAsync(this.update);
            this.mockService.Setup(x => x.Paginator(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PaginatorServiceDto<UserDto>
            {
                Rows = new List<UserDto>
                {
                    new UserDto
                    {
                        Age = 18,
                        Name = "axity 2",
                        Status = true,
                        SurName = "user",
                    },
                },
                TotalRow = 1,
            });
            this.mockService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new UserDto
            {
                Age = 18,
                Name = "axity 2",
                Status = true,
                SurName = "user",
            });
            this.mockService.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();
            this.modelFacade = new UserFacade(this.mockService.Object);
        }

        /// <summary>
        /// Paginator test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "get paginator user")]
        [Category("users")]
        public async Task PaginatorTest()
        {
            var result = await this.modelFacade.Paginator(10, 1);
            Assert.That(result.Rows[0].Name, Is.EqualTo("axity 2"));
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
            var result = await this.modelFacade.Create(this.create);
            Assert.That(result.Data.Name, Is.EqualTo(this.create.Name));
            Assert.That(result.Data.Age, Is.EqualTo(this.create.Age));
            Assert.That(result.Data.Status, Is.True);
        }

        /// <summary>
        /// Create test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "create  user")]
        [Category("users")]
        public async Task Upadte()
        {
            var result = await this.modelFacade.Update(this.update);
            Assert.That(result.Data.Name, Is.EqualTo(this.update.Name));
            Assert.That(result.Data.Age, Is.EqualTo(this.update.Age));
            Assert.That(result.Data.Status, Is.True);
        }

        /// <summary>
        /// get by id test.
        /// </summary>
        /// <returns>Task.</returns>
        [Test(Author = "axitymx", Description = "get by id  user")]
        [Category("users")]
        public async Task GetById()
        {
            var result = await this.modelFacade.GetById(1);
            Assert.That(result.Data.Name, Is.EqualTo("axity 2"));
            Assert.That(result.Data.Age, Is.EqualTo(18));
            Assert.That(result.Data.Status, Is.True);
        }

        /// <summary>
        /// delete test.
        /// </summary>
        [Test(Author = "axitymx", Description = "get by id  user")]
        [Category("users")]
        public void Delete()
        {
            Assert.DoesNotThrowAsync(async () => await this.modelFacade.Delete(2));
        }
    }
}
