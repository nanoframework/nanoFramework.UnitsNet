[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.md) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md)
[![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

# Welcome to Units.NET for .NET **nanoFramework**

## About

This library brings **[Units.NET](https://github.com/angularsen/UnitsNet)** — the popular strongly typed units of measurement library — to the [.NET nanoFramework](https://www.nanoframework.net/) platform. It is a fork of the original [Units.NET](https://github.com/angularsen/UnitsNet) project, adapted and maintained specifically for embedded and IoT devices running .NET nanoFramework.

Add strongly typed quantities to your nanoFramework code and get merrily on with your life.
No more magic constants, no more second-guessing the unit of parameters and variables — even on microcontrollers.

### Acknowledgements

**Units.NET** was created and is maintained by [Andreas Gullberg Larsen](https://github.com/angularsen). This nanoFramework port would not be possible without his excellent work on the original library. A huge thank you to Andreas and all the contributors to the Units.NET project!

> **If you are targeting full .NET** (.NET Standard, .NET 6/8/9/10 or .NET Framework), please use the original [UnitsNet NuGet package](https://www.nuget.org/packages/UnitsNet) instead. This repository is specifically tailored for the constraints and capabilities of .NET nanoFramework.

## Features

- **Strongly typed quantities** — `Length`, `Mass`, `Temperature`, `Pressure`, `ElectricCurrent`, and [130+ more](nanoFramework.UnitsNet/GeneratedCode) quantity types.
- **Unit conversions** — convert between units of the same quantity with ease.
- **Lightweight** — each quantity is delivered as an individual NuGet package, so you only include what you need on your constrained device.
- **Generated from the same unit definitions** as the original Units.NET, ensuring consistency and correctness.

## Usage

### Construct quantities and convert units

```csharp
using UnitsNet;

// Construct a length
Length meter = Length.FromMeters(1);

// Convert to other units
double cm = meter.Centimeters;         // 100
double feet = meter.Feet;              // 3.28084
double inches = meter.Inches;          // 39.3701
```

### Use strongly typed quantities to avoid conversion mistakes

```csharp
using UnitsNet;

// Pass quantity types instead of raw values — communicate intent clearly
string PrintSensorReading(Temperature temp)
{
    return $"Temperature: {temp.DegreesCelsius:F1} °C ({temp.DegreesFahrenheit:F1} °F)";
}

// Construct from sensor reading
Temperature reading = Temperature.FromDegreesCelsius(23.5);
string output = PrintSensorReading(reading);
// "Temperature: 23.5 °C (74.3 °F)"
```

### Work with mass and other quantities

```csharp
using UnitsNet;

Mass package = Mass.FromKilograms(1.5);
double grams = package.Grams;           // 1500
double pounds = package.Pounds;         // 3.30693
double ounces = package.Ounces;         // 52.9109
```

### Pressure, Speed, and more

```csharp
using UnitsNet;

// Atmospheric pressure
Pressure atm = Pressure.FromAtmospheres(1);
double pascals = atm.Pascals;            // 101325
double psi = atm.PoundsForcePerSquareInch; // 14.6959

// Vehicle speed
Speed speed = Speed.FromKilometersPerHour(120);
double mph = speed.MilesPerHour;         // 74.5645
double ms = speed.MetersPerSecond;       // 33.3333

// Electric current from a sensor
ElectricCurrent current = ElectricCurrent.FromAmperes(0.5);
double mA = current.Milliamperes;        // 500
```

### Typical IoT / Embedded scenario

```csharp
using UnitsNet;

// Read raw sensor values and wrap them in strongly typed quantities
Temperature ambientTemp = Temperature.FromDegreesCelsius(ReadTemperatureSensor());
RelativeHumidity humidity = RelativeHumidity.FromPercent(ReadHumiditySensor());
Pressure barometric = Pressure.FromHectopascals(ReadBarometricSensor());

// Now you can safely pass these around without mixing up units
Debug.WriteLine($"Ambient: {ambientTemp.DegreesCelsius:F1} °C");
Debug.WriteLine($"Humidity: {humidity.Percent:F0} %");
Debug.WriteLine($"Pressure: {barometric.Hectopascals:F1} hPa ({barometric.Millibars:F1} mbar)");
```

## Units.NET on other platforms

The same strongly typed units are available on other platforms, all based on the same [unit definitions](https://github.com/angularsen/UnitsNet/tree/master/Common/UnitDefinitions):

| Language                   | Name        | Package                                                              | Repository                                            | Maintainers  |
|----------------------------|-------------|----------------------------------------------------------------------|-------------------------------------------------------|--------------|
| C# (.NET)                  | UnitsNet    | [NuGet](https://www.nuget.org/packages/UnitsNet)                     | [GitHub](https://github.com/angularsen/UnitsNet)      | @angularsen  |
| JavaScript /<br>TypeScript | unitsnet-js | [npm](https://www.npmjs.com/package/unitsnet-js)                     | [GitHub](https://github.com/haimkastner/unitsnet-js)  | @haimkastner |
| Python                     | unitsnet-py | [pypi](https://pypi.org/project/unitsnet-py)                         | [GitHub](https://github.com/haimkastner/unitsnet-py)  | @haimkastner |
| Golang                     | unitsnet-go | [pkg.go.dev](https://pkg.go.dev/github.com/haimkastner/unitsnet-go)  | [GitHub](https://github.com/haimkastner/unitsnet-go)  | @haimkastner |

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Units.NET library is licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
