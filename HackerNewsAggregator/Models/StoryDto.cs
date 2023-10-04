using System.ComponentModel.DataAnnotations;

namespace HackerNewsAggregator.Models;

public class StoryDto
{
    [Display(Name = "by")] public string? By { get; set; }
    [Display(Name = "descendants")] public int Descendants { get; set; }
    [Display(Name = "id")] public int Id { get; set; }
    [Display(Name = "kids")] public List<int>? Kids { get; set; }
    [Display(Name = "score")] public int Score { get; set; }
    [Display(Name = "time")] public int Time { get; set; }
    [Display(Name = "title")] public string? Title { get; set; }
    [Display(Name = "type")] public string? Type { get; set; }
    [Display(Name = "url")] public string? Url { get; set; }
}