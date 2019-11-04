using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using Logger;
using System;

namespace LoggerBench
{
    public class LoggerBenchConfig : ManualConfig
    {
        public LoggerBenchConfig()
        {
            Add(Job.MediumRun
                   .WithLaunchCount(1)
                   .With(InProcessEmitToolchain.Instance)
                   .WithId("InProcess"));
        }
    }

    [RankColumn]
    [Config(typeof(LoggerBenchConfig))]
    public class LoggerBench
    {
        static readonly Student st =
            new Student(154134, "Ze Manel", 5243, "ze", new DateTime(1990, 12, 7));
        static readonly LoggerEmit emit = new LoggerEmit(new LoggerToString());
        static readonly LoggerReflect reflect = new LoggerReflect(new LoggerToString());

        [Benchmark]
        public void BenchLoggerReflect()
        {
            reflect.Log(st);
        }
       
        [Benchmark]
        public void BenchLoggerEmit()
        {
            emit.Log(st);
        }
    }
}
