using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using TicketSystem.Core.Models;
using TicketSystem.Core.Models.Enums;

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
            LoadData<List<AccountInfo>>(Path.Combine(this._env.ContentRootPath, "Data", "Accounts.json"), Constant.Account);
            LoadData<Dictionary<RoleType, List<TicketStatus>>>(Path.Combine(this._env.ContentRootPath, "Data",
                "TickStatusMapping.json"), Constant.TickStatusMapping);
            return Task.CompletedTask;
        }

        private void LoadData<T>(string filePath, string name)
        {
            var path = Path.Combine(filePath);
            var json = ReadFromFile(path);
            var data = JsonConvert.DeserializeObject<T>(json);
            _memoryCache.Set(name, data);
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
