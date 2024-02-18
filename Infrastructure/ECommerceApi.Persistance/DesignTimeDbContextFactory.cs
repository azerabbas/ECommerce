using ECommerceApi.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceApiDbContext>
    {
        public ECommerceApiDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ECommerceApiDbContext> dbContextBuilder = new();
            dbContextBuilder.UseSqlServer(Configurations.ConnectionString);
            return new(dbContextBuilder.Options);
        }
    }
}
