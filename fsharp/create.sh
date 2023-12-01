#!/bin/bash

# dotnet new install ./day-template
day=$(printf "day-%02d" $1)

mkdir $day
cd $day

dotnet new adventofcode --day $day

cd ..

dotnet sln adventofcode.sln add $day/$day/$day.fsproj
dotnet sln adventofcode.sln add $day/$day.test/$day.test.fsproj

cd $day

dotnet test $day.test
