using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IStreamerRepository streamerRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> logger;

        public UpdateStreamerCommandHandler(IStreamerRepository streamerRepository, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
        {
            this.streamerRepository = streamerRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamerToUpdate = await streamerRepository.GetByIdAsync(request.Id);

            if (streamerToUpdate == null)
            {
                logger.LogError($"No se encontro el streamer con el id, { request.Id }");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            // actualiza todos los datos

            mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));

            await streamerRepository.UpdateAsync(streamerToUpdate);

            logger.LogInformation($"Streamer actualizado exitosamente, { request.Id }");
            return Unit.Value;
        }
    }
}
