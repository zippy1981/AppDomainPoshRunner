 <#
.SYNOPSIS 
    Gets the directory Inkscape is installed to. 
.DESCRIPTION 
    Returns the directory  Inkscape is installed to.
.INPUTS 
    None 
.OUTPUTS 
    System.String
 #>
function Get-InkscapeDir() {
    if ([IntPtr]::Size -eq 8) {
	    $inkscapeDir = (Get-ItemProperty HKLM:\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Inkscape InstallDir).InstallDir
    } else {
	    $inkscapeDir = (Get-ItemProperty HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Inkscape InstallDir).InstallDir
    }

    if ([string]::IsNullOrEmpty($inkscapeDir)) {
	    throw (New-Object IO.FileNotFoundException "Inkscape does not appear to be installed")
    } 
    if (-not (Test-Path -PathType Container $inkscapeDir)) {
	    throw (New-Object IO.FileNotFoundException "InkScape folder $($inkscapeDir) not found", $inkscapeDir)
    }
    Write-Verbose "Inkscape install path $($installDir)"
    $inkscapeDir
}

<#
.SYNOPSIS 
    Gets the path of inkscape.com,inkscapec.exe or inkscape.exe. 
.DESCRIPTION 
    Returns the full path of the inkscape executable. This is one of three options:
    * inkscape.com: This is a wrapper to inkscape.exe introduced in Inkscape 0.48 that writes the output of inkscape.exe to the console.
    * inkscapec.exe: This is a crude first attempt at a wrapper. Source and description is available at http://kaioa.com/node/63.
    * inkscape.exe: This is the inkscape executable compiled as a windows app. This supports command line options and non-interactive running, but does not write any output to stdout or stderr
.INPUTS 
    None 
.OUTPUTS 
    System.String
 #>
function Get-InkscapeExePath() {
    param (
        [Parameter(Mandatory=$false, Position=0)][AllowEmptyString()][string] $InkscapeDir = $(
            if ([string]::IsNullOrEmpty($InkscapeDir)) { Get-InkscapeDir } else { $InkscapeDir }
        )
    )
    $inkscapeExe = Join-Path $InkscapeDir "inkscape.exe"
    if (-not (Test-Path -PathType Leaf $inkscapeExe))  {
	    throw (New-Object IO.FileNotFoundException "InkScape.exe not found", $inkscapeExe)
    }
    if (Test-Path -PathType Leaf (Join-Path $inkscapeDir "inkscape.com")) {
        $inkscapeExe = Join-Path $InkscapeDir "inkscape.com"
    }
    else {
	    
        if (Test-Path -PathType Leaf (Join-Path $inkscapeDir "inkscapec.exe")) {
	        Write-Warning "inkscapec.exe found. You should probably upgrade to inkscape 0.48.2 which uses inkscape.com"
            $inkscapeExe = Join-Path $inkscapeDir "inkscapec.exe"
        }
        else {
            Write-Warning 'inkscape.com not found. Images *should* be generated, but inkscape.exe will not write anything to stdout or stderr'
        }
        Write-Warning "`tSee http://inkscape.13.n6.nabble.com/Windows-Console-Options-td2792080.html#a2792083 for details."
    }
	    
    Write-Verbose "InkScape executablepath: $inkscapeExe"
    $inkscapeExe
}
#iex "start `"$inkscapeExe`""

# help Get-Inkscape*
$artFolder =  (split-path -parent $MyInvocation.MyCommand.Definition)
$inkscapeExe = Get-InkscapeExePath
[System.Collections.Generic.List[string]] $png2IcoArgs = New-Object 'System.Collections.Generic.List[string]'
$png2IcoArgs.Add("`"$(Join-Path $artFolder 'PoshRunner.Logo.ico')`"");

# TODO: Support 256 color icons.
16,32,48,64,128,256 | %{
    $pngName = "PoshRunner.Logo-$($_)x$($_).png"
    $pngName = "$(Join-Path $artFolder $pngName)"
	& "$inkscapeExe" --export-png=$($pngName) -w $_ -h $_ (Join-Path $artFolder 'PoshRunner.Logo.svg')  | Write-Verbose -Verbose
    $png2IcoArgs.Add("`"$($pngName)`"");
}

# This needs to run in a 32 bit instance of powershell
& "$($env:systemroot)\syswow64\WindowsPowerShell\v1.0\powershell.exe" -file buildIcon.ps1 -noprofile