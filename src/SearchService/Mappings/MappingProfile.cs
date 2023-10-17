using AutoMapper;

namespace SearchService.Mappings;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<AuctionCreated, Item>();
      CreateMap<AuctionUpdated, Item>();
   }
}
