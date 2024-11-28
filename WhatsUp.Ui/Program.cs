using WhatsUp.Core.Interfaces;
using WhatsUp.Core.Models;
using WhatsUp.Inferastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using WhatsUp.Core;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Net;
using System.Text;
using System.Net.WebSockets;
using Microsoft.AspNetCore.SignalR;
using WhatsUp.Ui.Hubs;

namespace WhatsUp3.Ui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
            builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            //builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // To make Cors allow any http request Not only https
            builder.Services.AddCors();

            builder.Services.AddLocalization(option=>option.ResourcesPath= "Resources");
            builder.Services.AddControllersWithViews()
              .AddDataAnnotationsLocalization();
            builder.Services.AddDbContext<WhatsUpContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
                /* options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);*/
            });


            builder.Services.AddAuthorization();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };
                foreach (var culture in supportedCultures)
                {
                    // Customize the number format for each supported culture
                    culture.NumberFormat.CurrencyDecimalDigits = 2;
                    culture.NumberFormat.CurrencyDecimalSeparator = ".";
                    culture.NumberFormat.CurrencyGroupSeparator = ",";
                }
                options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
                CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IRenterIdType, RenterIdType>();

            //builder.WebHost.UseUrls("http://localhost:6969");

            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // To make Cors allow any http request Not only https
            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            var supportedCultures = new[] { "en-US", "ar-EG" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);


            app.MapControllerRoute(
                name: "default",
                pattern: "{culture=en-US}/{controller=WhatsUp}/{action=Company_settings}/{id?}");

            //////////////////
            //////////////////
            //////////////////

            //app.UseStaticFiles();
            app.UseEndpoints(webSocket =>{
                webSocket.MapHub<MessageHub>("/messages");
            });
            app.UseMvc();



            /////////////////
            /////////////////
            //app.UseWebSockets();
            //var connections = new List<WebSocket>();

            //app.Map("/ws", async context =>
            //{
            //    if (context.WebSockets.IsWebSocketRequest)
            //    {
            //        var curName = context.Request.Query["name"];

            //        using var ws = await context.WebSockets.AcceptWebSocketAsync();

            //        connections.Add(ws);

            //        await Broadcast($"{curName} joined the room");
            //        await Broadcast($"{connections.Count} users connected");
            //        await ReceiveMessage(ws,
            //            async (result, buffer) =>
            //            {
            //                if (result.MessageType == WebSocketMessageType.Text)
            //                {
            //                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            //                    await Broadcast(curName + ": " + message);
            //                }
            //                else if (result.MessageType == WebSocketMessageType.Close || ws.State == WebSocketState.Aborted)
            //                {
            //                    connections.Remove(ws);
            //                    await Broadcast($"{curName} left the room");
            //                    await Broadcast($"{connections.Count} users connected");
            //                    await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            //                }
            //            });
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    }
            //});
            //async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
            //{
            //    var buffer = new byte[1024 * 4];
            //    while (socket.State == WebSocketState.Open)
            //    {
            //        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //        handleMessage(result, buffer);
            //    }
            //}

            //async Task Broadcast(string message)
            //{
            //    var bytes = Encoding.UTF8.GetBytes(message);
            //    foreach (var socket in connections)
            //    {
            //        if (socket.State == WebSocketState.Open)
            //        {
            //            var arraySegment = new ArraySegment<byte>(bytes, 0, bytes.Length);
            //            await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            //        }
            //    }
            //}

            //app.RunAsync();
            app.Run();
        }
    }
}