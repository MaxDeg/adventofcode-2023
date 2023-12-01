module Part2

open System
open System.Threading.Tasks


let filterDigit (line: string) =
    let digits =
        [ "one", '1'
          "two", '2'
          "three", '3'
          "four", '4'
          "five", '5'
          "six", '6'
          "seven", '7'
          "eight", '8'
          "nine", '9' ]

    let rec filterDigit' (idx: int) (acc: char list) =
        if idx >= line.Length then
            List.rev acc
        else
            let c = line[idx]

            if Char.IsDigit c then
                filterDigit' (idx + 1) (c :: acc)
            else
                match List.tryFind (fun (d: string, _) -> line.AsSpan().Slice(idx).StartsWith d) digits with
                | None -> filterDigit' (idx + 1) acc
                | Some(_, value) -> filterDigit' (idx + 1) (value :: acc)

    filterDigit' 0 []

let exec (input: string) (log: string -> unit) =
    input
    |> _.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.map (fun line ->
        filterDigit line
        |> fun arr ->
            let firstDigit = List.head arr
            $"%c{firstDigit}%c{List.tryLast arr |> Option.defaultValue firstDigit}"
        |> int)
    |> Array.sum
    |> string
    |> Task.FromResult