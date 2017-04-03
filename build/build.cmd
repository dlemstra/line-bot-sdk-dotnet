@echo off

dotnet restore

dotnet build  LineBot.sln -c release /p:codecov=true