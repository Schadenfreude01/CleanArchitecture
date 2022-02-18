using AutoMapper;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using Xunit;

namespace CleanArchitecture.Application.UnitTests.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandlerXUnitTests
    {
        private readonly Mock<UnitOfWork> unitOfWork;
        private readonly IMapper mapper;
        private readonly Mock<ILogger<DeleteStreamerCommandHandler>> logger;

        public DeleteStreamerCommandHandlerXUnitTests()
        {
            unitOfWork = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile<MappingProfile>();
            });
            mapper = mapperConfig.CreateMapper();

            logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

            MockStreamerRepository.AddDataStreamerRepository(unitOfWork.Object.StreamerDbContext);
        }

        [Fact]
        public async Task DeleteStreamerCommand_InputStreamer_ReturnsUnit()
        {
            var streamerInput = new DeleteStreamerCommand
            {
                Id = 9999
            };

            var handler = new DeleteStreamerCommandHandler(mapper, logger.Object, unitOfWork.Object);

            var result = await handler.Handle(streamerInput, CancellationToken.None);

            result.ShouldBeOfType<Unit>();
        }
    }
}
