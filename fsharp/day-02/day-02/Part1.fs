module Part1

open System
open System.Threading.Tasks
open System

open FParsec
open System

// only 12 red cubes, 13 green cubes, and 14 blue cubes

type CubeSet =
    { nbOfBlue: int
      nbOfGreen: int
      nbOfRed: int }

module CubeSet =
    let isPossible (cubeSet: CubeSet) =
        cubeSet.nbOfBlue <= 14 && cubeSet.nbOfGreen <= 13 && cubeSet.nbOfRed <= 12

type GameRecord = int * CubeSet list

module GameRecord =
    let isPossible (gameRecord: GameRecord) =
        snd gameRecord |> List.tryFind (not << CubeSet.isPossible) |> Option.isNone


let parseInput (input: string) =
    let parseRecord input : GameRecord =
        let pcolor color updateState =
            attempt (pint32 .>> spaces .>> skipStringCI color |>> fun i -> updateState i)

        let pcubeset =
            let pset =
                choice
                    [ pcolor "blue" (fun n -> fun cs -> { cs with nbOfBlue = n })
                      pcolor "green" (fun n -> fun cs -> { cs with nbOfGreen = n })
                      pcolor "red" (fun n -> fun cs -> { cs with nbOfRed = n }) ]

            sepEndBy1 (spaces >>. pset .>> spaces) (pstring ",")
            |>> List.fold
                    (fun state fn -> fn state)
                    { nbOfBlue = 0
                      nbOfGreen = 0
                      nbOfRed = 0 }

        let parser =
            skipStringCI "Game" >>. spaces >>. pint32 .>> skipString ":" .>> spaces
            .>>. sepEndBy1 pcubeset (pstring ";")

        match run parser input with
        | Success(value, _, _) -> value
        | Failure(err, _, _) -> failwith err

    input
    |> _.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
    |> Array.map parseRecord

let exec (input: string) (log: string -> unit) =
    parseInput input
    |> Array.filter (fun record ->
        log (sprintf "%A -> %b" record (GameRecord.isPossible record))
        GameRecord.isPossible record)
    |> Array.sumBy fst
    |> string
    |> Task.FromResult