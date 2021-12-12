using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAutopark.BusinessLayer.Extensions;
using WebAutopark.BusinessLayer.MappingProfiles;
using WebAutopark.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Configure services

/*builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

builder.Configuration.AddJsonFile("appsettings.json", false, true);*/

builder.Services.AddControllersWithViews();

// added my repositories, services, context, automapper and identity configuration
builder.Services.AddCustomSolutionConfigs(builder.Configuration);

builder.Services.AddAutoMapper(cfg => cfg.AddProfiles(new Profile[]
{
    new ConfigureBusinessLayerProfile(),
    new ConfigureViewModelProfile()
}));

// Configure application and environment 
using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

app.Run();