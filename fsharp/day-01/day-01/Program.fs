module Program

open System.Threading.Tasks
open Helpers

[<EntryPoint>]
let main args =
    (execute Day01.Part1.exec Day01.Part2.exec args).Wait()
    0