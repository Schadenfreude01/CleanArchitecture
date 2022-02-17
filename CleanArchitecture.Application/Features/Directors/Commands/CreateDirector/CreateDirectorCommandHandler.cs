using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly ILogger<CreateDirectorCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public CreateDirectorCommandHandler(ILogger<CreateDirectorCommandHandler> logger, IMapper mapper, IUnitOfWork uow)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var directorEntity = mapper.Map<Director>(request);
            
            uow.Repository<Director>().AddEntity(directorEntity);
            var result = await uow.Complete();

            if(result <= 0)
            {
                logger.LogError("No se inserto el record del director");
                throw new Exception("No se pudo insertar el record del director");
            }

            return directorEntity.Id;
        }
    }
}
