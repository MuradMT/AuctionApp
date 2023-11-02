


namespace SearchService.Controller;
[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParamsDto dto)
    {
        try
        {
            var query = DB.PagedSearch<Item, Item>();


            if (!string.IsNullOrEmpty(dto.SearchTerm))
            {
                query.Match(Search.Full, dto.SearchTerm).SortByTextScore();
            }
            //Ordering
            query = dto.OrderBy switch
            {
                "make" => query.Sort(x => x.Ascending(a => a.Make)).Sort(x=>x.Ascending(a=>a.Model)),
                "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
            };
            //Filtering
            query = dto.FilterBy switch
            {
                "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
                _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
            };
            if (!string.IsNullOrEmpty(dto.Seller))
            {
                query.Match(x => x.Seller == dto.Seller);
            }
            if (!string.IsNullOrEmpty(dto.Winner))
            {
                query.Match(x => x.Winner == dto.Winner);
            }
            //Pagination
            query.PageNumber(dto.PageNumber);
            query.PageSize(dto.PageSize);
            var result = await query.ExecuteAsync();

            return Ok(
                new
                {
                    results = result.Results,
                    pageCount = result.PageCount,
                    totalCount = result.TotalCount
                }
            );
        }
        catch (Exception ex)
        {
            return BadRequest($"{Message.ExceptionThrown}{ex.ToString}");
        }

    }
}
