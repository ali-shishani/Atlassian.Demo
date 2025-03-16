using Atlassian.Demo.Data.Entity;
using Atlassian.Demo.Models;
using Atlassian.Demo.Models.Constant;
using Atlassian.Demo.Models.Mood;
using Atlassian.Demo.Repositories;
using Microsoft.Extensions.Logging;

namespace Atlassian.Demo.Services.Mood
{
    public class MoodRatingService : IMoodRatingService
    {
        private readonly ILogger<MoodRatingService> _logger;
        private readonly IHttpClientFactory _factory;
        private readonly IMoodRatingRecordRepository _moodRatingRepository;

        public MoodRatingService(
            ILogger<MoodRatingService> logger,
            IHttpClientFactory factory,
            IMoodRatingRecordRepository moodRatingRepository)
        {
            _logger = logger;
            _factory = factory;
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

        public async Task<(bool, List<Error> errors)> RunSanityCheck(bool includeHttpClient = true)
        {
            var result = false;
            var errors = new List<Error>();

            try
            {
                _logger.LogInformation("Running sanity check");
                Console.WriteLine("Running sanity check" + Environment.NewLine);

                // check if the mood rating options are available
                var (optionsResult, optionsErrors) = await GetMoodRatingOptions();
                Console.WriteLine($"Sanity Check - rating options result: {string.Join(",", optionsResult.MoodRatingOptions.Select(x => x.DisplayName))}"
                    + Environment.NewLine);

                // check if I can record a mood rating
                var request = new RecordMoodRatingRequest()
                {
                    Email = "aa@bb.com",
                    Rating = 1,
                    Comment = "",
                };

                var (creationResult, creationErrors) = await RecordMoodRating(request);
                Console.WriteLine($"Sanity Check - rating creation result with Id: {creationResult.Id}"
                    + Environment.NewLine);

                if(includeHttpClient)
                {
                    // check that the app is able to send http requests
                    var baseUrl = "https://localhost:7110/api/moodrating/getmoodratingoptions";
                    using var httpClient = _factory.CreateClient();
                    var url = $"{baseUrl}";

                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(data);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode.ToString());
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                        return await Task.FromResult((result, errors));
                    }
                }

                _logger.LogInformation("Sanity Check Completed Successfully");
                Console.WriteLine("Sanity Check Completed Successfully" + Environment.NewLine);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }

            return await Task.FromResult((result, errors));
        }
    }
}
