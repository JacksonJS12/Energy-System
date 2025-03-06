namespace EnergySystem.Web.API.Models
{
    using System.Text.Json.Serialization;

    public class AcMessageModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
