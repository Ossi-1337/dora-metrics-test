using Shared;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { Service = "Customer", Status = "OK", Shared = SharedHelper.Ping() });
// test 
app.Run();
