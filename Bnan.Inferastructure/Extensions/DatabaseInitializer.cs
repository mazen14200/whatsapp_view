
using Bnan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure
{
    public static class DatabaseInitializer
    {
        public static void Initialize(BnanKSAContext context, string seedDataSql)
        {
            if (context.Database.EnsureCreated())
            {
                // Database was created, seed data
                context.Database.ExecuteSqlRaw(seedDataSql);
            }
        }
    }
}
