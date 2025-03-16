using AutoBogus;
using Bogus;
using FluentAssertions;
using Atlassian.Demo.Data;
using Atlassian.Demo.Data.Entity;
using Atlassian.Demo.Models.Mood;
using Atlassian.Demo.Repositories;
using Atlassian.Demo.Services.Mood;
using MockQueryable.Moq;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Services.Tests.MooRatingServicedTests
{
    [TestClass]
    public class RecordMoodRatingTest
    {
        private AutoMocker _autoMocker;
        private IAutoFaker _faker;
        private Mock<AtlassianDemoDbContext> _mockMoodRatingDbContext;
        private Mock<IMoodRatingRecordRepository> _mockMoodRatingRecordRepository;
        private MoodRatingService _mockMoodRatingService;

        [TestInitialize]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _faker = AutoFaker.Create(builder => builder.WithRecursiveDepth(1).WithTreeDepth(2).WithRepeatCount(1));

            _mockMoodRatingDbContext = _autoMocker.GetMock<AtlassianDemoDbContext>();
            _mockMoodRatingRecordRepository = _autoMocker.GetMock<IMoodRatingRecordRepository>();
            _mockMoodRatingService = _autoMocker.CreateInstance<MoodRatingService>();
        }

        private void CreateFakeData()
        {
            var currentDateUtc = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

            var moodRatingRecorValues = new Faker<MoodRatingRecord>()
                .RuleFor(s => s.CreatedDateUtc, currentDateUtc)
                .RuleFor(s => s.Email, "test@test.com")
                .Generate(1).AsQueryable().BuildMockDbSet();

            _mockMoodRatingRecordRepository.Setup(context => context.GetAll()).Returns(moodRatingRecorValues.Object);
        }

        [TestMethod]
        public async Task RecordMoodRating_Should_Record_Successfully()
        {
            var request = new RecordMoodRatingRequest()
            {
                Email = "test@test.com",
                Comment = "test comment",
                Rating = 1,
            };

            var (result, errors) = await _mockMoodRatingService.RecordMoodRating(request);

            result.AlreadyRecorded.Should().BeFalse();
            errors.Count.Should().Be(0);
        }

        [TestMethod]
        public async Task RecordMoodRating_Should_Not_Record_Duplicates()
        {
            CreateFakeData();
            var request = new RecordMoodRatingRequest()
            {
                Email = "test@test.com",
                Comment = "test comment",
                Rating = 1,
            };

            var (result, errors) = await _mockMoodRatingService.RecordMoodRating(request);

            result.AlreadyRecorded.Should().BeTrue();
            errors.Count.Should().Be(1);
        }

        [TestMethod]
        public async Task RecordMoodRating_Should_Record_Multiple_Different_Ratings_Aday()
        {
            CreateFakeData();
            var request = new RecordMoodRatingRequest()
            {
                Email = "test@test.com",
                Comment = "test comment",
                Rating = 1,
            };

            // this is a duplicate record
            var (result, errors) = await _mockMoodRatingService.RecordMoodRating(request);
            result.AlreadyRecorded.Should().BeTrue();
            errors.Count.Should().Be(1);

            // should accept it after changing the email
            request.Email = "other.email@test.com";
            (result, errors) = await _mockMoodRatingService.RecordMoodRating(request);
            result.AlreadyRecorded.Should().BeFalse();
        }
    }
}
