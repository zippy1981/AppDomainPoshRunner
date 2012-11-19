What is ADPR?
-------------

AppDomainPoshRunner, or ADPS for short, is a library and executable for running PowerShell scripts in a separate appdomain.

### Why use ADPR? ###


Here are some compelling features:

* A script can have its own app.config separate from powershell.exe.config
* All outuput is written to a custom implementation of [log4Net.ILog](http://logging.apache.org/log4net/release/sdk/log4net.ILog.html). This means:
    * You can send script output to a log file instead of (or in addition to) the console.
    * You can have `Write-Error` write a message to the event log.
    * You can send script output to mongodb with [Log4Mongo](http://log4mongo.org/display/PUB/Log4mongo+for+.NET)
    * Much much more

### What is the status of ADPR ###

ADPR is a little rough around the edges, but quite useable. Because ADPR implements [PSHost](http://msdn.microsoft.com/en-us/library/system.management.automation.host.pshost(VS.85).aspx), the following cmdlets will run:

    Write-Verbose 'Verbose Message';
    Write-Debug 'Debug Message';
    Write-Host 'Host Message';
    Write-Host -ForegroundColor green "Green Text"
    Write-Warning 'Warning Message';
    Write-Error 'Error Message'
