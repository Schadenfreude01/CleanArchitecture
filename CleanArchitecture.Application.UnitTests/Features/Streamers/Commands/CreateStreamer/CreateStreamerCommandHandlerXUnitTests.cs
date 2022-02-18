using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandHandlerXUnitTests
    {
        private readonly Mock<UnitOfWork> unitOfWork;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<CreateStreamerCommandHandler>> logger;

        public CreateStreamerCommandHandlerXUnitTests()
        {
            unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            mapper = mapperConfig.CreateMapper();

            logger = new Mock<ILogger<CreateStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber()
        {
            var streamerInput = new CreateStreamerCommand
            {
                Nombre = "Streamer Test",
                Url = "https://test.test"
            };

            var handler = new CreateStreamerCommandHandler(unitOfWork.Object, mapper, logger.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }
    }
}
