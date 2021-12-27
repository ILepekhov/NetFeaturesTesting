using ConfigurationBinderExample;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

TusConfiguration tusConfiguration = configuration.GetSection("TusConfiguration").Get<TusConfiguration>();

Console.WriteLine(tusConfiguration);
