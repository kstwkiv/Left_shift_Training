var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
//app.UseStaticFiles();
/*app.Use(async (context,next) =>
{ 
    await context.Response.WriteAsync("what is your Name?\n");
    await next.Invoke();
    await context.Response.WriteAsync("MiddleWare 2\n");
    });
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("In which city you stay?\n");
    await next.Invoke();
    await context.Response.WriteAsync("MiddleWare 3\n");
});
app.Run(async context =>
{
    await context.Response.WriteAsync("What is your Purpose?\n");
});*/

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}")
    .WithStaticAssets();




app.Run();
