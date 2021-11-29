using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebAutopark.BusinessLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services

/*builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());

builder.Configuration.AddJsonFile("appsettings.json", false, true);*/


// added my repositories and services      
builder.Services.AddCustomSolutionConfigs(builder.Configuration);

builder.Services.AddControllersWithViews();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

app.Run();