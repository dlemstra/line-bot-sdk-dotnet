@echo off

cd ..

dotnet restore

dotnet build LineBot.sln -c Release

if %errorlevel% neq 0 goto done

dotnet test tests\LineBot.Tests\LineBot.Tests.csproj --no-build -c Release

if %errorlevel% neq 0 goto done

dotnet pack src\LineBot\LineBot.csproj --no-build -c Release -o ..\..\publish

:done

pause