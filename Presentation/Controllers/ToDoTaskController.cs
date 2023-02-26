using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.ActionFilters;
using ServiceContracts;
using Shared.DTO;
using Shared.RequestFeatures;

namespace Presentation.Controllers
{
    [Route("api/accounts/{accountId}/tasks")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ToDoTaskController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetTasksForAccount(Guid accountId,
            [FromQuery] ToDoTaskParameters parameters)
        {
            var result = await _service.ToDoTaskService.GetToDosAsync(accountId, parameters, trackChanges: false);

            return Ok(result);
        }

        [HttpGet("{id:guid}", Name = "GetTasksForAccount")]
        public async Task<IActionResult> GetTaskForAccount(Guid accountId, Guid id)
        {
            var toDo = 
                await _service.ToDoTaskService.GetToDoAsync(accountId, id, trackChanges: false);

            return Ok(toDo);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateTaskForAccount(Guid accountId,
            [FromBody] ToDoTaskForCreationDto toDo)
        {
            var taskToReturn = 
                await _service.ToDoTaskService.CreateToDoTaskForAccountAsync(accountId, toDo, trackChanges: false);

            return CreatedAtRoute("GetTaskForAccount",
                new { accountId, id = taskToReturn.Id }, taskToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTaskForAccount(Guid accountId, Guid id)
        {
            await _service.ToDoTaskService.DeleteToDoTaskForAccountAsync(accountId, id, false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateTaskForAccount(Guid accountId, Guid id,
            [FromBody] ToDoTaskForUpdateDto toDo)
        {
            await _service.ToDoTaskService.UpdateToDoTaskForAccountAsync(accountId, id, toDo, 
                accTrackChanges: false, toDoTaskTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateTaskForAccount(Guid accountId, Guid id,
            [FromBody] JsonPatchDocument<ToDoTaskForUpdateDto> patch)
        {
            if (patch is null)
                return BadRequest("patch object sent from client is null.");

            var result = await _service.ToDoTaskService.GetToDoTaskForPatchAsync(
                accountId, id, accTrackChanges: false, toDoTaskTrackChanges: true);

            patch.ApplyTo(result.toDoTaskForUpdate, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            TryValidateModel(result.toDoTaskForUpdate);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.ToDoTaskService.SaveChangesForPatchAsync(result.toDoTaskForUpdate,
                result.toDoEntity);

            return NoContent();
        }
    }
}
