| | |
| --- | --- |
| **Build** | [![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/KeRNeLith/Here?branch=master&svg=true)](https://ci.appveyor.com/project/KeRNeLith/Here) |
| **Coverage** | <sup>Coveralls</sup> [![Coverage Status](https://coveralls.io/repos/github/KeRNeLith/Here/badge.svg?branch=master)](https://coveralls.io/github/KeRNeLith/Here?branch=master) <sup>SonarQube</sup> [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=here&metric=coverage)](https://sonarcloud.io/summary/new_code?id=here) | 
| **Quality** | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=here&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=here) | 
| **Nuget** | [![Nuget status](https://img.shields.io/nuget/v/here.svg)](https://www.nuget.org/packages/Here) |
| **License** | [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/KeRNeLith/Here/blob/master/LICENSE) |

# Here

## What is **Here**?

**Here** is a .NET library that mainly provides functional features for C#.
It handles and encapsulates the logic of functional based objects including the following ones.

- The `Option<T>`. This structure encapsulates the concept of having a value or not. This mechanic safely handles a traditional null return.
For more details on `Option<T>` usage see the following [tips](src/Here/Option/README.md).

- The `Either<TLeft, TRight>`. This structure encapsulates the concept of having a success value (right) or a failure (left). This provides a better management of method returns that can generate error by clarifying the error case.
For more details on `Either<TLeft, TRight>` usage see the following [tips](src/Here/Either/README.md).

- The `Result`. This structure encapsulates treatments results. This provides an improved return state, and also a better error management.
For more details on `Result` usage see the following [tips](src/Here/Result/README.md).

- The `ValueObject`. This structure encapsulates boilerplate code required to compare objects on their values rather than their references. This allows an easy creation of comparable/interchangeable objects.
For more details on `ValueObject` usage see the following [tips](src/Here/ValueObject/README.md).

See the library [documentation](https://kernelith.github.io/Here/).

---

## Targets

- [![.NET Standard](https://img.shields.io/badge/.NET%20Standard-%3E%3D%201.0-blue.svg)](#)
- [![.NET Core](https://img.shields.io/badge/.NET%20Core-%3E%3D%201.0-blue.svg)](#)
- [![.NET Framework](https://img.shields.io/badge/.NET%20Framework-%3E%3D%202.0-blue.svg)](#)

Supports Source Link

---

## Dependencies

**No package dependencies.**

### Notes

- It uses NUnit3 for unit testing (not published).

- The library code is published annotated with JetBrains annotations.

---

## Installation

Here is available on [NuGet](https://www.nuget.org/packages/Here)

    PM> Install-Package Here

---

## Maintainer(s)

[![](https://github.com/KeRNeLith.png?size=50)](https://github.com/KeRNeLith)

---