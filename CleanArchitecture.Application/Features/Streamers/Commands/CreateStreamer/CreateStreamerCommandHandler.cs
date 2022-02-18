using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        //private readonly IStreamerRepository streamerRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        //private readonly IEmailService emailService;
        private readonly ILogger<CreateStreamerCommandHandler> logger;

        public CreateStreamerCommandHandler(/* IStreamerRepository streamerRepository, */ IUnitOfWork uow, IMapper mapper, /*IEmailService emailService,*/ ILogger<CreateStreamerCommandHandler> logger)
        {
            //this.streamerRepository = streamerRepository;
            this.uow = uow;
            this.mapper = mapper;
            //this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamer = mapper.Map<Streamer>(request);

            //var newStreamer = await streamerRepository.AddAsync(streamer);
            uow.StreamerRepository.AddEntity(streamer);

            var result = await uow.Complete();

            if (result <= 0) throw new Exception($"No se pudo insertar el record de streamer");

            //logger.LogInformation($"Streamer { newStreamer.Id }, creado exitosamente");
            logger.LogInformation($"Streamer { streamer.Id }, creado exitosamente");

            //await SendEmail(newStreamer);

            //return newStreamer.Id;
            return streamer.Id;
        }

        //private async Task SendEmail(Streamer streamer)
        //{
        //    var email = new Email
        //    {
        //        To = "fabianalexis06@gmail.com",
        //        Body = "El streamer fue ingresado correctamente",
        //        Subject = "Mensaje de alerta"
        //    };

        //    try
        //    {
        //        await emailService.SendEmail(email);
        //    }
        //    catch (Exception)
        //    {
        //        logger.LogError($"Error al enviar el email de { streamer.Id }");
        //        throw;
        //    }
        //}
    }
}
