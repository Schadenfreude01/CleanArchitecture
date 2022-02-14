using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideosVM>>
    {
        private readonly IVideoRepository videoRepository;
        private readonly IMapper mapper;

        public GetVideoListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
        {
            this.videoRepository = videoRepository;
            this.mapper = mapper;
        }

        public async Task<List<VideosVM>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await videoRepository.GetByUsername(request.UserName);

            return mapper.Map<List<VideosVM>>(videoList);
        }
    }
}
