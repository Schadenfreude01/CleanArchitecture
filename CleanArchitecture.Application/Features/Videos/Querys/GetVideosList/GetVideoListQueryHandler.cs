using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<GetVideoListQueryResult>>
    {
        //private readonly IVideoRepository videoRepository;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GetVideoListQueryHandler(/*IVideoRepository videoRepository, */IMapper mapper, IUnitOfWork uow)
        {
            //this.videoRepository = videoRepository;
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<List<GetVideoListQueryResult>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            //var videoList = await videoRepository.GetByUsername(request.UserName);
            var videoList = await uow.VideoRepository.GetByUsername(request.UserName);

            return mapper.Map<List<GetVideoListQueryResult>>(videoList);
        }
    }
}
