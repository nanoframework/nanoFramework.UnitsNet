//
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

namespace CodeGen.Generators
{
    /// <summary>
    /// NanoFramework dependency versions.
    /// </summary>
    /// <param name="MscorlibVersion">mscorlib assembly version in nanoFramework.CoreLibrary nuget.</param>
    /// <param name="MscorlibNugetVersion">Nuget version of nanoFramework.CoreLibrary.</param>
    /// <param name="MathVersion">System.Math assembly version in nanoFramework.System.Math nuget.</param>
    /// <param name="MathNugetVersion">Nuget version of nanoFramework.System.Math.</param>
    /// <param name="NbgvNugetVersion">Nuget version of Nerdbank.GitVersioning used for the generated projects.</param>
    public record NanoFrameworkVersions(string MscorlibVersion, string MscorlibNugetVersion, string MathVersion, string MathNugetVersion, string NbgvNugetVersion)
    {

    }
}
