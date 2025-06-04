# DendroDocs.Shared

**DendroDocs.Shared** is a shared library used across multiple components of the DendroDocs ecosystem.
It provides common utilities, abstractions, and extensions that are essential for the functionality of tools like [DendroDocs.Tool](https://github.com/dendrodocs/dotnet-tool) and other .NET-based projects using DendroDocs.

## Features

- **Data Models**: Comprehensive data models for representing .NET code structure including types, methods, properties, fields, and documentation comments
- **Code Analysis**: Tools for parsing and representing .NET code elements with support for modifiers, attributes, and inheritance
- **JSON Serialization**: Optimized JSON serialization utilities with custom converters for efficient data exchange
- **String Extensions**: Helper methods for namespace and class name manipulation
- **Statement Representations**: Models for control flow statements like if/else, switch, and foreach
- **Documentation Parsing**: XML documentation comment parsing with support for all standard tags

## Installation

To use **DendroDocs.Shared** in your project, install it as a NuGet package:

```shell
dotnet add package DendroDocs.Shared
```

## Example usage

```csharp
using DendroDocs;
using DendroDocs.Extensions;
using DendroDocs.Json;
using Newtonsoft.Json;

// Working with type descriptions
var types = new List<TypeDescription>();

// more code

var result = JsonConvert.SerializeObject(types.OrderBy(t => t.FullName), serializerSettings);
```

## Library Components

### Data Models (`DendroDocs` namespace)

The library provides comprehensive data models for representing .NET code structure:

- **`TypeDescription`**: Represents classes, interfaces, structs, enums, and delegates with their members
- **`MethodDescription`**: Represents methods with parameters, return types, and method body statements  
- **`PropertyDescription`**: Represents properties with getters and setters
- **`FieldDescription`**: Represents fields and constants
- **`ConstructorDescription`**: Represents class constructors
- **`EventDescription`**: Represents events and event handlers
- **`AttributeDescription`**: Represents attributes applied to code elements
- **`DocumentationCommentsDescription`**: Represents parsed XML documentation comments

### String Extensions (`DendroDocs.Extensions` namespace)

Utility methods for working with fully qualified type names:

- **`ClassName()`**: Extracts the class name from a fully qualified name
- **`Namespace()`**: Extracts the namespace from a fully qualified name  
- **`NamespaceParts()`**: Splits a namespace into its component parts

### JSON Utilities (`DendroDocs.Json` namespace)

Optimized JSON serialization for DendroDocs data models:

- **`JsonDefaults`**: Provides pre-configured settings for both Newtonsoft.Json and System.Text.Json
- **`SkipEmptyCollectionsContractResolver`**: Custom contract resolver to optimize JSON output
- **`ConcreteTypeConverter`**: Handles polymorphic type serialization

### Statement Models (`DendroDocs` namespace)

Represents control flow and code statements:

- **`Statement`**: Base class for all statement types
- **`If`** / **`IfElseSection`**: Conditional statements
- **`Switch`** / **`SwitchSection`**: Switch statements  
- **`ForEach`**: Iteration statements
- **`InvocationDescription`**: Method and property invocations
- **`AssignmentDescription`**: Variable assignments
- **`ReturnDescription`**: Return statements

## The DendroDocs Ecosystem

**DendroDocs.Shared** is a crucial part of the broader DendroDocs ecosystem.  
Explore [DendroDocs](https://github.com/dendrodocs) to find more tools, libraries, and documentation resources that help you bridge the gap between your code and its documentation.

## Contributing

Contributions are welcome! Please feel free to create [issues](https://github.com/dendrodocs/dotnet-shared-lib/issues) or [pull requests](https://github.com/dendrodocs/dotnet-shared-lib/pulls).

## License

This project is licensed under the MIT License.
