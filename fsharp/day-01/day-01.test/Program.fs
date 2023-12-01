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
                1abc2
                pqr3stu8vwx
                a1b2c3d4e5f
                treb7uchet
                """

            // Act
            let! result = Part1.exec input outputHelper.WriteLine

            // Assert
            Assert.Equal("142", result)
        }

    [<Theory>]
    [<InlineData("""
        two1nine
        eightwothree
        abcone2threexyz
        xtwone3four
        4nineeightseven2
        zoneight234
        7pqrstsixteen
        """,
                 "281")>]
    [<InlineData("""
        threerznlrhtkjp23mtflmbrzq395three
        9sevenvlttm
        3twochzbv
        mdxdlh5six5nqfld9bqzxdqxfour
        422268
        vdctljvnj2jpgdfnbpfjv1
        tshl7foureightvzvzdcgt
        1fourrj
        6mfbqtzbprqfive
        4sevens34
        """,
                 "486")>]
    [<InlineData("""
        5eightbghcktjjninermkpmbpk
        4zctvpqqfxqdpf
        six5onebljkhvlzfour3vf7
        three7sevenspczxeight3
        eightsjxdbgcjllvpxn5ninehrhlp
        r4
        """,
                 "336")>]
    [<InlineData("eight5oneights", "88")>]
    let ``Part2`` (input: string) (expected: string) =
        task {
            // Arrange
            // Act
            let! result = Part2.exec input outputHelper.WriteLine

            // Assert
            Assert.Equal(expected, result)
        }

[<EntryPoint>]
let main _ = 0