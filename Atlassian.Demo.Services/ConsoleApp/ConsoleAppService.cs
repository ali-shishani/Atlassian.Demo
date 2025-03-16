using Atlassian.Demo.Models.Mood;
using Atlassian.Demo.Services.Mood;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Atlassian.Demo.Services.ConsoleApp
{
    public class ConsoleAppService : IConsoleAppService
    {
        private readonly ILogger<ConsoleAppService> _logger;
        private readonly IMoodRatingService _moodRatingService;

        public ConsoleAppService(
            ILogger<ConsoleAppService> logger,
            IMoodRatingService moodRatingService)
        {
            _logger = logger;
            _moodRatingService = moodRatingService;
        }

        private async Task RunCodeInterview1()
        {
            _logger.LogInformation("Running code interview 1");
            Console.WriteLine("Running code interview 1" + Environment.NewLine);

            // TODO: Implement code interview 1

            Console.WriteLine("Click Enter to exit..." + Environment.NewLine);
            Console.ReadLine();
        }

        private async Task RunCodeInterview2()
        {
            _logger.LogInformation("Running code interview 2");
            Console.WriteLine("Running code interview 1" + Environment.NewLine);

            // TODO: Implement code interview 2

            Console.WriteLine("Click Enter to exit..." + Environment.NewLine);
            Console.ReadLine();
        }

        private async Task RunSanityCheck()
        {
            _logger.LogInformation("Running sanity check");

            // check if the the sanity check can run successfully
            var (optionsResult, optionsErrors) = await _moodRatingService.RunSanityCheck(false);

            // sanity check is done
            Console.WriteLine("Click Enter to exit..." + Environment.NewLine);
            Console.ReadLine();
        }

        public async Task RunConsole()
        {

            try
            {
                // welcome the user
                Console.WriteLine("Welcome to the Atlassian Demo Console App!" + Environment.NewLine);
                Console.WriteLine("Please entry 1 to run code interview 1, or 2 to run code interview 2.");

                // read user entry
                var entry = Console.ReadLine();

                switch (entry)
                {
                    case "0":
                        await RunSanityCheck();
                        break;
                    case "1":
                        await RunCodeInterview1();
                        break;
                    case "2":
                        await RunCodeInterview2();
                        break;
                    default:
                        throw new NotImplementedException($"The input of \"{entry}\" is not supported");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ooops... there is an error: {ex.Message}");
                Console.WriteLine($"Click Enter to continue");
                Console.ReadLine();
                throw;
            }
        }
    }
}
