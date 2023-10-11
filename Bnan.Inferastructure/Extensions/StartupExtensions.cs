
using Bnan.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace Bnan.Inferastructure
{
    public static class StartupExtensions
    {
        public static void UseDatabaseInitializer(this IApplicationBuilder app, string seedDataSql)
        {
            using (var Scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = Scope.ServiceProvider.GetRequiredService<BnanKSAContext>();
                DatabaseInitializer.Initialize(dbContext, seedDataSql);
            }
        }
    }
}
