using Newtonsoft.Json;

namespace Project.Models
{
    public class JsonProduct
    {
        public double carbohydrates_100g { get; set; }

        [JsonProperty("energy-kcal_100g")]
        public int energykcal_100g { get; set; }
        public double fat_100g { get; set; }
        public string product_name { get; set; }
        public double proteins_100g { get; set; }

    }
}
