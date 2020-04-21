using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZC.Customer.Repository.Data
{
    class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer("Server=192.168.1.40;Database=ZC.Customer.Test;User ID=wikitec_dcc;Password=3bRiRBYKyccbzOFJC7Xi;");
            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
