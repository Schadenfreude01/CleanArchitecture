using CleanArchitecture.Application.Features.Videos.Querys.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator mediator;

        public VideoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{username}", Name = "GetByUserName")]
        [ProducesResponseType(typeof(IEnumerable<VideosVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<VideosVM>>> GetByUserName(string username)
        {
            var query = new GetVideoListQuery(username);
            var videos = await mediator.Send(query);

            return Ok(videos);
        }


    }
}
