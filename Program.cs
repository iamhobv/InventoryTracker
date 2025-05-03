
using System.Diagnostics;
using System.Text;
using AutoMapper;
using Hangfire;
using InventoryTracker.Data;
using InventoryTracker.HangfireService;
using InventoryTracker.Middlewares;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InventoryTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("CS"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information);
            });

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<DataBaseContext>();


            builder.Services.AddHangfire(h => h.UseSqlServerStorage(builder.Configuration.GetConnectionString("CS")));

            builder.Services.AddHangfireServer();


            builder.Services.AddScoped<IGeneralRepo<Category>, GeneralRepo<Category>>();
            builder.Services.AddScoped<IGeneralRepo<ArchivedInventoryTransaction>, GeneralRepo<ArchivedInventoryTransaction>>();
            builder.Services.AddScoped<IGeneralRepo<InventoryTransaction>, GeneralRepo<InventoryTransaction>>();
            builder.Services.AddScoped<IGeneralRepo<Notification>, GeneralRepo<Notification>>();
            builder.Services.AddScoped<IGeneralRepo<Product>, GeneralRepo<Product>>();
            builder.Services.AddScoped<IGeneralRepo<ProductWarehouse>, GeneralRepo<ProductWarehouse>>();
            builder.Services.AddScoped<ITransactionHistoryReportBuilder, TransactionHistoryReportBuilder>();
            builder.Services.AddScoped<IGeneralRepo<Report>, GeneralRepo<Report>>();
            builder.Services.AddScoped<IGeneralRepo<Warehouse>, GeneralRepo<Warehouse>>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new()
                   {
                       ValidateIssuer = false,
                       ValidIssuer = builder.Configuration["JWT:Iss"],
                       ValidateAudience = false,
                       ValidAudience = builder.Configuration["JWT:Aud"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                   };
               });


            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddMediatR(opts =>
                opts.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //builder.Services.AddScoped<GlobalErrorHandlerMiddleware>();

            var app = builder.Build();

            //app.UseMiddleware<GlobalErrorHandlerMiddleware>();

            MapperServices.Mapper = app.Services.GetService<IMapper>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.UseHangfireDashboard("/hangfireDashboard");

            //RecurringJob.AddOrUpdate("TestJob",
            //    () => Console.WriteLine("hello testing hangfire RecurringJob"), Cron.Minutely());

            RecurringJob.AddOrUpdate<CheckProductThresholdHangfireServiceHandler>(
                "CehckProductThreshold",
                (x) => x.CheckProductThresholdAsync(),
                Cron.Minutely());

            RecurringJob.AddOrUpdate<CheckProductYearArchivedTransactionHangfireService>(
                "ArchiveTransactionThatMoreThanOneyear",
                (x) => x.CheckProductYearAsync(),
                Cron.Minutely());



            app.MapControllers();

            app.Run();
        }
    }
}
