| | |
| --- | --- |
| **Build** | [![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/KeRNeLith/Here?branch=master&svg=true)](https://ci.appveyor.com/project/KeRNeLith/Here) |
| **Coverage** | <sup>Coveralls</sup> [![Coverage Status](https://coveralls.io/repos/github/KeRNeLith/Here/badge.svg?branch=master)](https://coveralls.io/github/KeRNeLith/Here?branch=master) <sup>SonarQube</sup> [![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=here&metric=coverage)](https://sonarcloud.io/component_measures/metric/coverage/list?id=here) | 
| **Quality** | [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=here&metric=alert_status)](https://sonarcloud.io/dashboard?id=here) | 
| **Nuget** | [![Nuget downloads](https://img.shields.io/nuget/v/here.svg)](https://www.nuget.org/packages/Here) |
| **License** | [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/KeRNeLith/Here/blob/master/LICENSE) |

# Here

## What is **Here**?

**Here** is a .NET library that provide mainly functional features for C#.
It handles and encapsulates the logic of functional based object including the following ones.

- The `Maybe<T>`, also called Option or Monad. This structure encapsulates the concept of having a value or not. This mechanic safely handle a traditional null return.
For more details on `Maybe<T>` usage see the following [tips](src/Here/Maybe/README.md).

- The `Result`. This structure encapsulates treatments results. This provide an improved return state, and also a better error management.
For more details on `Result` usage see the following [tips](src/Here/Result/README.md).

---

## Target

- .NET Standard 1.1+
- .NET Core 1.0+
- .NET Framework 2.0+

---

## Dependencies

**No package dependencies.**

### Notes

- It uses NUnit3 for unit testing (not published).

- The library code is published annotated with JetBrains annotations that are embedded in the library. But they will **not conflict** with any of your referenced packages or project defined attributes as they are **internal** to Here.

---

## Installation

Here is available on [NuGet](https://www.nuget.org/packages/Here)

	PM> Install-Package Here

---