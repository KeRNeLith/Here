[![Nuget downloads](https://img.shields.io/nuget/v/here.svg)](https://www.nuget.org/packages/Here)
[![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/KeRNeLith/Here?branch=master&svg=true)](https://ci.appveyor.com/project/KeRNeLith/Here)
[![GitHub license](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/KeRNeLith/Here/blob/master/LICENSE)

# Here

## What is **Here**?

**Here** is a .NET library that provide mainly functional features for C#.
It handles and encapsulates the logic of functional based object including the following ones.

- The `Maybe<T>`, also called Option or Monad. This structure encapsulates the concept of having a value or not. This mechanic safely handle a traditional null return.
For more details on `Maybe<T>` usage see the following [tips](src/Here/Maybe/README.md).

---

## Target

- .NET Standard 1.0
- .NET Framework 4.5.2

---

## Dependencies

- JetBrains.Annotations
- NUnit (for unit testing)

---

## Installation

Here is available on [NuGet](https://www.nuget.org/packages/Here)

	PM> Install-Package Here

---