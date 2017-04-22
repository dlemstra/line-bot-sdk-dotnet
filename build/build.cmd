@echo off

dotnet restore

dotnet build LineBot.sln -c Release /p:codecov=true