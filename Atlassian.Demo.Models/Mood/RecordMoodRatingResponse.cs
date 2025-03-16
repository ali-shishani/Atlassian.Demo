using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Models.Mood
{
    public class RecordMoodRatingResponse
    {
        public Guid Id { get; set; }

        public bool AlreadyRecorded { get; set; }
    }
}
