//
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

using CodeGen.Generators;
using CodeGen.Helpers.UnitEnumValueAllocation;
using CodeGen.JsonTypes;
using Serilog;
using Serilog.Events;
using System.CommandLine;
using System.Diagnostics;
using System.Reflection;

public class Program
{
    public static int Main(string[] args)
    {
        var verboseOption = new Option<bool>("--verbose");
        var unitsnetRootOption = new Option<DirectoryInfo?>("--unitsnet-root") 
        { 
            Description = "Path to the root of the UnitsNet repository. If not specified, it will be auto-detected." 
        };

        var rootCommand = new RootCommand("Code generation tool for nanoFramework.UnitsNet")
        {
            verboseOption,
            unitsnetRootOption
        };

        rootCommand.SetAction((parseResult) =>
        {
            var verbose = parseResult.GetValue(verboseOption);
            var unitsnetRoot = parseResult.GetValue(unitsnetRootOption);
            
            Run(
                verbose,
                unitsnetRoot);

            return 0;
        });

        return rootCommand.Parse(args).Invoke();
    }

    private static void Run(
        bool verbose,
        DirectoryInfo? unitsnetRoot)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo
            .Console(verbose ? LogEventLevel.Verbose : LogEventLevel.Information)
            .CreateLogger();

        // Enable emojis and other UTF8 symbols.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        try
        {
            unitsnetRoot ??= FindUnitsNetRoot();
            DirectoryInfo nanoRoot = FindNanoRoot();

            string unitsNetRootDir = unitsnetRoot.FullName;
            string nanoRootDir = nanoRoot.FullName;

            Stopwatch sw = Stopwatch.StartNew();

            Quantity[] quantities = QuantityJsonFilesParser.ParseQuantities(unitsNetRootDir);
            
            QuantityNameToUnitEnumValues quantityNameToUnitEnumValues = UnitEnumValueAllocator.AllocateNewUnitEnumValues($"{unitsNetRootDir}/Common/UnitEnumValues.g.json", quantities);

            QuantityRelationsParser.ParseAndApplyRelations(
                unitsNetRootDir,
                quantities);

            UnitsNetGenerator.Generate(
                unitsNetRootDir,
                quantities,
                quantityNameToUnitEnumValues);

            NanoFrameworkGenerator.Generate(
                unitsNetRootDir,
                nanoRootDir,
                quantities,
                quantityNameToUnitEnumValues);

            Log.Information("Completed in {ElapsedMs} ms!", sw.ElapsedMilliseconds);
        }
        catch (Exception e)
        {
            Log.Error(e, "Unexpected error");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static DirectoryInfo FindUnitsNetRoot()
    {
        var executableParentDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!);
        Log.Verbose("Executable dir: {ExecutableParentDir}", executableParentDir);

        for (DirectoryInfo? dir = executableParentDir; dir != null; dir = dir.Parent)
        {
            if (dir.GetFiles("nanoFramework.UnitsNet.CodeGen.slnx").Any())
            {
                Log.Verbose("Found nanoFramework Units Net repo root: {Dir}", dir);

                // now point to the UnitsNet sub-module
                

                return new DirectoryInfo(Path.Combine(dir.FullName.ToString(), "UnitsNet"));
            }

            Log.Verbose("Not repo root: {Dir}", dir);
        }

        throw new Exception($"Unable to find  Units Net repository root in directory hierarchy: {executableParentDir}");
    }

    private static DirectoryInfo FindNanoRoot()
    {
        var executableParentDir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!);
        Log.Verbose("Executable dir: {ExecutableParentDir}", executableParentDir);

        for (DirectoryInfo? dir = executableParentDir; dir != null; dir = dir.Parent)
        {
            if (dir.GetFiles("nanoFramework.UnitsNet.CodeGen.slnx").Any())
            {
                Log.Verbose("Found nanoFramework repo root: {Dir}", dir);

                return dir;
            }

            Log.Verbose("Not repo root: {Dir}", dir);
        }

        throw new Exception($"Unable to find repository root in directory hierarchy: {executableParentDir}");
    }
}
