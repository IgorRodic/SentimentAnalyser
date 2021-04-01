using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalyser.Application.Dto
{
    public class SentimentDto
    {
        public int Id { get; set; }

        public string Word { get; set; }

        public float SentimentScore { get; set; }
    }
}
