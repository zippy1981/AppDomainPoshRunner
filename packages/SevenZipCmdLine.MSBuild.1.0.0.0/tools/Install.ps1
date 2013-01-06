# Copyright (c) 2013 Justin Dearing. All rights reserved.
# Copyright (c) 2012 Adam Ralph. All rights reserved.

param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath "Remove.psm1")

function Append-TextElement($doc, $namespace, $parent, $elementName, $condition, $text)
{
    $element = $doc.CreateElement($elementName, $namespace)
    $element.SetAttribute('Condition', $condition)
    $element.SetAttribute('Text', $text)
    $parent.AppendChild($element)
}

function Append-Property($doc, $namespace, $propertyGroup, $propertyName, $value)
{
    $property = $doc.CreateElement($propertyName, $namespace)
    $property.AppendChild($doc.CreateTextNode($value))
    $propertyGroup.AppendChild($property)
}

# remove content hook from project and delete file
$hookName = "SevenZipCmdLine.MSBuild.ContentHook.txt"
$project.ProjectItems.Item($hookName).Remove()
Split-Path $project.FullName -parent | Join-Path -ChildPath $hookName | Remove-Item

# save removal of content hook and any other unsaved changes to project before we start messing about with project file
$project.Save()

# load project XML
$doc = New-Object System.Xml.XmlDocument
$doc.Load($project.FullName)
$namespace = 'http://schemas.microsoft.com/developer/msbuild/2003'

# remove previous changes - executed here for safety, in case for some reason Uninstall.ps1 hasn't been executed
Remove-Changes $doc $namespace

# add targets file property
$absolutePath = Join-Path $toolsPath "SevenZipCmdLine.MSBuild.targets"
$absoluteUri = New-Object -typename System.Uri -argumentlist $absolutePath
$projectUri = New-Object -typename System.Uri -argumentlist $project.FullName
$relativeUri = $projectUri.MakeRelativeUri($absoluteUri)
$relativePath = [System.URI]::UnescapeDataString($relativeUri.ToString()).Replace([System.IO.Path]::AltDirectorySeparatorChar, [System.IO.Path]::DirectorySeparatorChar)
$propertyGroup = $doc.CreateElement('PropertyGroup', $namespace)
Append-Property $doc $namespace $propertyGroup 'SevenZipCmdLineMSBuildTargetsFile' $relativePath
$doc.Project.AppendChild($propertyGroup)

# add import
$import = $doc.CreateElement('Import', $namespace)
# Adam Ralph added a condition, but I'd rather fail.
# $import.SetAttribute('Condition', "Exists('`$(SevenZipCmdLineMSBuildTargetsFile)')")
$import.SetAttribute('Project', '$(SevenZipCmdLineMSBuildTargetsFile)')
$doc.Project.AppendChild($import)

# save changes
$doc.Save($project.FullName)