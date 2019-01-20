using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace BookRank.Integration.Tests.Setup
{
    public class TestContext : IDisposable
    {
        private TestServer _server;

        public HttpClient Client { get; set; }

        public TestContext()
        {
            SetupClient();

            RunCommandPromptCommand("docker pull amazon/dynamodb-local");
            RunCommandPromptCommand("docker run -d -p 8000:8000 amazon/dynamodb-local");
        }

        public static void RunCommandPromptCommand(string argument)
        {
            using (var process = new Process())
            {
                var startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = $"/C {argument}"
                };
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
            }
        }

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("development")
                .UseStartup<Startup>())
                {
                    BaseAddress = new Uri("http://localhost:8000")
                };

            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            Client?.Dispose();
        }
    }
}
