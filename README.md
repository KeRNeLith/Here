[![AppVeyor Build Status](https://ci.appveyor.com/api/projects/status/github/KeRNeLith/Here?branch=master&svg=true)](https://ci.appveyor.com/project/KeRNeLith/Here)

# Here

## What is **Here**?

**Here** is a .NET library that provide mainly functional features for C#.
It handles and encapsulates the logic of functional based object including the following ones.

- The `Maybe<T>`, also called Option or Monad. This structure encapsulates the concept of having a value or not. This mechanic safely handle a traditional null return.
For more details on `Maybe<T>` usage see the following [tips](src/Maybe/README.md).

---

## Target

- C# Framework 4.5

---

## Dependencies

- JetBrains.Annotations
- NUnit (for unit testing)

---

## How to contribute?

The project use the solution generator Sharpmake to generate the solution and project files (.csproj).

To start working on Here, you simply have to run the `generateSolution.bat` at repository root. It will create all needed files.

If you need more informations about Sharpmake, consult the [GitHub repository](https://github.com/ubisoftinc/Sharpmake).