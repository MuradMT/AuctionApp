
namespace AuctionService.Consumers;

public class AuctionFinishedConsumer(AuctionDbContext _context) : IConsumer<AuctionFinished>
{
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        Console.WriteLine("Consuming auction finished");
        var auction=await _context.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));
        if(context.Message.ItemSold){
            auction.Winner=context.Message.Winner;
            auction.SoldAmount=context.Message.Amount;
        }
        auction.Status=auction.SoldAmount>auction.ReservePrice
        ?Status.Finished:Status.ReserveNotMet;
        await _context.SaveChangesAsync();
    }
}
