using HackerNewsAggregator.Models;

namespace HackerNewsAggregator.Services;

public interface IHackerNewsService
{
    Task<List<int>> GetBestStoriesIdsAsync();
    Task<StoryDto?> GetStoryAsync(int storyId);
    Task<List<StoryDto?>> GetBestStoriesAsync();
}