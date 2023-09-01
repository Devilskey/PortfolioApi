namespace webApi.Types;

public class PostObject
{
    public string Token { get; set; }
    #nullable enable
    public int? postId { get; set; }
    public string? PostTitle { get; set; }
    public string? PostContent { get; set; }
    public string? PostTag { get; set; }
    public string? thumbnail { get; set; }
}
