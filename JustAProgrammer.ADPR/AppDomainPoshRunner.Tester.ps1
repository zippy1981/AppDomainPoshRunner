Add-Type -AssemblyName 'JustAProgrammer.ADPR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=622b08f923d495ef'
try {
	[JustAProgrammer.ADPR.AppDomainPoshRunner]::RunScriptInAppDomain("$(Split-Path -Parent $MyInvocation.MyCommand.Path)\AppDomainPoshRunner.SecondAppDomain.ps1");
}
catch {
	Write-Error $_.Exception;
}