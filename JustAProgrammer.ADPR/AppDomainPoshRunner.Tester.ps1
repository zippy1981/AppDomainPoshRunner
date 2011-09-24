Add-Type -Path 'JustAProgrammer.ADPR.dll'
Add-Type -Path 'JustAProgrammer.ADPR.Helper.dll'

try {
	[JustAProgrammer.ADPR.Helper.AppDomainPoshRunnerHelper]::RunScriptInAppDomain("AppDomainPoshRunner.SecondAppDomain.ps1");
}
catch {
	Write-Error $_.Exception;
}
