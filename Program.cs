
using My_MVCApp;
using My_MVCApp.Models;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
//code to read the connection string from 
builder.Services.AddDbContext<pizzadbContext > (o => o.UseSqlServer(builder.Configuration.GetConnectionString("cc")));
//create builder: contains alogic to create/build kestral webserver
//logic to call appsettings/launchsettings.json
//No features is added yet
// 1) Services

//injector
//singleton: all cient will share same object
//transient: every request new object is created
//scoped: single object  is assigned for each client

builder.Services.AddSingleton<IMath, MyClass>();
builder.Services.AddControllersWithViews();

// Required for Session
//this application uses controllers with returns json, string and content
builder.Services.AddDistributedMemoryCache(); // in-memory cache for session store
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // adjust as needed
    options.Cookie.HttpOnly = true;                 // security best practice
    options.Cookie.IsEssential = true;              // needed if you use cookie consent
});

var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/India/Error");
//    app.UseHsts();
//}
// 2) Pipeline

//app.UseStateCodePages();
//app.UseStateCodePagesWithRedirects("/India/Show");
//app.UseStateCodePagesWithReExecute("/India/Show");
//app.UseHttpsRedirection();


app.UseHttpsRedirection();

// Serve wwwroot (images, css, js)
app.UseStaticFiles();

app.UseRouting();

// Enable Session BEFORE Authorization/Endpoints
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MyPizza}/{action=Menu}/{id?}");

app.Run();