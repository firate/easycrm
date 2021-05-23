using AutoMapper;
using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Business.Managers.Concrete;
using EasyCRM.Data.EF;
using EasyCRM.Entity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace EasyCRM.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DataContext>(options=>
				options.UseSqlServer(Configuration.GetConnectionString("LocalDBConnection"))
			);

			IdentityBuilder builder = services.AddIdentityCore<User>(opt=> 
			{
				opt.Password.RequireDigit = false;
				opt.Password.RequiredLength = 4;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
			});

			builder = new IdentityBuilder(builder.UserType,typeof(Role), builder.Services);
			builder.AddEntityFrameworkStores<DataContext>();
			builder.AddRoleValidator<RoleValidator<Role>>();
			builder.AddRoleManager<RoleManager<Role>>();
			builder.AddSignInManager<SignInManager<User>>();

			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<DataContext>();

			services.AddTransient<IAccountManager, AccountManager>();
			services.AddTransient<IContactManager, ContactManager>();
			services.AddTransient<IAddressManager, AddressManager>();
			services.AddTransient<ICommunicationInfoManager,CommunicationInfoManager>();
			services.AddTransient<IProductManager, ProductManager>();
			services.AddTransient<ISalesOrderManager, SalesOrderManager>();

			services.AddControllers().AddNewtonsoftJson(options =>
						options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);

			//services.AddControllers().AddFluentValidation(fv =>
			//{
			//    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
			//});

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			services.AddAutoMapper(typeof(Startup));

			services.AddSwaggerDocument(config => {
				config.PostProcess = document =>
				{
					document.Info.Version = "V1";
					document.Info.Title = "EasyCRM";
					document.Info.Description = "Easy way to Management CRM Operations";
					document.Info.Contact = new NSwag.OpenApiContact
					{
						Name = "Fırat Ergül",
						Email = "",
						Url = "https://twitter.com/firatergul0"
					};
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseOpenApi();
			app.UseSwaggerUi3();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


			//app.UseSpa(spa =>
			//{
			//	spa.Options.SourcePath = "ClientApp";

			//	if (env.IsDevelopment())
			//	{
			//		spa.UseReactDevelopmentServer(npmScript: "start");
			//	}
			//});
		}
	}
}
