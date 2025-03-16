using Atlassian.Demo.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Models.Mood
{
    public class MoodRatingOptionResponse
    {
        public MoodRatingOptionResponse(MoodRatingOption moodRatingOption)
        {
            Code = moodRatingOption;
            switch (moodRatingOption)
            {
                case MoodRatingOption.NotGoodAtAll:
                    DisplayName = "Not good at all";
                    break;
                case MoodRatingOption.AbitMeh:
                    DisplayName = "A bit “meh”";
                    break;
                case MoodRatingOption.PrettyGood:
                    DisplayName = "Pretty good";
                    break;
                case MoodRatingOption.FeelingGreat:
                    DisplayName = "Feeling great";
                    break;
                default:
                    throw new NotImplementedException($"Mood rating option {moodRatingOption.ToString()} is not implemented");
            }
        }

        public MoodRatingOption Code { get; set; }

        public string DisplayName { get; set; }
    }
}
