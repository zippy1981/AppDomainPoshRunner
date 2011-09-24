AppDomainPoshRunner, or ADPS for short, is a powershell runner that runs PowerShell scripts in a separate appdomain.

Its main purpose is for using powershell to test DLLs and web services, or scripts and odules that wrap around them. This is because once you load a DLL via Add-Type-Path or a Web Service proxy via New-WebServiceProxy, it remains for the life of the powershell instance, even if you reset the runspace. You cannot unload a dll from an AppDomain, and RunSpaces live inside AppDomain's.
