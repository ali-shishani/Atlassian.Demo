using Atlassian.Demo.Models;
using Atlassian.Demo.Models.Mood;
using Atlassian.Demo.Services.Mood;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Atlassian.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodRatingController : ControllerBase
    {
        private readonly ILogger<MoodRatingController> _logger;
        private readonly IMoodRatingService _moodRatingService;

        public MoodRatingController(
            ILogger<MoodRatingController> logger,
            IMoodRatingService moodRatingService
            )
        {
            _logger = logger;
            _moodRatingService = moodRatingService;
        }

        [HttpGet("GetMoodRatingOptions")]
        public async Task<ApiResponse<GetMoodRatingOptionsResponse>> GetMoodRatingOptions()
        {
            _logger.LogInformation("User is trying to get the mood rating page options");
            var (result, errors) = await _moodRatingService.GetMoodRatingOptions();

            return new ApiResponseBuilder<GetMoodRatingOptionsResponse>()
                .WithErrors(errors)
                .WithData(result)
                .WithHttpStatus(Response, HttpStatusCode.OK)
                .Build();
        }

        [HttpPost("RecordMoodRating")]
        public async Task<ApiResponse<RecordMoodRatingResponse>> RecordMoodRating(RecordMoodRatingRequest request)
        {
            _logger.LogInformation("User is trying to record the mood rating");
            var (result, errors) = await _moodRatingService.RecordMoodRating(request);

            return new ApiResponseBuilder<RecordMoodRatingResponse>()
                .WithErrors(errors)
                .WithData(result)
                .WithHttpStatus(Response, HttpStatusCode.OK)
                .Build();
        }

        [HttpGet("RunSanityCheck")]
        public async Task<ApiResponse<bool>> RunSanityCheck()
        {
            _logger.LogInformation("Running Sanity Check");
            var (result, errors) = await _moodRatingService.RunSanityCheck();

            return new ApiResponseBuilder<bool>()
                .WithErrors(errors)
                .WithData(result)
                .WithHttpStatus(Response, HttpStatusCode.OK)
                .Build();
        }
    }
}
