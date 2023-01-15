using GameSever.Services.ClientPosition;
using GameSever.Services.PlayerData;
using GameSever.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GameSever
{
    class Program
    {   
        static void Main(string[] args)
        {
            // Load .env file located in project root
            EnvironmentParameters.Init();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IClientPositionService, ClientPositionService>()
                .AddSingleton<IPlayerDataService, PlayerDataService>()
                .BuildServiceProvider();

            var server = new Server(serviceProvider);
        }
    }
}

