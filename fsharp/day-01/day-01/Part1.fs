module Day01.Part1

open System
open System.Threading.Tasks

let exec (input: string) (log: string -> unit) =
    input
    |> _.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.map (fun line ->
        line.ToCharArray()
        |> Array.filter Char.IsDigit
        |> fun arr ->
            let firstDigit = Array.head arr
            $"%c{firstDigit}{Array.tryLast arr |> Option.defaultValue firstDigit}"
        |> int)
    |> Array.sum
    |> string
    |> Task.FromResult