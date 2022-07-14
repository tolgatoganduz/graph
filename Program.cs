// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");



using BenchmarkDotNet.Running;

// using BenchmarkDotNet.Columns;
// using BenchmarkDotNet.Configs;
// using BenchmarkDotNet.Loggers;

// using BenchmarkDotNet.Validators;

// var config = new ManualConfig()
//         .WithOptions(ConfigOptions.DisableOptimizationsValidator)
//         .AddValidator(JitOptimizationsValidator.DontFailOnError)
//         .AddLogger(ConsoleLogger.Default)
//         .AddColumnProvider(DefaultColumnProviders.Instance);

// BenchmarkRunner.Run<GraphBenchMark>(config);

BenchmarkRunner.Run<GraphBenchMark>();







