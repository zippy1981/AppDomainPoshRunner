What is ADPR?
-------------
[![Gitter](https://badges.gitter.im/Join Chat.svg)](https://gitter.im/zippy1981/AppDomainPoshRunner?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

AppDomainPoshRunner, or ADPS for short, is a library and executable for running PowerShell scripts in a separate appdomain. Its most common form is the standalone executable poshrunner.exe.

### Usage examples ###

Included in the zip file is AppDomainPoshRunner.SecondAppDomain.ps1, so you can run these examples by unzipping the files, opening a command prompt, and cd-ing to the directory you unzipped the files to

    REM run AppDomainPoshRunner.SecondAppDomain.ps1 and write output to the console
    poshrunner -f AppDomainPoshRunner.SecondAppDomain.ps1
	REM run AppDomainPoshRunner.SecondAppDomain.ps1 and write output to poshrunner.log
	poshrunner -f AppDomainPoshRunner.SecondAppDomain.ps1 --log4netConfigType=File

### Why use ADPR? ###

Here are some compelling features:

* A script can have its own app.config separate from powershell.exe.config
* All output is written to a custom implementation of [log4Net.ILog](http://logging.apache.org/log4net/release/sdk/log4net.ILog.html). This means:
    * You can send script output to a log file instead of (or in addition to) the console.
    * You can have `Write-Error` write a message to the event log.
    * You can send script output to mongodb with [Log4Mongo](http://log4mongo.org/display/PUB/Log4mongo+for+.NET)
    * Much much more

### How do I download ADPR ###

The recommended way to install poshrunner.exe is [the chocolatey package](http://chocolatey.org/packages/PoshRunner/).

Since GitHub [no longer allows downloads](https://github.com/blog/1302-goodbye-uploads), I have created [a sourceforge.net project for PoshRunner](https://sourceforge.net/projects/poshrunner/). Here is a [direct link](https://sourceforge.net/projects/poshrunner/files/latest/download) to the latest download.

### What is the status of ADPR ###

ADPR is a little rough around the edges, but quite useable. Because ADPR implements [PSHost](http://msdn.microsoft.com/en-us/library/system.management.automation.host.pshost(VS.85).aspx), the following cmdlets will run:

    Write-Verbose 'Verbose Message';
    Write-Debug 'Debug Message';
    Write-Host 'Host Message';
    Write-Host -ForegroundColor green "Green Text"
    Write-Warning 'Warning Message';
    Write-Error 'Error Message'
