using ASP_NET_07._TaskFlow_Introduction.Models;
using ASP_NET_07._TaskFlow_Introduction.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_07._TaskFlow_Introduction.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskItemsController : ControllerBase
{

    private readonly ITaskItemService _taskService;

    public TaskItemsController(ITaskItemService taskService)
    {
        _taskService = taskService;

    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<TaskItem>> GetById(int projectId)
    {
        var task = await _taskService.GetByProjectIdAsync(projectId);
        if (task == null) return NotFound($"No task found for project ID {projectId}");
        return Ok(task);
    }


    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem taskItem)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdTask = await _taskService.CreateAsync(taskItem);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }
}
