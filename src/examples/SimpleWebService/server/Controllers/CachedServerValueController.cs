using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Examples.SimpleWebService.Core.DAL;
using KnowledgeBase.Examples.SimpleWebService.Core.Models;

namespace KnowledgeBase.Examples.SimpleWebService.Server.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class CachedServerValueController : ControllerBase
{
    private readonly ILogger<CachedServerValueController> m_logger;
    private readonly CachedServerValueDAL m_cachedServerValueDAL;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public CachedServerValueController(
        ILogger<CachedServerValueController> logger,
        CachedServerValueDAL cachedServerValueDAL)
    {
        m_logger = logger;
        m_cachedServerValueDAL = cachedServerValueDAL;
    }

    /// <summary>
    /// 
    /// </summary>
    [HttpPost(Name = "PostCachedServerValues")]
    public IActionResult PostCachedServerValues(List<CachedServerValueEntry> values)
    {
        if (values == null || values.Count == 0)
        {
            return BadRequest("No values provided.");
        }

        try
        {
            var orderedValues = values.OrderBy(v => v.Code).ToList();
            values.Clear();
            m_cachedServerValueDAL.InsertCachedValues(orderedValues);
            orderedValues.Clear();
            return Ok("Values saved successfully.");
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [HttpGet(Name = "GetCachedServerValue")]
    public IActionResult GetCachedServerValues(int? code)
    {
        try
        {
            var cachedValues = m_cachedServerValueDAL.GetCachedValues(code);
            return Ok(cachedValues);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
