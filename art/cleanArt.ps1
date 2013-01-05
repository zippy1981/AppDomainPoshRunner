$artFolder =  (split-path -parent $MyInvocation.MyCommand.Definition)
rm -Force (Join-Path $artFolder "*.png") -Verbose
rm -Force (Join-Path $artFolder "*.ico") -Verbose