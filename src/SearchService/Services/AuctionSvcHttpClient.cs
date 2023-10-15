namespace SearchService.Services;

public class AuctionSvcHttpClient(HttpClient client, IConfiguration config)
{
    public async Task<List<Item>> GetItemsForSearchDb()
    {
        var lastUpdated = await DB.Find<Item, string>()
        .Sort(x => x.Descending(a => a.UpdatedAt))
        .Project(x => x.UpdatedAt.ToString())
        .ExecuteFirstAsync();
        return await client.GetFromJsonAsync<List<Item>>
        ($"{config["AuctionServiceUrl"]}/api/auctions?date={lastUpdated}");
    }
}
