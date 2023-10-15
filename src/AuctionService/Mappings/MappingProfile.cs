

using AuctionService.DTOs;
using Contracts;

namespace AuctionService.Mappings;

public class MappingProfile:Profile
{
  public MappingProfile()
  {
    CreateMap<Auction,AuctionDto>().IncludeMembers(x=>x.Item);
    CreateMap<Item,AuctionDto>();
    CreateMap<CreateAuctionDto,Auction>().ForMember(x=>x.Item,y=>y.MapFrom(s=>s));
    CreateMap<CreateAuctionDto,Item>();
    CreateMap<AuctionDto,AuctionCreated>();
  }
}
