﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using User.Service.CustomFilters;
using User.Service.CustomModelBinders;
using User.Service.Entities;
using User.Service.Repositories;
using User.Service.Services;

namespace User.Service
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
            if (Configuration["DATA_STORAGE"] == "remote")
            {
                services.AddDbContext<UserDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UserConnection")));
                services.AddScoped<IUserRepository, RemoteUserRepository>();
            }
            else if (Configuration["DATA_STORAGE"] == "local")
            {
                services.AddSingleton<IUserRepository, LocalUserRepository>();
            }
            else
            {
                throw new Exception("No one instance for IUserRepository was instantiated");
            }
            
            services.AddScoped<IEntityService<UserEntity>, UserService>();

            services.AddMvc(options => {
                options.Filters.Add(new CustomJSONExceptionFilter());
                options.ModelBinderProviders.Insert(0, new ValidateModelBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
