Add-Type -AssemblyName 'JustAProgrammer.ADPR, Version=0.9.4285.38564, Culture=neutral, PublicKeyToken=622b08f923d495ef'
try {
    $script = "$(Split-Path -Parent $MyInvocation.MyCommand.Path)\AppDomainPoshRunner.SecondAppDomain.ps1"
	#[JustAProgrammer.ADPR.AppDomainPoshRunner]::RunScriptInNewAppDomain($script);
	[JustAProgrammer.ADPR.AppDomainPoshRunner]::RunScriptInPSHost($script, $Host) > $null;
	#Start-Job -FilePath $script
    #Invoke-Command -FilePath $script
}
catch {
	Write-Error $_.Exception;
}