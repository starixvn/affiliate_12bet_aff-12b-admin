@echo off
..\..\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-target:..\..\packages\xunit.runner.console.2.3.1\tools\net452\xunit.console.exe" "-targetargs:..\Fanex.Affiliate.Admin.Core.UnitTests\bin\Debug\Fanex.Affiliate.Admin.Core.UnitTests.dll" -filter:"+[*]* -[Fanex.Affiliate.Admin.Core.UnitTests*]*" -output:Admin.Core.UnitTests.Coverage.xml -showunvisited -searchdirs:"..\Fanex.Affiliate.Admin.Core\bin\Debug"

..\..\packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe "-reports:.\Admin.Core.UnitTests.Coverage.xml" "-targetdir:.\CoverageReport"