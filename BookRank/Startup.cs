using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using BookRank.Libs.Mappers;
using BookRank.Libs.Repositories;
using BookRank.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BookRank
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAWSService<IAmazonDynamoDB>(new AWSOptions
            {
                Region = RegionEndpoint.EUWest2
            });

            services.AddSingleton<ISetupService, SetupService>();
            services.AddSingleton<IBookRankService, BookRankService>();
            services.AddSingleton<IBookRankRepository, BookRankRepository>();
            services.AddSingleton<IMapper, Mapper>();
        }

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
