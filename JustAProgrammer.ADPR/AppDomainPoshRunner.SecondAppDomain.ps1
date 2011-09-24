#This test adapted from: http://www.eggheadcafe.com/microsoft/Powershell/30766269/how-to-load--unload-dll.aspx

$assemblyPath = "$($ENV:TEMP)\$([Guid]::NewGuid()).dll"

$cSharpCodeProvider = New-Object Microsoft.CSharp.CSharpCodeProvider

$parameters = New-Object System.CodeDom.Compiler.CompilerParameters
$parameters.GenerateInMemory = $true
$parameters.GenerateExecutable = $false
$parameters.OutputAssembly = $assemblyPath
$cSharpCodeProvider.CompileAssemblyFromSource($parameters, @"
public static class HelloWorld {
	public static string SayHello() {
		return "Hello World";
	}
}
"@) > $null;

Add-Type -Path $assemblyPath;
[HelloWorld]::SayHello();