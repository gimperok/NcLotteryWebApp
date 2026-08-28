using NcLotteryWebApp.Components;
using NcLotteryWebApp.Services.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
// Enable support for classic REST API controllers 
builder.Services.AddControllers();

// Configuring Swagger generation for auto-documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new ()
    {
        Title = "NC Lottery Web API",
        Version = "v1",
        Description = "Public API for generating random numbers for North Carolina lotteries (Powerball, Mega Millions)."
    });
});

// Register fabric in DI
builder.Services.AddScoped<LotteryFactory>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NC Lottery API v1");
    c.RoutePrefix = "swagger";    // for adress '/swagger'
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
