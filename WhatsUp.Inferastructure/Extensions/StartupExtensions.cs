
using WhatsUp.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace WhatsUp.Inferastructure
{
    public static class StartupExtensions
    {
        public static void UseDatabaseInitializer(this IApplicationBuilder app, string seedDataSql)
        {
            using (var Scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = Scope.ServiceProvider.GetRequiredService<WhatsUpContext>();
                DatabaseInitializer.Initialize(dbContext, seedDataSql);
            }
        }
    }
}
