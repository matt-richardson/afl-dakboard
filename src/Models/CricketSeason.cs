using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models;

public class CricketSeason
{
    [JsonConstructor]
    public CricketSeason(
        [JsonProperty("resource")] string resource,
        [JsonProperty("id")] int id,
        [JsonProperty("league_id")] int leagueId,
        [JsonProperty("name")] string name,
        [JsonProperty("code")] string code,
        [JsonProperty("updated_at")] DateTime updatedAt
    )
    {
        Resource = resource;
        Id = id;
        LeagueId = leagueId;
        Name = name;
        Code = code;
        UpdatedAt = updatedAt;
    }


    [JsonProperty("resource")]
    public string Resource { get; }

    [JsonProperty("id")]
    public int Id { get; }

    [JsonProperty("league_id")]
    public int LeagueId { get; }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("code")]
    public string Code { get; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; }
}

public class CricketSeasonRoot
{
    [JsonConstructor]
    public CricketSeasonRoot(
        [JsonProperty("data")] List<CricketSeason> data
    )
    {
        Seasons = data;
    }

    [JsonProperty("data")] public IReadOnlyList<CricketSeason> Seasons { get; }
}
    
