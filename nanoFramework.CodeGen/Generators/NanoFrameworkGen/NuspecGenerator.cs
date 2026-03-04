//
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

using CodeGen.JsonTypes;

namespace CodeGen.Generators.NanoFrameworkGen
{
    class NuspecGenerator : GeneratorBase
    {
        private readonly Quantity _quantity;
        private readonly string _mscorlibNuGetVersion;
        private readonly string _mathNuGetVersion;

        public NuspecGenerator(
            Quantity quantity,
            string mscorlibNuGetVersion,
            string mathNuGetVersion)
        {
            _quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            _mscorlibNuGetVersion = mscorlibNuGetVersion;
            _mathNuGetVersion = mathNuGetVersion;
        }

        public string Generate()
        {
            Writer.WL($@"<?xml version=""1.0"" encoding=""utf-8""?>
<package xmlns=""http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd"">
  <metadata>
    <id>nanoFramework.UnitsNet.{_quantity.Name}</id>
    <version>$version$</version>
    <title>.NET nanoFramework - Units.NET {_quantity.Name}</title>
    <authors>nanoframework</authors>
    <license type=""file"">LICENSE.md</license>
    <projectUrl>https://github.com/nanoframework/nanoFramework.UnitsNet</projectUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>Adds {_quantity.Name} units for Units.NET on .NET nanoFramework. For .NET or .NET Core, use UnitsNet instead.</description>
    <icon>images\logo-128.png</icon>
    <readme>README.md</readme>
    <releaseNotes>
    </releaseNotes>
    <copyright>Copyright (c) .NET Foundation and Contributors</copyright>
    <tags>nanoframework {_quantity.Name.ToLower()} unit units quantity quantities measurement si metric imperial abbreviation abbreviations convert conversion parse immutable</tags>
    <repository type=""git"" url=""https://github.com/nanoframework/nanoFramework.UnitsNet"" />
    <dependencies>
      <group targetFramework="".NETnanoFramework1.0"">
        <dependency id=""nanoFramework.CoreLibrary"" version=""{_mscorlibNuGetVersion}"" />");

            if (NanoFrameworkGenerator.ProjectsRequiringMath.Contains(_quantity.Name))
            {
                Writer.WL($@"
        <dependency id=""nanoFramework.System.Math"" version=""{_mathNuGetVersion}"" />");
            }

            Writer.WL($@"
      </group>
    </dependencies>
  </metadata>
  <files>
    <file src=""..\..\..\README.md"" target="""" />
    <file src=""..\..\..\assets\readme.txt"" target="""" />
    <file src=""..\..\..\LICENSE.md"" target="""" />
    <file src=""..\..\..\UnitsNet\Docs\Images\logo-128.png"" target=""images\"" />
    <file src=""bin\Release\nanoFramework.UnitsNet.{_quantity.Name}.*"" target=""lib\netnano1.0"" />
  </files>
</package>");

            return Writer.ToString();
        }
    }
}
