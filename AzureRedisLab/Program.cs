using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.IO;

namespace AzureRedisLab
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var connectionString = config["CacheConnection"];

            using (var cache = ConnectionMultiplexer.Connect(connectionString))
            {
                IDatabase db = cache.GetDatabase();
                bool setValue = db.StringSet("test:key", "some value");
                Console.WriteLine($"SET: {setValue}");

                string value = db.StringGet("test:key");
                Console.WriteLine(value);
            }

            Console.ReadLine();

        }
    }
}
