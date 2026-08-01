using System.Text.Json;
using octo_fiesta.Models.SquidWTF;
using Xunit;

namespace octo_fiesta.Tests;

/// <summary>
/// Tests for JioSaavn API response model deserialization.
/// Exercises the models used by SquidWTFMetadataService and SquidWTFDownloadService
/// when the Source is configured as "JioSaavn".
/// </summary>
public class JioSaavnApiResponseTests
{
    // --- JioSaavnResponse<T> wrapper -------------------------------------------------

    [Fact]
    public void JioSaavnResponse_Deserializes_Success()
    {
        var json = """{"success":true,"data":42}""";
        var result = JsonSerializer.Deserialize<JioSaavnResponse<int>>(json);
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(42, result.Data);
    }

    [Fact]
    public void JioSaavnResponse_Deserializes_Failure()
    {
        var json = """{"success":false,"data":null}""";
        var result = JsonSerializer.Deserialize<JioSaavnResponse<string>>(json);
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Null(result.Data);
    }

    // --- JioSaavnSong ---------------------------------------------------------------

    [Fact]
    public void JioSaavnSong_Deserializes_AllFields()
    {
        var json = """
        {
            "id": "abc123",
            "name": "Test Song",
            "year": "2024",
            "releaseDate": "2024-01-15",
            "duration": 245,
            "label": "Test Label",
            "explicitContent": false,
            "playCount": 1000000,
            "language": "english",
            "hasLyrics": true,
            "url": "https://saavn.squid.wtf/songs/abc123",
            "copyright": "© 2024 Test Label",
            "album": {
                "id": "alb456",
                "name": "Test Album",
                "url": "https://saavn.squid.wtf/albums/alb456"
            },
            "artists": {
                "primary": [
                    {"id": "art789", "name": "Test Artist", "role": "artist", "type": "artist"}
                ],
                "featured": [],
                "all": [
                    {"id": "art789", "name": "Test Artist", "role": "artist", "type": "artist"}
                ]
            },
            "image": [
                {"quality": "50x50",  "url": "https://img.example/50.jpg"},
                {"quality": "150x150","url": "https://img.example/150.jpg"},
                {"quality": "500x500","url": "https://img.example/500.jpg"}
            ],
            "downloadUrl": [
                {"quality": "12kbps",  "url": "https://cdn.example/song_12kbps.m4a"},
                {"quality": "48kbps",  "url": "https://cdn.example/song_48kbps.m4a"},
                {"quality": "96kbps",  "url": "https://cdn.example/song_96kbps.m4a"},
                {"quality": "160kbps", "url": "https://cdn.example/song_160kbps.m4a"},
                {"quality": "320kbps", "url": "https://cdn.example/song_320kbps.m4a"}
            ]
        }
        """;

        var song = JsonSerializer.Deserialize<JioSaavnSong>(json);

        Assert.NotNull(song);
        Assert.Equal("abc123", song.Id);
        Assert.Equal("Test Song", song.Name);
        Assert.Equal("2024", song.Year);
        Assert.Equal(245, song.Duration);
        Assert.False(song.ExplicitContent);
        Assert.True(song.HasLyrics);
        Assert.Equal("alb456", song.Album?.Id);
        Assert.Equal("Test Album", song.Album?.Name);
        Assert.Single(song.Artists?.Primary ?? []);
        Assert.Equal("Test Artist", song.Artists?.Primary?[0].Name);
        Assert.Equal(3, (song.Image ?? []).Count);
        Assert.Equal(5, (song.DownloadUrl ?? []).Count);
        Assert.Equal("320kbps", song.DownloadUrl?[4].Quality);
        Assert.Equal("https://cdn.example/song_320kbps.m4a", song.DownloadUrl?[4].Url);
    }

    [Fact]
    public void JioSaavnSong_WrappedInResponse_Deserializes()
    {
        var json = """
        {
            "success": true,
            "data": [
                {
                    "id": "s1",
                    "name": "Wrapped Song",
                    "duration": 180,
                    "downloadUrl": [
                        {"quality": "320kbps", "url": "https://cdn.example/s1_320.m4a"}
                    ]
                }
            ]
        }
        """;

        var result = JsonSerializer.Deserialize<JioSaavnResponse<List<JioSaavnSong>>>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Single(result.Data);
        Assert.Equal("s1", result.Data[0].Id);
        Assert.Equal("Wrapped Song", result.Data[0].Name);
        Assert.Equal("320kbps", result.Data[0].DownloadUrl?[0].Quality);
    }

    // --- JioSaavnSearchResult<T> ----------------------------------------------------

    [Fact]
    public void JioSaavnSearchResult_Songs_Deserializes()
    {
        var json = """
        {
            "success": true,
            "data": {
                "total": 2,
                "start": 0,
                "results": [
                    {"id": "r1", "name": "Result One", "duration": 200},
                    {"id": "r2", "name": "Result Two", "duration": 300}
                ]
            }
        }
        """;

        var result = JsonSerializer.Deserialize<JioSaavnResponse<JioSaavnSearchResult<JioSaavnSong>>>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(2, result.Data.Total);
        Assert.Equal(0, result.Data.Start);
        Assert.Equal(2, result.Data.Results?.Count);
        Assert.Equal("r1", result.Data.Results?[0].Id);
        Assert.Equal("Result Two", result.Data.Results?[1].Name);
    }

