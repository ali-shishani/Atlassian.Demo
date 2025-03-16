using Atlassian.Demo.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Models.Mood
{
    public class RecordMoodRatingRequest
    {
        public int Rating { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }
    }
}
