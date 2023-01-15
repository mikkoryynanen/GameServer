// sing NuGet package https://github.com/tonerdo/dotnet-env

using System;

namespace GameSever.Utils
{
    public struct EnvironmentVariables
    {
        public ushort Port;
        public int MaxClients;
    }
    
    public class EnvironmentParameters
    {
        public static void Init()
        {
             DotNetEnv.Env.TraversePath().Load(".env");
        }

        public static EnvironmentVariables? GetEnvironmentVariables()
        {
            try
            {
                return new EnvironmentVariables
                {
                    Port = ushort.Parse(DotNetEnv.Env.GetString("Port")),
                    MaxClients = int.Parse(DotNetEnv.Env.GetString("MaxClients"))
                };
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error: .env file is missing arguments");
            }

            return null;
        }
    }
}