using System.Runtime.InteropServices;
using StackExchange.Redis;
using System.Text.Json;
using HackerNewsAggregator.Models;

namespace HackerNewsAggregator.Repositories;

public class HackerNewsRepository : IHackerNewsRepository
{
    private readonly ILogger<HackerNewsRepository> _logger;
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;

    private string _bestStoriesIdsKey = "BestStoriesIds";
    private string _bestStoriesItemsKey = "BestStoriesItems";

    private static JsonSerializerOptions SerializerOptions => new()
    {
        PropertyNameCaseInsensitive = true
    };

    public HackerNewsRepository(ILogger<HackerNewsRepository> logger, ConnectionMultiplexer redis)
    {
        _logger = logger;
        _redis = redis;
        _database = redis.GetDatabase();
    }

    private IServer GetServer()
    {
        var endpoint = _redis.GetEndPoints();
        return _redis.GetServer(endpoint.First());
    }

    public IEnumerable<string> GetItems()
    {
        var server = GetServer();
        var data = server.Keys();

        return data.Select(k => k.ToString());
    }

    public async Task<bool> DeleteStoryAsync(string id)
    {
        return await _database.KeyDeleteAsync(id);
    }

    public async Task<StoryDto?> GetStoryAsync(int storyId)
    {
        var data = await _database.StringGetAsync(storyId.ToString());

        if (data.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<StoryDto>(data, SerializerOptions);
    }

    public async Task<StoryDto?> UpdateStoryAsync(int storyId, StoryDto? story)
    {
        var created = await _database.StringSetAsync(
            storyId.ToString(),
            JsonSerializer.Serialize(story, SerializerOptions),
            // there is no conditions to reach eventual consistency, so expiry time instead
            TimeSpan.FromDays(30)
        );

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the item");
            return null;
        }

        _logger.LogInformation("Story item persisted successfully");

        return await GetStoryAsync(storyId) ??
               throw new ExternalException("Failed to load story from cache after uploading it to cache");
    }

    public async Task<List<int>?> GetBestStoriesIdsAsync()
    {
        var data = await _database.StringGetAsync(_bestStoriesIdsKey);

        if (data.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<List<int>>(data, SerializerOptions);
    }

    public async Task<List<int>?> UpdateBestStoriesIdsAsync(List<int> bestStoriesIds)
    {
        var created = await _database.StringSetAsync(
            _bestStoriesIdsKey,
            JsonSerializer.Serialize(bestStoriesIds, SerializerOptions),
            // there is no conditions to reach eventual consistency, so expiry time instead
            TimeSpan.FromHours(1)
        );

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the best stories ids");
            return null;
        }

        _logger.LogInformation("Best stories ids persisted successfully");

        return await GetBestStoriesIdsAsync() ??
               throw new ExternalException("Failed to load best stories ids from cache after uploading it to cache");
    }

    public async Task<List<StoryDto?>?> GetBestStoriesItemsAsync()
    {
        var data = await _database.StringGetAsync(_bestStoriesItemsKey);

        if (data.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<List<StoryDto>>(data, SerializerOptions);
    }

    public async Task<List<StoryDto?>?> UpdateBestStoriesItemsAsync(List<StoryDto?> bestStoriesIds)
    {
        var created = await _database.StringSetAsync(
            _bestStoriesItemsKey,
            JsonSerializer.Serialize(bestStoriesIds, SerializerOptions),
            // there is no conditions to reach eventual consistency, so expiry time instead
            TimeSpan.FromHours(12)
        );

        if (!created)
        {
            _logger.LogInformation("Problem occur persisting the best stories");
            return null;
        }

        _logger.LogInformation("Best stories persisted successfully");

        return await GetBestStoriesItemsAsync() ??
               throw new ExternalException("Failed to load best stories from cache after uploading it to cache");
    }
}