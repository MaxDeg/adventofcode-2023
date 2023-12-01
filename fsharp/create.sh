#!/bin/bash

day=$(printf "day-%02d" $1)

mkdir $day
cd $day

mkdir inputs
touch inputs/part1.txt
touch inputs/part2.txt

dotnet new console -lang f# -n $day
dotnet new xunit -lang f# -n $day.test

dotnet add $day/$day.fsproj reference ../Helpers/Helpers.fsproj
dotnet add $day.test/$day.test.fsproj reference $day/$day.fsproj

cd ..

dotnet sln adventofcode.sln add $day/$day/$day.fsproj
dotnet sln adventofcode.sln add $day/$day.test/$day.test.fsproj

dotnet test $day.test
