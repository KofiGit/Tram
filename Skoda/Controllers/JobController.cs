using Microsoft.AspNetCore.Mvc;
using Skoda.Entities;
using Skoda.Managers;

namespace Skoda.Controllers;

[ApiController]
[Route("/api/job")]
[Produces("application/json")]
public class JobController : ControllerBase
{
    /// <summary>
    /// Set init data for app
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("setInitData")]
    public async Task<IActionResult> SetInitData(
        [FromServices] ITramManager manager,
        CancellationToken cancellationToken)
    {
        try
        {
            await manager.SetInitData(cancellationToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    /// <summary>
    /// Get all data
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("getTramInfo")]
    public async Task<ActionResult<Tram>> GetTramInfo(
        [FromServices] ITramManager manager,
        CancellationToken cancellationToken)
    {
        try
        {
            var data = await manager.GetTrams(cancellationToken);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Assign job
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("assingTramJob")]
    public async Task<ActionResult<string>> AssignTramJob(
        [FromServices] ITramManager manager,
        CancellationToken cancellationToken)
    {
        try
        {
            var tramId = await manager.AssingTramJob(cancellationToken);
            return Ok($"Tram {tramId} was assigned job.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
