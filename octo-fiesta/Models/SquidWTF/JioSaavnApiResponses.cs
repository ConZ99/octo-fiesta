using System.Text.Json.Serialization;

namespace octo_fiesta.Models.SquidWTF;

#region JioSaavn API Responses (saavn.squid.wtf)

/// <summary>
/// Top-level wrapper returned by saavn.squid.wtf for all responses.
/// </summary>
public class JioSaavnResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; }
}

/// <summary>
/// Paginated search result wrapper used by /api/search/songs, /api/search/albums, /api/search/artists.
/// </summary>
public class JioSaavnSearchResult<T>
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("start")]
    public int Start { get; set; }

    [JsonPropertyName("results")]
    public List<T>? Results { get; set; }
}

/// <summary>
/// Quality-keyed URL (image or download link).
/// </summary>
public class JioSaavnQualityUrl
{
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

/// <summary>
/// Minimal artist reference returned inside song / album results.
/// </summary>
public class JioSaavnArtistRef
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("image")]
    public List<JioSaavnQualityUrl>? Image { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

/// <summary>
/// Artists breakdown as returned by the song / album models.
/// </summary>
public class JioSaavnArtists
{
    [JsonPropertyName("primary")]
    public List<JioSaavnArtistRef>? Primary { get; set; }

    [JsonPropertyName("featured")]
    public List<JioSaavnArtistRef>? Featured { get; set; }

    [JsonPropertyName("all")]
    public List<JioSaavnArtistRef>? All { get; set; }
}

/// <summary>
/// Minimal album reference embedded inside a song result.
/// </summary>
public class JioSaavnAlbumRef
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

/// <summary>
/// Full song object returned by /api/songs/{id} and search results.
/// </summary>
public class JioSaavnSong
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("year")]
    public string? Year { get; set; }

    [JsonPropertyName("releaseDate")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("explicitContent")]
    public bool ExplicitContent { get; set; }

    [JsonPropertyName("playCount")]
    public long? PlayCount { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("hasLyrics")]
    public bool HasLyrics { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("copyright")]
    public string? Copyright { get; set; }

    [JsonPropertyName("album")]
    public JioSaavnAlbumRef? Album { get; set; }

    [JsonPropertyName("artists")]
    public JioSaavnArtists? Artists { get; set; }

    [JsonPropertyName("image")]
    public List<JioSaavnQualityUrl>? Image { get; set; }

    [JsonPropertyName("downloadUrl")]
    public List<JioSaavnQualityUrl>? DownloadUrl { get; set; }
}

/// <summary>
/// Full album object returned by /api/albums?id={id}.
/// </summary>
public class JioSaavnAlbum
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("year")]
    public int? Year { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("playCount")]
    public long? PlayCount { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("explicitContent")]
    public bool ExplicitContent { get; set; }

    [JsonPropertyName("artists")]
    public JioSaavnArtists? Artists { get; set; }

    [JsonPropertyName("songCount")]
    public int? SongCount { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("image")]
    public List<JioSaavnQualityUrl>? Image { get; set; }

    [JsonPropertyName("songs")]
    public List<JioSaavnSong>? Songs { get; set; }
}

/// <summary>
/// Search album result (lighter than the full AlbumModel — no songs list).
/// </summary>
public class JioSaavnSearchAlbum
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("year")]
    public int? Year { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("playCount")]
    public long? PlayCount { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("explicitContent")]
    public bool ExplicitContent { get; set; }

    [JsonPropertyName("artists")]
    public JioSaavnArtists? Artists { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("image")]
    public List<JioSaavnQualityUrl>? Image { get; set; }
}

/// <summary>
/// Search artist result returned by /api/search/artists.
/// </summary>
public class JioSaavnSearchArtist
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("image")]
    public List<JioSaavnQualityUrl>? Image { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

#endregion
