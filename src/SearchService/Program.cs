
<<<<<<< HEAD
using System.Net;
using Polly;
using Polly.Extensions.Http;
=======
>>>>>>> 142a1e49b0368947de8b5e3f565513bef50602fd

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();
app.Lifetime.ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex);
    }

});

await DB.InitAsync("SearchDb", MongoClientSettings.
FromConnectionString(builder.Configuration.GetConnectionString("MongoDbConnection")));

await DB.Index<Item>()
.Key(x=>x.Make,KeyType.Text)
.Key(x=>x.Model,KeyType.Text)
.Key(x=>x.Color,KeyType.Text)
.CreateAsync();


app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy() =>
HttpPolicyExtensions.HandleTransientHttpError().
OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound).WaitAndRetryForeverAsync(_ =>
TimeSpan.FromSeconds(3));
;
