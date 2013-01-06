# Copyright (c) 2013 Justin Dearing. All rights reserved.
# Copyright (c) Adam Ralph. All rights reserved.

function Remove-Changes
{
    param(
        [parameter(Position = 0, Mandatory = $true)]
        [System.Xml.XmlDocument]$doc,
        
        [parameter(Position = 1, Mandatory = $true)]
        [string]$namespace
    )

    $properties = Select-Xml "//msb:Project/msb:PropertyGroup/msb:SevenZipCmdLineMSBuildTargetsFile" $doc -Namespace @{msb = $namespace}
    if ($properties)
    {
        foreach ($property in $properties)
        {
            $propertyGroup = $property.Node.ParentNode
            $propertyGroup.RemoveChild($property.Node)
            if (!$propertyGroup.HasChildNodes)
            {
                $propertyGroup.ParentNode.RemoveChild($propertyGroup)
            }
        }
    }


    # remove imports
    $imports = Select-Xml "//msb:Project/msb:Import[contains(@Project,'SevenZipCmdLineMSBuild')]" $doc -Namespace @{msb = $namespace}
    if ($imports)
    {
        foreach ($import in $imports)
        {
            $import.Node.ParentNode.RemoveChild($import.Node)
        }
    }
}