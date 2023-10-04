using HackerNewsAggregator.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsAggregator.Controllers;

[ApiController]
[Route("[controller]")]
public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(
        IHackerNewsService hackerNewsService
    )
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("story/best/ids")]
    public async Task<IActionResult> GetBestStoriesIds()
    {
        return Ok(await _hackerNewsService.GetBestStoriesIdsAsync());
    }

    [HttpGet("story/best")]
    public async Task<IActionResult> GetBestStories()
    {
        return Ok(await _hackerNewsService.GetBestStoriesAsync());
    }

    [HttpGet("story/{storyId}")]
    public async Task<IActionResult> GetStory(int storyId)
    {
        return Ok(await _hackerNewsService.GetStoryAsync(storyId));
    }
}