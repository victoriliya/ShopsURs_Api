using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Data;
using ShopsRUs.Services.Implementations;
using ShopsRUs.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Testing
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
            services.AddControllers().AddApplicationPart(Assembly.Load("ShopsRUs")).AddControllersAsServices();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("MyTestDB" + Guid.NewGuid());
            });

            /*            services.AddDbContext<ApplicationDbContext>(cfg =>
                                cfg.UseSqlite(Configuration.GetConnectionString("TestShopsRUs.db"))
                         );*/

            services.AddScoped<ICostumerRepository, CostumerRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IDiscountsRepository, DiscountsRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
  


            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



/*            Seeder.Seed(app).Wait();
            Seeder.GenerateInvoice(app).Wait();*/

        }
    }
}
