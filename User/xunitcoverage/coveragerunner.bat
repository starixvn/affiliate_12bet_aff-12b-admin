@echo off
..\..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-target:..\..\packages\xunit.runner.console.2.3.1\tools\net452\xunit.console.exe" "-targetargs:..\Fanex.Affiliate.User.Core.UnitTests\bin\Debug\Fanex.Affiliate.User.Core.UnitTests.dll" -filter:"+[*]* -[Fanex.Affiliate.User.Core.UnitTests*]*" -output:User.Core.UnitTests.Coverage.xml -showunvisited -searchdirs:"..\Fanex.Affiliate.User.Core\bin\Debug"

..\..\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe "-reports:.\User.Core.UnitTests.Coverage.xml" "-targetdir:.\CoverageReport"