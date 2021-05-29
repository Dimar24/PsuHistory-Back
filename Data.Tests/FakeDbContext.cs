using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.EF.SQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tests
{
	class FakeDbContext : DbContextBase
	{
		private FakeDbContext(DbContextOptions<DbContextBase> options) : base(options)
		{

		}

		public static FakeDbContext GetInstance()
		{
			var options = new DbContextOptionsBuilder<DbContextBase>()
				.UseInMemoryDatabase(databaseName: $"MemoryDb#{Guid.NewGuid()}")
				.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
				.Options;
			return new FakeDbContext(options);
		}
	}

}
