// <summary>
// <copyright file="DatabaseContext.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Entities.Context
{
    using Axity.Users.Entities.Model.User;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Databasecontext.
    /// </summary>
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        /// <param name="options">options.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        /// <inheritdoc/>
        public DbSet<UserModel> Users { get; set; }

        /// <summary>
        /// Configuration on model creating.
        /// </summary>
        /// <param name="modelBuilder">Object ModelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
