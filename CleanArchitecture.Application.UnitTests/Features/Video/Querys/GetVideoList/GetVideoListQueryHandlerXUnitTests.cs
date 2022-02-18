using AutoMapper;
using CleanArchitecture.Application.Features.Videos.Querys.GetVideosList;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Video.Querys.GetVideoList
{
    public class GetVideoListQueryHandlerXUnitTests
    {
        private readonly IMapper mapper;
        private readonly Mock<UnitOfWork> unitOfWork;

        public GetVideoListQueryHandlerXUnitTests()
        {
            unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            mapper = mapperConfig.CreateMapper();

            MockVideoRepository.AddDataVideoRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task GetVideoListQueryTest()
        {
            var handler = new GetVideoListQueryHandler(mapper, unitOfWork.Object);
            var request = new GetVideoListQuery("fabian.monzon");
            var result = await handler.Handle(request, CancellationToken.None );

            result.ShouldBeOfType<List<VideosVM>>();

            result.Count.ShouldBe(1);
;        }
    }
}
