using CleanArchitecture.Application.Features.Videos.Querys.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //[HttpGet("{username}", Name = "GetByUserName")]
        [HttpGet(Name = "GetByUserName")]
        //[Authorize]
        [ProducesResponseType(typeof(IEnumerable<GetVideoListQueryResult>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        //public async Task<ActionResult<IEnumerable<GetVideoListQueryResult>>> GetByUserName([FromQuery] string? username)
        public async Task<ActionResult<IEnumerable<GetVideoListQueryResult>>> GetByUserName(string? username)
        {
            var query = new GetVideoListQuery(username);
            var videos = await mediator.Send(query);

            return Ok(videos);

            //return videos.Count > 0 ? Ok(videos) : NoContent();
        }

        // Comentario
    }
}
