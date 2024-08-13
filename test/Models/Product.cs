using SQLite;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace test.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Thumbnail { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }

        // JSON representation of the Images
        public string ImageUrlsJson { get; set; }

        // Ignore Images property for SQLite
        [Ignore]
        public List<string> Images
        {
            get => string.IsNullOrEmpty(ImageUrlsJson) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(ImageUrlsJson);
            set => ImageUrlsJson = JsonConvert.SerializeObject(value);
        }

        // JSON representation of the Reviews
        public string ReviewsJson { get; set; }

        // Ignore Reviews property for SQLite
        [Ignore]
        public List<ReviewItem> Reviews
        {
            get => string.IsNullOrEmpty(ReviewsJson) ? new List<ReviewItem>() : JsonConvert.DeserializeObject<List<ReviewItem>>(ReviewsJson);
            set => ReviewsJson = JsonConvert.SerializeObject(value);
        }
    }

    public class ReviewItem
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string ReviewerName { get; set; }
    }
}
