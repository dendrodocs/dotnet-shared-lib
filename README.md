# DendroDocs.Shared

[![Nuget][NUGET_BADGE]][NUGET_FEED] [![Coverage Status](https://coveralls.io/repos/github/dendrodocs/dotnet-shared-lib/badge.svg?branch=main)](https://coveralls.io/github/dendrodocs/dotnet-shared-lib?branch=main)

**DendroDocs.Shared** is a shared library used across multiple components of the DendroDocs ecosystem.
It provides common utilities, abstractions, and extensions that are essential for the functionality of tools like [DendroDocs.Tool](https://github.com/dendrodocs/dotnet-tool) and other .NET-based projects using DendroDocs.

## Features

* Provides common code that can be leveraged by other tools in the DendroDocs ecosystem to ensure consistency and reduce duplication.
* *Includes helper methods and extensions that simplify common tasks like parsing, JSON handling, and model transformations.
* Contains shared data models used by the DendroDocs analyzers and documentation generators.

## Prerequisites

.NET 8.0 SDK or newer.

## Installation

To use **DendroDocs.Shared** in your project, install it as a NuGet package:

```shell
dotnet add package DendroDocs.Shared
```

## Example usage:

```csharp
using DendroDocs.Extensions;
using DendroDocs.Json;
using Newtonsoft.Json;

var types = new List<TypeDescription>();

// more code

var serializerSettings = JsonDefaults.SerializerSettings();
var result = JsonConvert.SerializeObject(types.OrderBy(t => t.FullName), serializerSettings);
```

# The DendroDocs Ecosystem

**DendroDocs.Shared** is a crucial part of the broader DendroDocs ecosystem.
Explore [DendroDocs](https://github.com/dendrodocs) to find more tools, libraries, and documentation resources that help you bridge the gap between your code and its documentation.

## LivingDocumentation

This shared library consolidates the following libraries previously part of [Living Documentation](https://github.com/eNeRGy164/LivingDocumentation):

* LivingDocumentation.Descriptions
* LivingDocumentation.Extensions
* LivingDocumentation.Abstractions
* LivingDocumentation.Statements

These libraries have been combined and restructured for better modularity and ease of use in the DendroDocs ecosystem.

## Contributing

Contributions are welcome! Please feel free to create [issues](https://github.com/dendrodocs/dotnet-shared-lib/issues) or [pull requests](https://github.com/dendrodocs/dotnet-shared-lib/pulls).

## License

This project is licensed under the [MIT License](./LICENSE).

[NUGET_BADGE]: https://img.shields.io/nuget/v/DendroDocs.Shared.svg?style=plastic
[NUGET_FEED]: https://www.nuget.org/packages/DendroDocs.Shared/
