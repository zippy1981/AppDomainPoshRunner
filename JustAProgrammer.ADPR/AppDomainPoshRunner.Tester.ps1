Add-Type -Path 'JustAProgrammer.ADPR.dll'

try {
	[JustAProgrammer.ADPR.AppDomainPoshRunner]::RunScriptInAppDomain("AppDomainPoshRunner.SecondAppDomain.ps1");
}
catch {
	Write-Error $_.Exception;
}
