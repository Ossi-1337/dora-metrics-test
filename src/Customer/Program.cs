using Shared;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new { Service = "Customer", Status = "OK", Shared = SharedHelper.Ping() });
// test 67
app.Run();
