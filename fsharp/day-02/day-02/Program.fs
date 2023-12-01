module Program

open System.Threading.Tasks
open Helpers

[<EntryPoint>]
let main args =
    (execute Part1.exec Part2.exec args).Wait()
    0