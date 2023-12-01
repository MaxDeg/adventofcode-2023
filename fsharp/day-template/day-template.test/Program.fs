module Test

open System
open Xunit

open Xunit.Abstractions
open Xunit

type Specs(outputHelper: ITestOutputHelper) =
    [<Fact>]
    let ``Part1`` () =
        task {
            // Arrange
            let input =
                """
                """

            // Act
            let! result = Part1.exec input outputHelper.WriteLine

            // Assert
            Assert.Equal("...", result)
        }

    [<Fact>]
    let ``Part2`` () =
        task {
            // Arrange
            let input =
                """
                """

            // Act
            let! result = Part2.exec input outputHelper.WriteLine

            // Assert
            Assert.Equal("...", result)
        }

[<EntryPoint>]
let main _ = 0