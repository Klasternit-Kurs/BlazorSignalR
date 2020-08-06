using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace BlazorSignalR.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			using (DB baza = new DB())
			{
				baza.Database.EnsureCreated();

				OtMA a = new OtMA();
				OtMB b1 = new OtMB();
				OtMB b2 = new OtMB();

				a.lista.Add(b1);
				a.lista.Add(b2);

				baza.OtMAs.Add(a);
				baza.OtMBs.Add(b1);
				baza.OtMBs.Add(b2);

				baza.SaveChanges();

				var nesto = baza.OtMBs.First();
				Console.WriteLine(nesto.A);

				OtOA ooa = new OtOA();
				OtOB oob = new OtOB();
				ooa.B = oob;

				baza.OtOAs.Add(ooa);
				baza.OtOBs.Add(oob);

				baza.SaveChanges();

				MtMA a1mm = new MtMA();
				MtMA a2mm = new MtMA();
				MtMB b1mm = new MtMB();
				MtMB b2mm = new MtMB();

				baza.MtMA_Bs.Add(new MtMA_B(a1mm, b1mm));
				baza.MtMA_Bs.Add(new MtMA_B(a1mm, b2mm));
				baza.MtMA_Bs.Add(new MtMA_B(a2mm, b1mm));
				baza.MtMA_Bs.Add(new MtMA_B(a2mm, b2mm));
				baza.MtMAs.Add(a1mm);
				baza.MtMAs.Add(a2mm);
				baza.MtMBs.Add(b1mm);
				baza.MtMBs.Add(b2mm);

				baza.SaveChanges();
			}
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddRazorPages();
			services.AddSignalR();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<Hab>("th");
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
