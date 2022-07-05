using ContactMicroservice.DataAccess.Abstract;
using ContactMicroservice.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ContactMicroservice
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });

            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddDbContext<ContactMicroserviceContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgreSqlConnectionString")));

            var context = services.BuildServiceProvider()
                       .GetService<ContactMicroserviceContext>(); 

            try
            {
                context.Database.Migrate();
            }
            catch (Exception)
            {

            }
            if (context.Persons.Count() == 0)
            {
                var PersonId = Guid.NewGuid();
                var ContactId = Guid.NewGuid();

                context.Persons.Add(new Entities.Concrete.Person { Name = "emre", Surname = "gazel", Company = "mycompany", PersonId = PersonId });
                context.Contacts.Add(new Entities.Concrete.Contact { PersonId = PersonId, ContactId = ContactId, Email = "my@mail.com", Phone = "555 55 55", Location = "Antalya" });

                context.SaveChanges();
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactMicroservice");
            });
        }
    }
}
