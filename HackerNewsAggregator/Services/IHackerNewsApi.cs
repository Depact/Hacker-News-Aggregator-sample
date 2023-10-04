using HackerNewsAggregator.Models;
using RestEase;

namespace HackerNewsAggregator.Services;

[Header("User-Agent", "RestEase")]
public interface IHackerNewsApi
{
    [Get("beststories.json")]
    Task<List<int>> GetBestStoriesIdsAsync();

    /// <summary>
    /// Best stories ids.
    /// If story not exist - return null
    /// </summary>
    [Get("item/{storyId}.json")] 
    Task<StoryDto?> GetStoryAsync([Path] int storyId);
}