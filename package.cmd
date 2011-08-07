@echo off

echo Rebuilding the project...
"%systemroot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" nunit.extensions.sln /t:Rebuild /p:Configuration=Release

echo Updating NuGet...
external\NuGet.exe update -self

echo Packaging the project...
external\NuGet.exe pack nunit.extensions\nunit.extensions.csproj -Prop Configuration=Release
