using DevFreela.Application.Commands.CreateComment;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;
        private IValidator<CreateProjectCommand> _validator;
        public ProjectsController(IMediator mediator, IValidator<CreateProjectCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        // api/projects?query=netcore
        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get()
        {
            var getAllProjectsQuery = new GetAllProjectsQuery();

            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }
        // api/projects/1
        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectByIdQuery(id);

            var project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var isValid = _validator.Validate(command);
            if (!isValid.IsValid)
            {
                var messages = isValid.Errors
                .Select(x => x.ErrorMessage)
                .ToList();
                return BadRequest(messages);
            }

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }
        // api/projects/2
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }

        // api/projects/3
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/comments
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComments(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        //api/projects/1/start
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
        //api/projects/1/finish
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);

            return Accepted(response);
        }
    }
}
