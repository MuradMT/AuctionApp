using AuctionService.Data;
using AuctionService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AuctionService.Controllers;
[ApiController]
[Route("api/auctions")]
public class AuctionController(AuctionDbContext _context, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
    {
        var auctions = await _context.Auctions
        .Include(x => x.Item)
        .OrderBy(x => x.Item.Make)
        .ToListAsync();
        return _mapper.Map<List<AuctionDto>>(auctions);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
    {
        var auction = await _context.Auctions
        .Include(x => x.Item)
        .FirstOrDefaultAsync(x => x.Id == id);
        if (auction is null) return NotFound();
        return _mapper.Map<AuctionDto>(auction);
    }
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto dto)
    {
        var auction = _mapper.Map<Auction>(dto);
        _context.Auctions.Add(auction);
        if(await ChangeTracker()){
            return BadRequest("Could not save changes to db");
        }
        return CreatedAtAction(nameof(GetAuctionById), new { auction.Id }, _mapper.Map<AuctionDto>(auction));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto dto)
    {
        var auction = await _context.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);
        if (auction is null) return NotFound();
        auction.Item.Make = dto.Make ?? auction.Item.Make;
        auction.Item.Model = dto.Model ?? auction.Item.Model;
        auction.Item.Color = dto.Color ?? auction.Item.Color;
        auction.Item.Mileage = dto.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = dto.Year ?? auction.Item.Year;
        if(await ChangeTracker()){
            return BadRequest("Could not save changes to db");
        }
        return Ok();

    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions.FindAsync(id);
        if (auction == null) return NotFound();
        _context.Auctions.Remove(auction);
        if(await ChangeTracker()){
            return BadRequest("Could not save changes to db");
        }
        return Ok();
    }
    async Task<bool> ChangeTracker(){
         var res= await _context.SaveChangesAsync() > 0;
         return res;
    }
}
