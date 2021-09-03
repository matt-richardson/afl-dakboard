using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace afl_dakboard.Models
{
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BigBashTeam
    {
        [JsonConstructor]
        public BigBashTeam(
            [JsonProperty("resource")] string resource,
            [JsonProperty("id")] int id,
            [JsonProperty("name")] string name,
            [JsonProperty("code")] string code,
            [JsonProperty("image_path")] string imagePath,
            [JsonProperty("country_id")] int countryId,
            [JsonProperty("national_team")] bool nationalTeam,
            [JsonProperty("updated_at")] DateTime updatedAt
        )
        {
            Resource = resource;
            Id = id;
            Name = name;
            Code = code;
            ImagePath = imagePath;
            CountryId = countryId;
            NationalTeam = nationalTeam;
            UpdatedAt = updatedAt;
        }

        [JsonProperty("resource")]
        public string Resource { get; }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("code")]
        public string Code { get; }

        [JsonProperty("image_path")]
        public string ImagePath { get; }

        [JsonProperty("country_id")]
        public int CountryId { get; }

        [JsonProperty("national_team")]
        public bool NationalTeam { get; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; }
    }

    public class BigBashTeamsRoot
    {
        [JsonConstructor]
        public BigBashTeamsRoot(
            [JsonProperty("data")] List<BigBashTeam> data
        )
        {
            Teams = data;
        }

        [JsonProperty("data")]
        public IReadOnlyList<BigBashTeam> Teams { get; }
    }
}
