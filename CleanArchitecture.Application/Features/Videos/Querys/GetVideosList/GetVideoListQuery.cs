using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class GetVideoListQuery : IRequest<List<GetVideoListQueryResult>>
    {
        public GetVideoListQuery(string? userName)
        {
            UserName = userName;
        }

        //public GetVideoListQuery(string userName)
        //{
        //    UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        //}

        public string? UserName { get; set; }
    }
}
