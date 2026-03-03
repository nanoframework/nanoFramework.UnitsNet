//
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//

namespace CodeGen.Generators.NanoFrameworkGen
{
    class PropertyGenerator : GeneratorBase
    {
        public string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.W($@"using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(""UnitsNet"")]
[assembly: AssemblyDescription(""Get all the common units of measurement and the conversions between them. It is light-weight and thoroughly tested."")]
[assembly: AssemblyCompany(""nanoFramework Contributors"")]
[assembly: AssemblyProduct(""nanoFramework UnitsNet"")]
[assembly: AssemblyCopyright(""Copyright (c) .NET Foundation and Contributors"")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

");
            return Writer.ToString();
        }
    }
}
