module Helpers

open Argu

open System.IO
open System.Reflection
open System.Threading.Tasks
open System.Diagnostics

[<RequireQualifiedAccess>]
type PartArgument =
    | [<MainCommand; ExactlyOnce>] InputPath of inputPath: string

    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | InputPath _ -> "Path to the input file"

[<RequireQualifiedAccess>]
type Arguments =
    | [<CliPrefix(CliPrefix.None)>] Part1 of ParseResults<PartArgument>
    | [<CliPrefix(CliPrefix.None)>] Part2 of ParseResults<PartArgument>

    interface IArgParserTemplate with
        member this.Usage =
            match this with
            | Part1 _ -> "Execute Part1"
            | Part2 _ -> "Execute Part2"

type PartExecutor = string -> (string -> unit) -> Task<string>

let executePart (exec: PartExecutor) inputPath =
    task {
        let! data = File.ReadAllTextAsync inputPath
        let sw = Stopwatch.StartNew()
        let! result = exec data (printfn "%s")
        printfn $"executed in {sw.Elapsed}"
        printfn $"%s{result}"
    }

let execute (execPart1: PartExecutor) (execPart2: PartExecutor) args =
    task {
        try
            let pgName = Assembly.GetEntryAssembly().FullName
            let parser = ArgumentParser.Create<Arguments>(programName = pgName)
            let results = parser.ParseCommandLine(inputs = args, raiseOnUsage = true)

            if results.IsUsageRequested then
                printfn $"%s{parser.PrintUsage()}"
            else
                match results.GetSubCommand() with
                | Arguments.Part1 input ->
                    printfn "Execute Part1"
                    do! executePart execPart1 (input.GetResult PartArgument.InputPath)
                | Arguments.Part2 input ->
                    printfn "Execute Part2"
                    do! executePart execPart2 (input.GetResult PartArgument.InputPath)
        with e ->
            printfn "%s" e.Message
    }