using Atlassian.Demo.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Models.Mood
{
    public class GetMoodRatingOptionsResponse
    {
        public GetMoodRatingOptionsResponse()
        {
            MoodRatingOptions = new List<MoodRatingOptionResponse>();

            var values = System.Enum.GetValues(typeof(MoodRatingOption)).Cast<MoodRatingOption>(); ;
            foreach (var value in values) 
            {
                MoodRatingOptions.Add(new MoodRatingOptionResponse(value));
            }
        }

        public List<MoodRatingOptionResponse> MoodRatingOptions { get; set; }
    }
}
