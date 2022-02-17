using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
    {
        //private readonly IStreamerRepository streamerRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly ILogger<DeleteStreamerCommandHandler> logger;

        public DeleteStreamerCommandHandler(/*IStreamerRepository streamerRepository, */IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger, IUnitOfWork uow)
        {
            //this.streamerRepository = streamerRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.uow = uow;
        }

        public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
        {
            //var streamerToDelete = await streamerRepository.GetByIdAsync(request.Id);
            var streamerToDelete = await uow.StreamerRepository.GetByIdAsync(request.Id);

            if (streamerToDelete == null)
            {
                logger.LogError($"{ request.Id }, no existe");
                throw new NotFoundException(nameof(Streamer), request.Id);
            }

            //await streamerRepository.DeleteAsync(streamerToDelete);
            uow.StreamerRepository.DeleteEntity(streamerToDelete);
            await uow.Complete();

            logger.LogInformation($"El { request.Id } fue eliminado con exito");

            return Unit.Value;
        }
    }
}
