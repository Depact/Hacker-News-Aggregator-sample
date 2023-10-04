using HackerNewsAggregator.Models;

namespace HackerNewsAggregator.Repositories;

public interface IHackerNewsRepository
{
    IEnumerable<string>? GetItems();
    Task<bool> DeleteStoryAsync(string id);
    Task<StoryDto?> GetStoryAsync(int storyId);
    Task<StoryDto?> UpdateStoryAsync(int storyId, StoryDto? story);
    Task<List<int>?> GetBestStoriesIdsAsync();
    Task<List<int>?> UpdateBestStoriesIdsAsync(List<int> bestStoriesIds);
    Task<List<StoryDto?>?> GetBestStoriesItemsAsync();
    Task<List<StoryDto?>?> UpdateBestStoriesItemsAsync(List<StoryDto?> bestStoriesIds);
}