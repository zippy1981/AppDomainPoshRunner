#This test adapted from: http://www.eggheadcafe.com/microsoft/Powershell/30766269/how-to-load--unload-dll.aspx


function Make-Dll {
	$assemblyPath = "$($ENV:TEMP)\$([Guid]::NewGuid()).dll"

	$sourceCode = @"
	public class HelloWorld {
		public string SayHello() {
			return "Hello World";
		}
		
		public string SayHello(Person person) {
			return string.Format("Hello {0}", person.FirstName);
		}
	}

	public sealed class Person {
		private string _firstName;
		public string FirstName { 
			get { return _firstName; }
			set { _firstName = value; }
		}
		
		private string _lastName;
		public string LastName { 
			get { return _lastName; }
			set { _lastName = value; }
		}
	}
"@

	Add-Type -TypeDefinition $sourceCode #-OutputAssembly $assemblyPath
};

Make-Dll;

$person = New-Object Person -Property @{
	"FirstName" = "Justin";
	"LastName" = "Dearing";
}

Make-Dll;

$hw = New-Object HelloWorld
$Host


$VerbosePreference = 'Continue';
$DebugPreference = 'Continue';
Write-Verbose 'Verbose Message';
Write-Debug 'Debug Message';
Write-Host 'Host Message';
Write-Warning 'Warning Message';
Write-Error 'Error Messaage'

Write-Host "Config File: $([AppDomain]::CurrentDomain.SetupInformation.ConfigurationFile)";
Add-Type -AssemblyName 'System.Configuration'
[System.Configuration.ConfigurationManager]::AppSettings| %{ "$($_): $([System.Configuration.ConfigurationManager]::AppSettings[$_])" }
