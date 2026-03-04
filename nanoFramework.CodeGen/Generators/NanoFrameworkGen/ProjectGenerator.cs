//
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

using CodeGen.Helpers;
using CodeGen.JsonTypes;

namespace CodeGen.Generators.NanoFrameworkGen
{
    class ProjectGenerator : GeneratorBase
    {
        private readonly Quantity _quantity;
        private readonly NanoFrameworkVersions _versions;

        public ProjectGenerator(Quantity quantity, NanoFrameworkVersions versions)
        {
            _quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            _versions = versions;
        }
        private const string SourceLinkVersion = "8.0.0";

        public string Generate()
        {
            Writer.WL($@"<?xml version=""1.0"" encoding=""utf-8""?>
<Project ToolsVersion=""15.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
  <Import Project=""..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.props"" Condition=""Exists('..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.props')"" />
  <Import Project=""..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.props"" Condition=""Exists('..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.props')"" />
  <Import Project=""..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.props"" Condition=""Exists('..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.props')"" />
  <Import Project=""..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.props"" Condition=""Exists('..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.props')"" />
  <PropertyGroup Label=""Globals"">
    <NanoFrameworkProjectSystemPath>$(MSBuildExtensionsPath)\nanoFramework\v1.0\</NanoFrameworkProjectSystemPath>
  </PropertyGroup>
  <Import Project=""$(NanoFrameworkProjectSystemPath)NFProjectSystem.Default.props"" Condition=""Exists('$(NanoFrameworkProjectSystemPath)NFProjectSystem.Default.props')"" />
  <PropertyGroup>
    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>
    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>
    <ProjectTypeGuids>{{11A8DD76-328B-46DF-9F39-F559912D0360}};{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}</ProjectTypeGuids>
    <ProjectGuid>{HashGuid.ToHashGuid(_quantity.Name):B}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <FileAlignment>512</FileAlignment>
    <RootNamespace>UnitsNet</RootNamespace>
    <AssemblyName>nanoFramework.UnitsNet.{_quantity.Name}</AssemblyName>
    <TargetFrameworkVersion>v1.0</TargetFrameworkVersion>
    <DocumentationFile>bin\$(Configuration)\$(AssemblyName).xml</DocumentationFile>
    <Deterministic>true</Deterministic>
    <DebugType>portable</DebugType>
    <DebugSymbols>true</DebugSymbols>
    <PublishRepositoryUrl>true</PublishRepositoryUrl>
    <EmbedUntrackedSources>true</EmbedUntrackedSources>
    <ContinuousIntegrationBuild Condition=""'$(TF_BUILD)' == 'true'"">true</ContinuousIntegrationBuild>
  </PropertyGroup>
  <PropertyGroup>
    <SignAssembly>true</SignAssembly>
  </PropertyGroup>
  <PropertyGroup>
    <AssemblyOriginatorKeyFile>..\..\..\key.snk</AssemblyOriginatorKeyFile>
  </PropertyGroup>
  <PropertyGroup>
    <DelaySign>false</DelaySign>
  </PropertyGroup>
  <Import Project=""$(NanoFrameworkProjectSystemPath)NFProjectSystem.props"" Condition=""Exists('$(NanoFrameworkProjectSystemPath)NFProjectSystem.props')"" />
  <ItemGroup>
    <Compile Include=""..\Quantities\{_quantity.Name}.g.cs"" Link="".{_quantity.Name}.g.cs"" />
    <Compile Include=""..\Units\{_quantity.Name}Unit.g.cs"" Link=""{_quantity.Name}Unit.g.cs"" />
    <Compile Include=""..\Properties\AssemblyInfo.cs"" Link=""Properties\AssemblyInfo.cs"" />
  </ItemGroup>
  <ItemGroup>
    <Reference Include=""mscorlib, Version={_versions.MscorlibVersion}, Culture=neutral, PublicKeyToken=c07d481e9758c731"">
      <HintPath>..\..\..\packages\nanoFramework.CoreLibrary.{_versions.MscorlibNugetVersion}\lib\mscorlib.dll</HintPath>
      <Private>True</Private>
      <SpecificVersion>False</SpecificVersion>
    </Reference>");

            if (NanoFrameworkGenerator.ProjectsRequiringMath.Contains(_quantity.Name))
            {
                Writer.WL($@"
    <Reference Include=""System.Math, Version={_versions.MathVersion}, Culture=neutral, PublicKeyToken=c07d481e9758c731"">
      <HintPath>..\..\..\packages\nanoFramework.System.Math.{_versions.MathNugetVersion}\lib\System.Math.dll</HintPath>
      <Private>True</Private>
      <SpecificVersion>False</SpecificVersion>
    </Reference>");
            }

            Writer.WL($@"
  </ItemGroup>
  <ItemGroup>
    <None Include=""..\..\..\key.snk"" Link=""key.snk"" />
    <None Include=""packages.config"" />
  </ItemGroup>
  <Import Project=""$(NanoFrameworkProjectSystemPath)NFProjectSystem.CSharp.targets"" Condition=""Exists('$(NanoFrameworkProjectSystemPath)NFProjectSystem.CSharp.targets')"" />
  <ProjectExtensions>
    <ProjectCapabilities>
      <ProjectConfigurationsDeclaredAsItems />
    </ProjectCapabilities>
  </ProjectExtensions>
  <Target Name=""EnsureNuGetPackageBuildImports"" BeforeTargets=""PrepareForBuild"">
    <PropertyGroup>
      <ErrorText>This project references NuGet package(s) that are missing on this computer. Enable NuGet Package Restore to download them.  For more information, see http://go.microsoft.com/fwlink/?LinkID=322105.The missing file is {{0}}.</ErrorText>
    </PropertyGroup>
    <Error Condition=""!Exists('..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.props'))"" />
    <Error Condition=""!Exists('..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.targets')"" Text=""$([System.String]::Format('$(ErrorText)', '..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.targets'))"" />
    <Error Condition=""!Exists('..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.props'))"" />
    <Error Condition=""!Exists('..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.props'))"" />
    <Error Condition=""!Exists('..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.props')"" Text=""$([System.String]::Format('$(ErrorText)', '..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.props'))"" />
  </Target>
  <Import Project=""..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.targets"" Condition=""Exists('..\..\..\packages\Nerdbank.GitVersioning.{_versions.NbgvNugetVersion}\build\Nerdbank.GitVersioning.targets')"" />
  <Import Project=""..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.targets"" Condition=""Exists('..\..\..\packages\Microsoft.Build.Tasks.Git.{SourceLinkVersion}\build\Microsoft.Build.Tasks.Git.targets')"" />
  <Import Project=""..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.targets"" Condition=""Exists('..\..\..\packages\Microsoft.SourceLink.Common.{SourceLinkVersion}\build\Microsoft.SourceLink.Common.targets')"" />
  <Import Project=""..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.targets"" Condition=""Exists('..\..\..\packages\Microsoft.SourceLink.GitHub.{SourceLinkVersion}\build\Microsoft.SourceLink.GitHub.targets')"" />

</Project>");

            return Writer.ToString();
        }
    }
}
