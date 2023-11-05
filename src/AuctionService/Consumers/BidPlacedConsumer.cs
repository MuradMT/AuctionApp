﻿
namespace AuctionService.Consumers;

public class BidPlacedConsumer(AuctionDbContext _context) : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
         Console.WriteLine("Consuming bid placed");
         var auction=await _context.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));
         if(auction.CurrentHighBid==null ||context.Message.BidStatus.Contains("Accepted")
         &&context.Message.Amount>auction.CurrentHighBid){
            auction.CurrentHighBid=context.Message.Amount;
            await _context.SaveChangesAsync();
         }
    }
}
