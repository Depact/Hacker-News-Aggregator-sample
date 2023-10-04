using HackerNewsAggregator.Models;
using HackerNewsAggregator.Repositories;
using RestEase;

namespace HackerNewsAggregator.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsApi _api = RestClient.For<IHackerNewsApi>("https://hacker-news.firebaseio.com/v0");

    private readonly ILogger<HackerNewsService> _logger;
    private readonly IHackerNewsRepository _hackerNewsRepository;

    public HackerNewsService(
        IHackerNewsRepository hackerNewsRepository,
        ILogger<HackerNewsService> logger
    )
    {
        _hackerNewsRepository = hackerNewsRepository;
        _logger = logger;
    }

    public async Task<List<int>> GetBestStoriesIdsAsync()
    {
        var cachedBestStories = await _hackerNewsRepository.GetBestStoriesIdsAsync();
        if (cachedBestStories != null) return cachedBestStories;

        var stories = await _api.GetBestStoriesIdsAsync();
        await _hackerNewsRepository.UpdateBestStoriesIdsAsync(stories);
        return stories;
    }

    public async Task<StoryDto?> GetStoryAsync(int storyId)
    {
        var cachedStory = await _hackerNewsRepository.GetStoryAsync(storyId);
        if (cachedStory != null) return cachedStory;

        var story = await _api.GetStoryAsync(storyId);
        await _hackerNewsRepository.UpdateStoryAsync(storyId, story);
        return story;
    }

    public async Task<List<StoryDto?>> GetBestStoriesAsync()
    {
        var cachedBestStories = await _hackerNewsRepository.GetBestStoriesItemsAsync();
        if (cachedBestStories != null) return cachedBestStories;
        
        var bestStoriesIds = await GetBestStoriesIdsAsync();

        var bestStories = new List<StoryDto?>();
        foreach (var storyId in bestStoriesIds)
        {
            var story = await GetStoryAsync(storyId);

            bestStories.Add(story);
        }
        
        await _hackerNewsRepository.UpdateBestStoriesItemsAsync(bestStories);

        return bestStories;
    }
}