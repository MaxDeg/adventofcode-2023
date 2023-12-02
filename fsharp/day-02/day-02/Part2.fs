module Part2

open System
open System.Threading.Tasks

open Part1

module GameRecord =
    let isPossible (gameRecord: GameRecord) =
        snd gameRecord |> List.tryFind (not << CubeSet.isPossible)

let exec (input: string) (log: string -> unit) =
    parseInput input
    |> Array.map (fun record ->
        snd record
        |> List.fold
            (fun state lowestCubeSet ->
                { nbOfBlue = Math.Max(state.nbOfBlue, lowestCubeSet.nbOfBlue)
                  nbOfGreen = Math.Max(state.nbOfGreen, lowestCubeSet.nbOfGreen)
                  nbOfRed = Math.Max(state.nbOfRed, lowestCubeSet.nbOfRed) })
            { nbOfBlue = 0
              nbOfGreen = 0
              nbOfRed = 0 }
        |> fun cSet ->
            let res = cSet.nbOfBlue * cSet.nbOfGreen * cSet.nbOfRed
            log (sprintf "%A -> %i" cSet res)
            res)
    |> Array.sum
    |> string
    |> Task.FromResult