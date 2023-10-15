


using SearchService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x=>{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search",false));
    x.UsingRabbitMq((context,config)=>{
        config.ReceiveEndpoint("search-auction-created",e=>{
             e.UseMessageRetry(r=>r.Interval(5,5));
             e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
        config.ConfigureEndpoints(context);
    });
}
);
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
