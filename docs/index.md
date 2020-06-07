# Here documentation

## Badges

| | |
| --- | --- |
| **Build** | [![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/KeRNeLith/Here?branch=master&svg=true)](https://ci.appveyor.com/project/KeRNeLith/Here) |
| **Coverage** | <sup>Coveralls</sup> [![Coverage Status](https://coveralls.io/repos/github/KeRNeLith/Here/badge.svg?branch=master)](https://coveralls.io/github/KeRNeLith/Here?branch=master) <sup>SonarQube</sup> [![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=here&metric=coverage)](https://sonarcloud.io/component_measures/metric/coverage/list?id=here) | 
| **Quality** | [![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=here&metric=alert_status)](https://sonarcloud.io/dashboard?id=here) | 
| **Nuget** | [![Nuget status](https://img.shields.io/nuget/v/here.svg)](https://www.nuget.org/packages/Here) |
| **License** | [![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/KeRNeLith/Here/blob/master/LICENSE) |

## Introduction

**Here** is a .NET library that mainly provides functional features for C#.
It handles and encapsulates the logic of functional based objects including the following ones.

- The `Option<T>`. This structure encapsulates the concept of having a value or not. This mechanic safely handles a traditional null return.
For more details on `Option<T>` usage see the following [tips](documentation/tutorials/option.md).

- The `Either<TLeft, TRight>`. This structure encapsulates the concept of having a success value (right) or a failure (left). This provides a better management of method returns that can generate error by clarifying the error case.
For more details on `Either<TLeft, TRight>` usage see the following [tips](documentation/tutorials/either.md).

- The `Result`. This structure encapsulates treatments results. This provides an improved return state, and also a better error management.
For more details on `Result` usage see the following [tips](documentation/tutorials/results.md).

- The `ValueObject`. This structure encapsulates boilerplate code required to compare objects on their values rather than their references. This allows an easy creation of comparable/interchangeable objects.
For more details on `ValueObject` usage see the following [tips](documentation/tutorials/value-object.md).

You can find library sources on [GitHub](https://github.com/KeRNeLith/Here).

## Targets

- .NET Standard 1.0+
- .NET Core 1.0+
- .NET Framework 2.0+

Supports Source Link

## Dependencies

**No package dependencies.**

## Installation

Here is available on [NuGet](https://www.nuget.org/packages/Here)

	PM> Install-Package Here

<img src="images/here_logo.png" width="128" height="128" style="display: block; margin-left: auto; margin-right: auto" />