<#
    .Synopis
    Creates an icon of the PoshRunner logo from the pngs.
    .Notes
    Because this script uses a 32 bit version of FreeImage.dll, it needs to be run from 32 bit PowerShell.
    Copyright 2013 Justin Dearing (zippy1981@gmail.com)
#>
if ([IntPtr]::Size -ne 4) {
    Throw 'This script must be run from a 32 bit version of powershell.exe (i.e %systemroot%\syswow64\WindowsPowerShell\v1.0\powershell.exe)'
}

$artPath =  Split-Path $MyInvocation.MyCommand.Path
$iconFile = Join-Path $artPath 'PoshRunner.Logo.ico'
Add-Type -Path (Join-Path $artPath '.\FreeImageNET.dll')

if (Test-Path $iconFile) { rm -Force $iconFile }
try { 
    16,32,48,64,128,256 | %{
        $fileName = Join-Path $artPath "PoshRunner.Logo-$($_)x$($_).png";
        $fib = New-Object FreeImageAPI.FreeImageBitmap $fileName;
        if ($_ -eq 16) { $fib.Save($iconFile); }
        else { $fib.SaveAdd((Join-Path $artPath  ".\PoshRunner.Logo.ico")); }
        $fib.Dispose()
    }
    
} catch {
    throw $_.Exception
}
