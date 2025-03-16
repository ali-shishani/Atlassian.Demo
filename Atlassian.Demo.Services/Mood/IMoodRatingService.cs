using Atlassian.Demo.Models;
using Atlassian.Demo.Models.Mood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Services.Mood
{
    public interface IMoodRatingService
    {
        Task<(GetMoodRatingOptionsResponse, List<Error> errors)> GetMoodRatingOptions();
        Task<(RecordMoodRatingResponse, List<Error> errors)> RecordMoodRating(RecordMoodRatingRequest request);
    }
}
