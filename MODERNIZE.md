# Spark.NET Modernization and Dependency Confusion

Spark.NET is Sparklines for C# applications

This is an application ported by [Ahmet Alp Balkan](https://github.com/ahmetb) way back in May, 2013 to .NET 4.5, migrated to .net 6.0 in November, 2021.

Sparklines are one of my all-time favourite visualizations, though I randomly picked this project as an upgrade candidate due to its age and relative simplicity.  I also didn't have an intention to dig deeper into the Dependency Confusion issues which surfaced back in February, 2021.

You can see some of my findings and tools which might be used to prevent the Dependency COnfusion issue in the [Security](##Security) section below.

Some alternatives to this project for other languages:

 <https://rosettacode.org/wiki/Sparkline_in_unicode>

 <https://github.com/deeplook/sparklines>

## Background on Sparklines

See <https://en.wikipedia.org/wiki/Sparkline> and <https://en.wikipedia.org/wiki/Edward_Tufte>

I'm a big fan of Edward Tufte and the enormous contribution he has had to data visualization.  I own the book Beautiful Evidence by Tufte, Edward R. 2006 and it is well-read over the years.

I recall some controversy when Microsoft didn't include pie charts in one of their visualization tools on release.  It was hard to explain to clients that this "evil" visualization wasn't included because Mr. Tufte suggested it wasn't necessary and was misleading.  Considering it was one of the most overused visualizations in the world...

<https://www.datavis.ca/gallery/evil-pies.php>

<https://www.storytellingwithdata.com/blog/2011/07/death-to-pie-charts>

Peter Zelchenko <https://pete.zelchenko.com/> was the implementor of the sparkline, then known as inline charts, in Medved Quotetracker.  I used Quotetracker frequently for charting stocks.  Medved Trader <https://www.medvedtrader.com/> is the latest iteration of the software.

Edward Tufte - Microsoft patent claim for "sparklines in the grid"

<https://www.edwardtufte.com/bboard/q-and-a-fetch-msg?msg_id=0003Y1>

<https://patents.google.com/patent/US20090282325A1/en>

## Nuget Dependency Confusion?

This is not Spark.NET based on the Apache Spark project. If you're looking for that project:

<https://github.com/dotnet/spark>

There are quite a few downloads of the Spark.NET sparklines project, likely due to this confusion rather than its intended use as a sparkline library in applications.

This is not a Dependency Confusion attack, since Spark.NET wasn't around back then and I'm sure the author's intent wasn't malicious in any way.

More on the Dependency Confusion attack here <https://github.com/NuGet/Home/issues/10566> and here <https://dhiyaneshgeek.github.io/web/security/2021/09/04/dependency-confusion/>

Running `dotnet add package Spark.NET` won't install Microsoft Spark.NET.  You'll want `dotnet add package Microsoft.Spark` instead.

If you're looking for the unofficial dotnet library for consuming RESTful APIs for Cisco Spark, you'll need `dotnet add package SparkDotNet`

There used to be a Spark View engine for ASP.NET MVC, FubuMVC, NancyFx and Castle Project MonoRail frameworks.  See <https://github.com/SparkViewEngine/spark>. Somehow it's also Apache-licensed software.  It's package id was just `spark`.

For Apache Spark, see <https://spark.apache.org/>

## Preparation

It's a good idea to get familiar with the changes from the version you are upgrading to latest.

### History of C\#

<https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history>

### Compiler breaking changes

<https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/breaking-changes>

### Download Dotnet 6.0 SDK

<https://dotnet.microsoft.com/download/dotnet/6.0>

```shell
dotnet --info
```

## NuGet 6.0

November 8, 2021 - <https://devblogs.microsoft.com/nuget/announcing-nuget-6/>

## Support

If you are not working with a Microsoft Partner or directly with Microsoft, you can get support through Visual Studio Subscription Support or via the Dotnet Community.

### Microsoft Partner

<https://docs.microsoft.com/en-us/partner-center/find-a-partner>

### Visual Studio Subscriptions

<https://visualstudio.microsoft.com/subscriptions/#support?cat=visual-studio-enterprise-subscription-with-github-enterprise>

### Dotnet Community

<https://dotnet.microsoft.com/platform/community>

## Target Frameworks

This project was designed to be packaged and distributed to NuGet.

We will be targeting the `net6.0` package framework, although multiple frameworks can be targeted.

<https://docs.microsoft.com/en-us/nuget/create-packages/multiple-target-frameworks-project-file>

```plaintext
NuGet 6.0 is the first release to offer full authoring and restoring support for NuGet packages targeting .NET 6.0. You can now target the following target frameworks:

net6.0
net6.0-windows
net6.0-android
net6.0-ios
net6.0-macos
net6.0-maccatalyst
net6.0-tvos
net6.0-tizen
```

.NET 6.0 Target Framework Spec

<https://github.com/dotnet/designs/blob/main/accepted/2021/net6.0-tfms/net6.0-tfms.md>

## Modernization Path

Dotnet migration Path

First tested my usual goto, CsprojecToVS2017 which didn't work on the machine I was on.

<https://github.com/hvanbakel/CsprojToVs2017/issues/286>

I didn't try the try-convert tool.

<https://github.com/dotnet/try-convert>

Back in `March, 2021`, Microsoft announced the `.net upgrade assistant`. This is part of a broader initiative across Microsoft for modernization of desktop and .NET applications.

<https://devblogs.microsoft.com/dotnet/introducing-the-net-upgrade-assistant-preview/>

<https://github.com/dotnet/upgrade-assistant?WT.mc_id=dotnet-35129-website>

<https://dotnet.microsoft.com/platform/upgrade-assistant>

<https://docs.microsoft.com/en-us/aspnet/core/migration/50-to-60>

## Modernization Preparation

Logs have been checked in.  It could be useful to check in '.clef' logs for PR review if they aren't very large.  Otherwise `clef-tool` could be useful here.

See <https://nblumhardt.com/2017/07/clef-tool/> <https://github.com/datalust/clef-tool> for compact log event format tool.

Scripts for installing and running some steps.

```shell
dotnet tool install --global Project2015To2017.Migrate2019.Tool --version 4.1.3
Tool 'project2015to2017.migrate2019.tool' is already installed.

dotnet tool list
Package Id      Version      Commands      Manifest
---------------------------------------------------

REM WHERE IS IT?  Global tools path is broken?

dotnet tool install Project2015To2017.Migrate2019.Tool --version 4.1.3

dotnet tool run dotnet-migrate-2019
It was not possible to find any compatible framework version
The framework 'Microsoft.NETCore.App', version '2.1.0' (x64) was not found.
  - The following frameworks were found:
      3.1.20 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      3.1.21 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      5.0.9 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      5.0.10 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      5.0.12 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
      6.0.0 at [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]

You can resolve the problem by installing the specified framework and/or SDK.

The specified framework can be found at:
  - https://aka.ms/dotnet-core-applaunch?framework=Microsoft.NETCore.App&framework_version=2.1.0&arch=x64&rid=win10-x64

dotnet tool install Project2015To2017.Migrate2019.Tool --version 4.1.3

You can invoke the tool from this directory using the following commands: 'dotnet tool run dotnet-migrate-2019' or 'dotnet dotnet-migrate-2019'.
Tool 'project2015to2017.migrate2019.tool' (version '4.1.3') was successfully installed. Entry is added to the manifest file

D:\projects\csharp\Spark.NET\.config\dotnet-tools.json.

dotnet tool install -g upgrade-assistant
Tool 'upgrade-assistant' is already installed.
dotnet tool update -g upgrade-assistant
Tool 'upgrade-assistant' was successfully updated from version '0.2.226201' to version '0.3.256001'.


D:\projects\csharp\Spark.NET>upgrade-assistant upgrade Spark.NET.sln
-----------------------------------------------------------------------------------------------------------------
Microsoft .NET Upgrade Assistant v0.3.256001+3c4e05c787f588e940fe73bfa78d7eedfe0190bd

We are interested in your feedback! Please use the following link to open a survey: https://aka.ms/DotNetUASurvey
-----------------------------------------------------------------------------------------------------------------

[07:17:17 INF] Loaded 5 extensions

Telemetry
----------
The .NET tools collect usage data in order to help us improve your experience. The data is collected by Microsoft and shared with the community. You can opt-out of telemetry by setting the DOTNET_UPGRADEASSISTANT_TELEMTRY_OPTOUT environment variable to '1' or 'true' using your favorite shell.

Read more about Upgrade Assistant telemetry: https://aka.ms/upgrade-assistant-telemetry
Read more about .NET CLI Tools telemetry: https://aka.ms/dotnet-cli-telemetry

[07:17:19 INF] Using MSBuild from C:\Program Files\dotnet\sdk\6.0.100\
[07:17:19 INF] Using Visual Studio install from d:\Program Files (x86)\Microsoft Visual Studio\2019\Community [v16]
[07:17:23 INF] Initializing upgrade step Select an entrypoint

Upgrade Steps

1. [Next step] Select an entrypoint
2. Select project to upgrade

Choose a command:
   1. Apply next step (Select an entrypoint)
   2. Skip next step (Select an entrypoint)
   3. See more step details
   4. Configure logging
   5. Exit
```

```shell
--------------------
Select an entrypoint
--------------------
The entrypoint is the application you run or the library that is to be upgraded. Dependencies will then be analyzed and a recommended process
will then be determined
  Status              : Incomplete
  Risk to break build : None
  Details             : No entrypoint was selected. Solutions require an entrypoint to proceed.
Please press enter to continue...

[07:20:58 INF] Applying upgrade step Select an entrypoint
Please select the project you run. We will then analyze the dependencies and identify the recommended order to upgrade projects.
   1. SparkLib
   2. SparkTest
> 1
[07:21:04 INF] Upgrade step Select an entrypoint applied successfully
Please press enter to continue...
```

```shell
> 2
[07:21:37 INF] Skipping upgrade step Back up project
[07:21:37 INF] Upgrade step Back up project skipped
Please press enter to continue...

[07:21:57 INF] Upgrade step Convert project file to SDK style skipped
Please press enter to continue...

[07:22:02 INF] Initializing upgrade step Clean up NuGet package references
[07:22:04 WRN] Failed to get package versions from source https://api.nuget.org/v3/index.json due to an HTTP error (null)
[07:22:04 WRN] .NET Upgrade Assistant analyzer NuGet package reference cannot be added because the package cannot be found
[07:22:04 INF] No package updates needed
[07:22:04 INF] Initializing upgrade step Update TFM
[07:22:04 INF] TFM needs updated to netstandard2.0

----------
Update TFM
----------
Update TFM for current project
  Status              : Incomplete
  Risk to break build : High
  Details             : TFM needs to be updated to netstandard2.0
Please press enter to continue...
```

Once I got to step 2, the console GUI UX experience wasn't so well designed. There were both step numbers and duplicate step action numbers. The skip next step option was number 2, as was the 2 [next step] Convert project file to SDK style. I pressed 2, skipping Convert project file to SDK style.

And there's no way to go back. CTRL-C.

```shell
[07:24:16 INF] Applying upgrade step Convert project file to SDK style
[07:24:16 INF] Converting project file format with try-convert, version 0.3.256001+8aa571efd8bac422c95c35df9c7b9567ad534ad0
[07:24:17 INF] Converting project D:\projects\csharp\Spark.NET\SparkLib\SparkLib.csproj to SDK style
[07:24:18 INF] Project file converted successfully! The project may require additional changes to build successfully against the new .NET target.
[07:24:22 INF] Upgrade step Convert project file to SDK style applied successfully
Please press enter to continue...
```

The first time I ran this without the convert project file to SDK style, I got an error. After modernizing the csproj file, things worked.

```shell
[07:29:09 INF] Applying upgrade step Update TFM
[07:29:11 INF] Updated TFM to netstandard2.0
[07:29:11 INF] Upgrade step Update TFM applied successfully
Please press enter to continue...
```

A few other steps were skipped.

```shell
[07:30:33 INF] Initializing upgrade step Update NuGet Packages
[07:30:34 INF] No package updates needed
[07:30:34 INF] Initializing upgrade step Add template files
[07:30:34 INF] 0 expected template items needed
[07:30:34 INF] Initializing upgrade step Update source code
[07:30:34 INF] Running analyzers on SparkLib
[07:30:35 INF] Identified 0 diagnostics in project SparkLib
[07:30:35 INF] Initializing upgrade step Move to next project

[07:31:14 INF] Applying upgrade step Finalize upgrade
[07:31:14 INF] Upgrade step Finalize upgrade applied successfully
Please press enter to continue...

[07:31:15 INF] Upgrade has completed. Please review any changes.
[07:31:16 INF] Deleting upgrade progress file at D:\projects\csharp\Spark.NET\.upgrade-assistant
```

After reviewing the `upgrade-assistant.clef` log

```shell
dotnet build
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
C:\Program Files\dotnet\sdk\6.0.100\Microsoft.Common.CurrentVersion.targets(1217,5): error MSB3644: The reference assemblies for .NETFramework,Version=v4.5 were not found. To resolve this, install the Developer Pack (SDK/Targeting Pack) for this framework version or retarget your application. You can download .NET Framework Developer Packs at https://aka.ms/msbuild/developerpacks [D:\projects\csharp\Spark.NET\SparkTest\SparkTest.csproj]
  SparkLib -> D:\projects\csharp\Spark.NET\SparkLib\bin\Debug\netstandard2.0\SparkLib.dll

Build FAILED.

C:\Program Files\dotnet\sdk\6.0.100\Microsoft.Common.CurrentVersion.targets(1217,5): error MSB3644: The reference assemblies for .NETFramework,Version=v4.5 were not found. To resolve this, install the Developer Pack (SDK/Targeting Pack) for this framework version or retarget your application. You can download .NET Framework Developer Packs at https://aka.ms/msbuild/developerpacks [D:\projects\csharp\Spark.NET\SparkTest\SparkTest.csproj]
    0 Warning(s)
    1 Error(s)

Time Elapsed 00:00:02.19
```

```shell
dotnet --list-sdks
3.1.120 [C:\Program Files\dotnet\sdk]
3.1.415 [C:\Program Files\dotnet\sdk]
5.0.303 [C:\Program Files\dotnet\sdk]
6.0.100 [C:\Program Files\dotnet\sdk]
```

Link in the warning took me to <https://docs.microsoft.com/en-us/dotnet/framework/install/guide-for-developers> which didn't mention .NET 6.0.

<https://docs.microsoft.com/en-us/dotnet/standard/frameworks>

<https://devblogs.microsoft.com/dotnet/the-future-of-net-standard/>

```plaintext
.NET 5 will be a single product with a uniform set of capabilities and APIs that can be used for Windows desktop apps, cross-platform mobile apps, console apps, cloud services, and websites:

To better reflect this, we’ve updated the target framework names (TFMs):

net5.0. This is for code that runs everywhere. It combines and replaces the netcoreapp and netstandard names. This TFM will generally only include technologies that work cross-platform (except for pragmatic concessions, like we already did in .NET Standard).

net5.0-windows (and later net6.0-android and net6.0-ios). These TFMs represent OS-specific flavors of .NET 5 that include net5.0 plus OS-specific functionality.

We won’t be releasing a new version of .NET Standard, but .NET 5 and all future versions will continue to support .NET Standard 2.1 and earlier. You should think of net5.0 (and future versions) as the foundation for sharing code moving forward.

And since net5.0 is the shared base for all these new TFMs, that means that the runtime, library, and new language features are coordinated around this version number. For example, in order to use C# 9, you need to use net5.0 or net5.0-windows.
```

So I commented out a few items from nuget (csharp), the extension for the upgrade analyzer, and bumped the framework from netstandard21 to net60.

```shell
dotnet build
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
  SparkLib -> D:\projects\csharp\Spark.NET\SparkLib\bin\Debug\net6.0\SparkLib.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.77

D:\projects\csharp\Spark.NET\SparkLib>dotnet run
Unable to run your project.
Ensure you have a runnable project type and ensure 'dotnet run' supports this project.
A runnable project should target a runnable TFM (for instance, net5.0) and have OutputType 'Exe'.
The current OutputType is 'Library'.
```

Since this isn't a console app, and don't have a winforms app, I built and ran the tests to ensure the upgrade is ok.

```shell
D:\projects\csharp\Spark.NET\SparkTest>dotnet build
Microsoft (R) Build Engine version 17.0.0+c9eb9dd64 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
C:\Program Files\dotnet\sdk\6.0.100\Microsoft.Common.CurrentVersion.targets(1217,5): error MSB3644: The reference assemblies for .NETFramework,Version=v4.5 were not found. To resolve this, install the Developer Pack (SDK/Targeting Pack) for this framework version or retarget your application. You can download .NET Framework Developer Packs at https://aka.ms/msbuild/developerpacks [D:\projects\csharp\Spark.NET\SparkTest\SparkTest.csproj]

Build FAILED.
```

I notice the tests project wasn't updated to the SDK style format. Followed the same procedure as above to update tests project.

Most of the time I use xunit for testing rather than mstest. I created a new xunit template with `dotnet new xunit` and grabbed the dependencies from the templated csproj file.

I removed the conditional references which had dependencies on Visual Studio versions and replaced them with nuget package references.

I looked up the absolute latest preview package references. Falling backwards from the latest working version (non-preview) is probably a better approach.

```shell
dotnet list package
Project 'SparkTest' has the following package references
   [net6.0]:
   Top-level Package                Requested                    Resolved
   > coverlet.collector             3.1.0                        3.1.0
   > Microsoft.NET.Test.Sdk         17.1.0-preview-20211109-03   17.1.0-preview-20211109-03
   > MSTest.TestAdapter             2.2.7                        2.2.7
   > MSTest.TestFramework           2.2.7                        2.2.7
   > xunit                          2.4.1                        2.4.1
   > xunit.runner.visualstudio      2.4.3                        2.4.3
```

## Nullable enable

As part of modernization, you'll want to get a good handle on usage of this in projects.  For larger projects it may take some effort for the code to become compliant with this compile-time check.

<https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/nullable-reference-types>

<https://cezarypiatek.github.io/post/non-nullable-references-in-dotnet-core/>

<https://blog.johnnyreilly.com/2021/07/14/directory-build-props-c-sharp-9-for-all/>

<https://stackoverflow.com/questions/53633538/how-to-enable-nullable-reference-types-feature-of-c-sharp-8-0-for-the-whole-proj>

## Code Analysis and Formatting

I added Roslynator to the SparkLib and SparkTest projects, 7 day old version at time if this writing in `November, 2021`

<https://github.com/JosefPihrt/Roslynator>

```shell
dotnet tool install -g roslynator.dotnet.cli
dotnet add package roslynator.analyzers
dotnet add package roslynator.codeanalysis.analyzers
dotnet add package roslynator.formatting.analyzers
```

I simplified the null check, implemented recommendations from Roslynator and the VSCode IDE.

## Code coverage and Report Generator

<https://github.com/coverlet-coverage/coverlet>

<https://github.com/danielpalme/ReportGenerator>

<https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=windows>

```shell
dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet add package ReportGenerator --version 5.0.0
```

Still no easy way to get pytest-cov style missing coverage line numbers to the console in 2021?

<https://github.com/coverlet-coverage/coverlet/blob/master/Documentation/GlobalTool.md>

<https://github.com/coverlet-coverage/coverlet/issues/681>

<https://thomas-cokelaer.info/tutorials/sphinx/rest_syntax.html>

<https://github.com/microsoft/vstest/issues/981>

```shell
dotnet test --collect:"XPlat Code Coverage"
dotnet test -v n
dotnet test -l "console;verbosity=detailed"
dotnet test -l "console;verbosity=detailed" --collect "Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.SplitCoverage="True"'
```

## Formatting and Style

Added an .editorconfig file with `dotnet new editorconfig`

The `dotnet format` command and its subcommands.

```shell
dotnet format Spark.NET.sln
dotnet format whitespace Spark.NET.sln
dotnet format style Spark.NET.sln
dotnet format analyzers Spark.NET.sln
dotnet format Spark.NET.sln --verify-no-changes
```

After adding .editorconfig from the template, there was some style warnings. One was IDE1006 for the prefix of a private static method to use s\_. I don't know if that's a great convention for a modern project.

```plaintext
m for members
c for constants/readonlys
p for pointer (and pp for pointer to pointer)
v for volatile
s for static
i for indexes and iterators
e for events
```

<https://softwareengineering.stackexchange.com/questions/177184/naming-convention-field-starting-with-m-or-s>

These could be fixed in .editorconfig, by applying [SuppressMessage("Microsoft.Design", "IDE1006", Justification = "Rule violation aceppted due blah blah..")] or by using #pragma warning disable IDE1006

## Markdown style

<https://github.com/DavidAnson/markdownlint>

Use `<!-- markdownlint-disable MD001 MD005 -->` to disable some rules per file.

## Security

How to Scan NuGet Packages for Security Vulnerabilities, Mar 2, 2021

<https://devblogs.microsoft.com/nuget/how-to-scan-nuget-packages-for-security-vulnerabilities/>

Best practices for a secure software supply chain, Nov 4, 2021

<https://docs.microsoft.com/en-us/nuget/concepts/security-best-practices>

```shell
dotnet list package
dotnet list package --include-transitive
dotnet list package --deprecated
dotnet list package --vulnerable
dotnet list package --vulnerable --include-transitive
```

Use a lock file

<https://azure.microsoft.com/en-us/resources/3-ways-to-mitigate-risk-using-private-package-feeds/>

```shell
dotnet restore --use-lock-file
dotnet restore --locked-mode
```

Use NuGetDefense

<https://github.com/digitalcoyote/NuGetDefense>

Note that OWASP's SafeNuGet is deprecated.

Security Code Scan

```shell
dotnet add package SecurityCodeScan.VS2019 --version 5.2.1
```

### Sign a Package

<https://docs.microsoft.com/en-us/nuget/create-packages/sign-a-package>

## Documentation

<https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html>

<https://www.doxygen.nl/index.html>

## Packing for NuGet

<https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-pack>

```shell
dotnet pack -p:TargetFrameworks=net60
```

```shell
C:\Program Files\dotnet\sdk\6.0.100\Sdks\Microsoft.NET.Sdk\targets\Microsoft.PackageDependencyResolution.targets(267,5): error NETSDK1005: Assets file 'D:\projects\csharp\Spark.NET\SparkLib\obj\project.assets.json' doesn't have a target for 'net6.0'. Ensure that restore has run and that you have included 'net6.0' in the TargetFrameworks for your project. [D:\projects\csharp\Spark.NET\SparkLib\SparkLib.csproj]
```

```shell
dotnet new globaljson
dotnet pack
```

## Hosting feeds

<https://docs.microsoft.com/en-us/nuget/hosting-packages/overview>

Did you know that even if you don't want your private packages published to NuGet, it is recommended to reserve the unique package identifier to prevent tampering?

<https://docs.microsoft.com/en-us/nuget/nuget-org/id-prefix-reservation>

## Publish project

<https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish>

```shell
dotnet publish Spark.NET.sln
```

Publish single file

```shell
https://github.com/dotnet/designs/blob/main/accepted/2020/single-file/design.md
```

Publish to Azure Artifacts

<https://docs.microsoft.com/en-us/azure/devops/artifacts/nuget/publish?view=azure-devops>

## Adding Readme to Published Package

<https://devblogs.microsoft.com/nuget/add-a-readme-to-your-nuget-package/>

## Package Source Mapping

<https://devblogs.microsoft.com/nuget/introducing-package-source-mapping/>

## Additional Modernization

- Upgrade the solution file.  
- Update the gitignore from GitHub/gitignore.
- Add pipelines and actions
- Language specification improvements - see <https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/introduction>
- Bring your .NET Apps Forward with the .NET Upgrade Assistant <https://www.codemag.com/Article/2111032/Bring-Your-.NET-Apps-Forward-with-the-.NET-Upgrade-Assistant>
