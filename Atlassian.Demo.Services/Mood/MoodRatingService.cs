using Atlassian.Demo.Config.Provider;
using Atlassian.Demo.Data.Entity;
using Atlassian.Demo.Models;
using Atlassian.Demo.Models.Constant;
using Atlassian.Demo.Models.Mood;
using Atlassian.Demo.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Services.Mood
{
    public class MoodRatingService : IMoodRatingService
    {
        private readonly ILogger<MoodRatingService> _logger;
        private readonly IMoodRatingRecordRepository _moodRatingRepository;

        public MoodRatingService(ILogger<MoodRatingService> logger, 
            IMoodRatingRecordRepository moodRatingRepository)
        {
            _logger = logger;
            _moodRatingRepository = moodRatingRepository;
        }

        public async Task<(GetMoodRatingOptionsResponse, List<Error> errors)> GetMoodRatingOptions()
        {
            var result = new GetMoodRatingOptionsResponse();
            var errors = new List<Error>();

            return await  Task.FromResult( (result, errors));
        }

        public async Task<(RecordMoodRatingResponse, List<Error> errors)> RecordMoodRating(RecordMoodRatingRequest request)
        {
            var result = new RecordMoodRatingResponse();
            var errors = new List<Error>();
            var currentDateUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

            var allMoodRatingRecords = _moodRatingRepository.GetAll();

            var myList = allMoodRatingRecords.ToList();

            // only one record should exist per day
            var existingRecord = allMoodRatingRecords.FirstOrDefault(s => s.Email == request.Email && s.CreatedDateUtc == currentDateUtc);
            if (existingRecord != null)
            {
                errors.Add(Error.InvalidRequestError(ErrorConstants.InvalidRequestInputCode, "You already rated your mood today!"));
                result.Id = existingRecord.Id;
                result.AlreadyRecorded = true;
                return (result, errors);
            }

            var newRecord = new MoodRatingRecord()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                CreatedDateUtc = currentDateUtc,
                Rating = (int)request.Rating,
                Comment = request.Comment
            };

            _moodRatingRepository.Add(newRecord);
            _moodRatingRepository.SaveChanges();
            result.Id = newRecord.Id;

            return await Task.FromResult((result, errors));
        }
    }
}
