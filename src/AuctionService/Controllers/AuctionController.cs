

using Microsoft.AspNetCore.Authorization;

namespace AuctionService.Controllers;
[ApiController]
[Route("api/auctions")]
public class AuctionController
(AuctionDbContext _context, IMapper _mapper, IPublishEndpoint publishEndpoint) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions(string date)
    {
        try
        {
            var query = _context.Auctions.OrderBy(x => x.Item.Make).AsQueryable();
            if (!string.IsNullOrEmpty(date))
            {
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }


    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        try
        {
            var auction = await _context.Auctions
                    .Include(x => x.Item)
                    .FirstOrDefaultAsync(x => x.Id == id);
            if (auction is null) return NotFound();
            return _mapper.Map<AuctionDto>(auction);
        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto dto)
    {
        try
        {
            var auction = _mapper.Map<Auction>(dto);
            auction.Seller=User.Identity.Name;
            _context.Auctions.Add(auction);
            var newAuction = _mapper.Map<AuctionDto>(auction);
            await publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));
            if (await ChangeTracker())
            {
                return BadRequest(Message.DbNotSaved);
            }
            return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, newAuction);
        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto dto)
    {
        try
        {
            var auction = await _context.Auctions
                            .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
            if (auction is null) return NotFound();
            if (auction.Seller!=User.Identity.Name) return Forbid();
            auction.Item.Make = dto.Make ?? auction.Item.Make;
            auction.Item.Model = dto.Model ?? auction.Item.Model;
            auction.Item.Color = dto.Color ?? auction.Item.Color;
            auction.Item.Mileage = dto.Mileage ?? auction.Item.Mileage;
            auction.Item.Year = dto.Year ?? auction.Item.Year;
            await publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));
            if (await ChangeTracker())
            {
                return BadRequest(Message.DbNotSaved);
            }
            return Ok();

        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }


    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        try
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null) return NotFound();
            if(auction.Seller!=User.Identity.Name) return Forbid();
            _context.Auctions.Remove(auction);
            await publishEndpoint.Publish<AuctionDeleted>(new{Id=auction.Id.ToString()});
            if (await ChangeTracker())
            {
                return BadRequest(Message.DbNotSaved);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }

    }
    async Task<bool> ChangeTracker()
    {
        var res = await _context.SaveChangesAsync() > 0;
        return res;
    }
}
