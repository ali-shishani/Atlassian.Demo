using Atlassian.Demo.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlassian.Demo.Services.ConsoleApp
{
    public interface IConsoleAppService
    {
        Task RunConsole();
    }
}
