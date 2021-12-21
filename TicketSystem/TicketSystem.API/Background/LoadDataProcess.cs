using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TicketSystem.Core.Models;

namespace TicketSystem.API.Background
{
    public class LoadDataProcess : IHostedService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;

        public LoadDataProcess(IWebHostEnvironment env, IMemoryCache memoryCache)
        {
            _env = env;
            _memoryCache = memoryCache;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var path = Path.Combine(this._env.ContentRootPath, "Data", "Accounts.json");
            var json = ReadFromFile(path);
            var data = JsonConvert.DeserializeObject<List<AccountInfo>>(json);
            _memoryCache.Set(Constant.Account, data);
            return Task.CompletedTask;
        }

        private string ReadFromFile(string filePath)
        {
            using var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(fileStream);
            var rawData = streamReader.ReadToEnd();
            return rawData;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
