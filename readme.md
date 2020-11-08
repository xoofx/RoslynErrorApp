# Roslyn error with .NET 5

Opening the RoslynErrorApp.sln with VS 2019 Preview 16.8.0 Preview 6.0 and run the app.

Trying to `MSBuildWorkspace` / `OpenSolutionAsync` + `GetCompilationAsync` generates the following error:


```
Evaluate        0:00.0905359    RoslynErrorApp.csproj
Msbuild failed when processing the file 'C:\code\Temp\RoslynErrorApp\RoslynErrorApp\RoslynErrorApp.csproj' with message: The SDK resolver type "WorkloadSdkResolver" failed to load. Could not load file or assembly 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'. The system cannot find the file specified.  C:\code\Temp\RoslynErrorApp\RoslynErrorApp\RoslynErrorApp.csproj
Compilation errors:
error CS5001: Program does not contain a static 'Main' method suitable for an entry point
Error, Exiting.
```

dotnet --version installed: `5.0.100-rc.2.20479.15`
