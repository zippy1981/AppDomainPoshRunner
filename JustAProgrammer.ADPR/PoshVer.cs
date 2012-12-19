namespace JustAProgrammer.ADPR
{
    /// <summary>Representation of the version of PowerShell</summary>
    public enum PoshVer : ushort
    {
        /// <summary>System default</summary>
        /// <remarks>This is the highest version of PowerShell Available.</remarks>
        Default = 0,
        /// <summary>PowerShell 1.0</summary>
        PowerShell1 = 1,
        /// <summary>PowerShell 2.0</summary>
        PowerShell2= 2,
        /// <summary>PowerShell 3.0</summary>
        PowerShell3 = 3
    }
}