using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Running;

namespace EverythingNet.Bench;

internal class Program
{
    private static void Main(string[] args)
    {
        BenchmarkSwitcher.FromAssembly(Assembly.GetCallingAssembly()).RunAll();
    }
}