    [Fact]
    public void JioSaavnSearchResult_Albums_Deserializes()
    {
        var json = """
        {
            "success": true,
            "data": {
                "total": 1,
                "start": 0,
                "results": [
                    {"id": "alb1", "name": "Search Album", "year": 2023, "language": "hindi"}
                ]
            }
        }
        """;

        var result = JsonSerializer.Deserialize<JioSaavnResponse<JioSaavnSearchResult<JioSaavnSearchAlbum>>>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Total);
        Assert.Equal("alb1", result.Data?.Results?[0].Id);
        Assert.Equal("Search Album", result.Data?.Results?[0].Name);
    }

    [Fact]
    public void JioSaavnSearchResult_Artists_Deserializes()
    {
        var json = """
        {
            "success": true,
            "data": {
                "total": 1,
                "start": 0,
                "results": [
                    {"id": "art1", "name": "Search Artist", "type": "artist",
                     "image": [{"quality": "150x150", "url": "https://img.example/art.jpg"}]}
                ]
            }
        }
        """;

        var result = JsonSerializer.Deserialize<JioSaavnResponse<JioSaavnSearchResult<JioSaavnSearchArtist>>>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("art1", result.Data?.Results?[0].Id);
        Assert.Equal("Search Artist", result.Data?.Results?[0].Name);
        Assert.Single(result.Data?.Results?[0].Image ?? []);
        Assert.Equal("150x150", result.Data?.Results?[0].Image?[0].Quality);
    }

    // --- JioSaavnAlbum --------------------------------------------------------------

    [Fact]
    public void JioSaavnAlbum_WithSongs_Deserializes()
    {
        var json = """
        {
            "success": true,
            "data": {
                "id": "fullAlbum1",
                "name": "Full Album",
                "year": 2024,
                "language": "english",
                "songCount": 2,
                "songs": [
                    {"id": "s1", "name": "Track 1", "duration": 180,
                     "downloadUrl": [{"quality": "320kbps", "url": "https://cdn.example/t1.m4a"}]},
                    {"id": "s2", "name": "Track 2", "duration": 210,
                     "downloadUrl": [{"quality": "320kbps", "url": "https://cdn.example/t2.m4a"}]}
                ]
            }
        }
        """;

        var result = JsonSerializer.Deserialize<JioSaavnResponse<JioSaavnAlbum>>(json);

        Assert.NotNull(result);
        Assert.True(result.Success);
        var album = result.Data;
        Assert.NotNull(album);
        Assert.Equal("fullAlbum1", album.Id);
        Assert.Equal("Full Album", album.Name);
        Assert.Equal(2024, album.Year);
        Assert.Equal(2, album.SongCount);
        Assert.Equal(2, album.Songs?.Count);
        Assert.Equal("s1", album.Songs?[0].Id);
        Assert.Equal("Track 1", album.Songs?[0].Name);
        Assert.Equal("320kbps", album.Songs?[0].DownloadUrl?[0].Quality);
    }

    // --- JioSaavnQualityUrl ---------------------------------------------------------

    [Theory]
    [InlineData("12kbps",  "https://cdn.example/12.m4a")]
    [InlineData("48kbps",  "https://cdn.example/48.m4a")]
    [InlineData("96kbps",  "https://cdn.example/96.m4a")]
    [InlineData("160kbps", "https://cdn.example/160.m4a")]
    [InlineData("320kbps", "https://cdn.example/320.m4a")]
    public void JioSaavnQualityUrl_Deserializes(string quality, string url)
    {
        var json = $$"""{"quality":"{{quality}}","url":"{{url}}"}""";
        var entry = JsonSerializer.Deserialize<JioSaavnQualityUrl>(json);
        Assert.NotNull(entry);
        Assert.Equal(quality, entry.Quality);
        Assert.Equal(url, entry.Url);
    }

    // --- JioSaavnArtistRef ----------------------------------------------------------

    [Fact]
    public void JioSaavnArtistRef_WithImage_Deserializes()
    {
        var json = """
        {
            "id": "artist99",
            "name": "Famous Artist",
            "role": "singer",
            "type": "artist",
            "image": [
                {"quality": "50x50",  "url": "https://img.example/50.jpg"},
                {"quality": "500x500","url": "https://img.example/500.jpg"}
            ],
            "url": "https://saavn.squid.wtf/artists/artist99"
        }
        """;

        var artist = JsonSerializer.Deserialize<JioSaavnArtistRef>(json);

        Assert.NotNull(artist);
        Assert.Equal("artist99", artist.Id);
        Assert.Equal("Famous Artist", artist.Name);
        Assert.Equal("singer", artist.Role);
        Assert.Equal(2, artist.Image?.Count);
        Assert.Equal("500x500", artist.Image?[1].Quality);
    }

    // --- Tolerance for missing / extra fields ----------------------------------------

    [Fact]
    public void JioSaavnSong_MissingOptionalFields_DeserializesWithNulls()
    {
        var json = """{"id":"minimal","name":"Minimal Song"}""";
        var song = JsonSerializer.Deserialize<JioSaavnSong>(json);
        Assert.NotNull(song);
        Assert.Equal("minimal", song.Id);
        Assert.Equal("Minimal Song", song.Name);
        Assert.Null(song.Album);
        Assert.Null(song.Artists);
        Assert.Null(song.DownloadUrl);
        Assert.Null(song.Image);
    }

    [Fact]
    public void JioSaavnAlbum_MissingOptionalFields_DeserializesWithNulls()
    {
        var json = """{"id":"alb-min","name":"Minimal Album"}""";
        var album = JsonSerializer.Deserialize<JioSaavnAlbum>(json);
        Assert.NotNull(album);
        Assert.Equal("alb-min", album.Id);
        Assert.Null(album.Songs);
        Assert.Null(album.Artists);
    }
}
