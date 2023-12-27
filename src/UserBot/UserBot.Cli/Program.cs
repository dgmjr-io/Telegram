// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var Services = new ServiceCollection();
Services.AddLogging(builder => builder.AddConsole());
Services.AddUserBot();
