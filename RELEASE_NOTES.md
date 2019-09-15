# Release notes

## What's new in 0.9.0 September 15 2019
### Option:
* Extend support of TryParseEnum to every targets.
* Extend support of NoneIfNullOrSpaces to every targets.
* Add an If treatment with templated result.
* Add NoneIf treatments.
* WARNING: Rename NoneClass to NoneOption.
* Add IfOfDefault and ElseOrDefault extensions.

### Results:
* Add an extension to throw the wrapped exception of a Result if there is one.
* Results structure no longer throw NullPointerException when initialized by default rather than with factory methods.
* Change some OnSuccess parameter to the wrapped value rather than result itself (for consistency).
* Add an OnFailure extensions for results.
* Add factory methods to create results from other failed results.

### Either:
* Fix a crash in the Equals for Either with direct comparison of a left or right value when null.

### Misc:
* Generate a documentation for the library via DocFX.
* Make Option, results, eithers and Unit serializable types.

## What's new in 0.8.0 January 25 2019
### General:
* Library use C# 7.0 features.
* Change target NetStandard 1.1 to NetStandard 1.0.
* Adjust some annotations of library methods.
* Add early throw for parameters that should not be null.
* Add exception documentation on library functions.

### Option:
* Add extensions to Enum, DateTime, DateTimeOffset and Guid.
* Replace methods returning void by Unit.
* Transform conversions from Option to other library types to extensions.
* Transform operations available for Option as extensions into instance methods.

### Results:
* Add extensions OnSuccessOrFailure.
* Update some OnSuccess/OnFailure extensions to return the input result to make possible to chain them.
* Add inputs parameters to lambda of some OnSuccess extensions.
* Transform conversions from Results to other library types to extensions.

### Unit:
* Add an implementation of Unit type.

### Either:
* Add a first implementation Either monad.
  * Operations methods (IfLeft/IfRight/Match/Map, etc.).
  * Linq extensions.
  * Comparison helpers.
  * Conversions to other library types.

---

## What's new in 0.7.0 November 29 2018
### General:
* Clean the library documentation.
* Documentation of the library is now packaged.
* Add support of Source Link.
* Remove dependency to Jetbrains.Annotations, embed the code of some attributes internally.
* The library is ReSharper compliant (use internal annotations recognized by ReSharper).
* Remove target .NET Framework 4.5.2.
* Annotate with "in" all functions parameters that support it.
* WARNING: All existing classes/structs have been moved from their own namespaces to the Here namespace, and extensions to the Here.Extensions namespace.

### Maybe => Option:
* Add extensions to unwrap the value embedded by Maybe&lt;T&gt;.
* Add static helpers to check equality and compare Maybe&lt;T&gt; (allows custom comparer).
* Maybe&lt;T&gt; is equatable with object of the wrapped type (also add a static helper for this).
* Make structure readonly.
* Rename Maybe&lt;T&gt; to Option&lt;T&gt;.

### Results:
* Add extensions to unwrap the value embedded by results structures.
* Add extensions to Flatten results structures.
* Rename SuccessEquals static helpers to SuccessEqual.
* Add static helpers to check equality and compare results structures (allows custom comparer).
* Result structures with value are equatable with object of the wrapped type (also add a static helper for this).
* Add cast methods on Result with value that perform a cast with the "as" operator.
* Add extensions OnSuccess/OnFailure allowing to return an output value other than a result structure.
* Make structures readonly.
* Add factory method to create a result from an exception.

### ValueObject:
* Add a first implementation of a base class for value objects.

---

## What's new in 0.6.1 September 21 2018
### Maybe:
* Add == and != operators to compare a value directly with the wrapped one.
* Add extensions to unwrap the Maybe value.
* Add Exists extension which is equivalent to Any with a predicate.
* Add extension to extract values from an enumerable of Maybe.
* Add extensions to convert a Maybe to Enumerable/Enumerator.
* Add extensions SingleOrNone and ElementAt on IEnumerable.
* Add an explicit Flatten extension.
* Add extensions to perform treatments on Maybe that wrap an IEnumerable (for each, any, where, etc).
* Make Maybe IComparable and IComparable<Maybe>.

### Results:
* Add == and != operators to compare a value directly with the wrapped one.
* Implement equals, == and != operators to compare results with a result of same type.
* WARNING: Remove implicit conversions to boolean that produce unwanted behaviors when trying to compare results.
* Add a Result scope to run a simple action.
* Add a SuccessEquals method for each result type and associated static helpers.
* Make result structures IComparable and IComparable.

---

## What's new in 0.5.0 September 2 2018
### General:
* Extend library compatibility until .NET Framework 2.0.

### Maybe:
* Add implicit conversion to boolean to handle test like a simple boolean.
* Remove true/false operators (replaced by the implicit conversion to boolean).
* Add extension on string: NoneIfNullOrSpace.
* Add extensions on Maybe<string>: NoneIfEmpty and NoneIfNullOrSpace.

### Results:
* Add implicit conversion to boolean to handle test like a simple boolean.
* Add "Ensure" extensions that allow to check a predicate on each Result type.
* Add cast methods on each Result type to create a Result of another type or with a different value type.

---

## What's new in 0.4.0 August 16 2018
### General:
* Update to latest JetBrains annotations package.

### Results:
* Add extensions OnSuccess.
* Add extensions OnFailure.
* Add extensions OnAny.

---

## What's new in 0.3.0 August 2 2018
### General:
* Add missing JetBrains annotations.

### Maybe:
* Add explicit and implicit converters to Result/Result<T>/CustomResult<TError>/Result<T, TError>.

### Results:
* Result can now always embed an Exception (for Warning and Failure).
* Add scopes to safely return a Result of any type.
* Add explicit and implicit converters to Maybe.

---

## What's new in 0.2.0 July 29 2018
### Maybe:
* Little optimizations
* New IfOr and ElseOr extensions.
* Easy conversions between numeric Maybe (ToInt(), ToLong(), ToDouble(), etc.)
* Conversion from numeric Maybe to Maybe<bool>.
* Try Get Value for dictionaries supports null key queries.
* Try Get Value for dictionary<TKey, object> supports value cast to an expected type.

---

## What's new in 0.1.0 July 13 2018
### General:
* Add JetBrains annotations on many methods of the library to clarify them.

### Maybe:
* Supports bitwise and logical AND and OR operators.
* New IfElse extensions.
* New Linq extension (OfType) and simplify usage of Cast too.
* Try parse now are available with custom arguments like those available in .NET Framework.

### Result:
* First implementation of Result.

---

## What's new in 0.0.1 June 29 2018
* First implementation of Maybe (Monad):
  * Parse string to Maybe
  * Maybe Linq implementation like
  * Some useful extensions
