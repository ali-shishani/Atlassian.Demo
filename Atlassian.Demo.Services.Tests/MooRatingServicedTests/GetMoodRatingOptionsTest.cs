using AutoBogus;
using FluentAssertions;
using Atlassian.Demo.Data;
using Atlassian.Demo.Repositories;
using Atlassian.Demo.Services.Mood;
using Moq;
using Moq.AutoMock;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Services.Tests.MooRatingServicedTests
{
    [TestClass]
    public class GetMoodRatingOptionsTest
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
            _mockMoodRatingService = _autoMocker.CreateInstance<MoodRatingService>();
        }

        [TestMethod]
        public async Task GetMoodRatingOptions_Returns_4_options()
        {
            var (result, errors) = await _mockMoodRatingService.GetMoodRatingOptions();

            result.MoodRatingOptions.Count.Should().Be(4);
        }

        [TestMethod]
        public async Task GetMoodRatingOptions_Returns_No_Duplicate_Options()
        {
            var (result, errors) = await _mockMoodRatingService.GetMoodRatingOptions();

            var containsDuplicates = result.MoodRatingOptions
                                    .Any(s => result.MoodRatingOptions.Count(x => x.Code == s.Code) > 1);

            containsDuplicates.Should().BeFalse();
        }
    }
}
