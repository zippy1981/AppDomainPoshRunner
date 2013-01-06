try {
  $package = 'poshrunner'
  $url = 'http://downloads.sourceforge.net/project/poshrunner/0.9.20130105/poshrunner-Release.zip?use_mirror=switch'
  $destination = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)" 

  Install-ChocolateyZipPackage $package -url $url -unzipLocation $destination
} catch {
  Write-ChocolateyFailure $package "$($_.Exception.Message)"
  throw
}