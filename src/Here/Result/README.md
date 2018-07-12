# Result

The `Result` is a structure that encapsulates treatments results. 
It provide an enhanced return state for methods that improve the code semantic and allow to react based on this state.
This can be useful when trying to implement a clear and simple error handling.
What you just have to do to use it is to put a using like this for basic implementation:

```csharp
using Here.Results;
```

And this one for extensions:

```csharp
using Here.Results.Extensions;
```

## Why Result?

Because it offers a better handling of methods returns. It can be a nice alternative to traditional booleans or return code.

---

## Create a result

### Construction

Result offers 3 different possibilities which are `Ok`, `Warn` and `Fail`. 
The `Ok` and `Warn` can be used as a result of an operation that succeed either without error or with a warning that is not critical.
Whereas the `Fail` correspond to an operation failed. So when you asked to create a `Result` you have to determine which of these option match your case.

There are 4 types of `Result`:
- A simple `Result`: string message for error and no value
- `Result<T>`: string message for error and has a value
- `CustomResult<TError>`: TError error object and no value
- `Result<T, TError>`: TError error object and has a value

With these possibilities you should be able to handle all cases.
Note that every time you use a warning or an error you will be asked to set a mandatory message because it gives feedback on what went wrong during a treatment.

See below for some examples:

```csharp
// Simple result
Result simpleResult = Result.Ok();
simpleResult = Result.Warn("Your Warning message.");
simpleResult = Result.Fail("Your ERROR message.");

// Result with value
Result<int> resultValue = Result.Ok(42);
resultValue = Result.Warn(12, "Your Warning message.");
resultValue = Result.Fail<int>("Your ERROR message.");

// Result without value but custom error object
CustomResult<Exception> customResult = Result.CustomOk<Exception>();
customResult = Result.CustomWarn<Exception>("Your Warning message.");
customResult = Result.CustomFail("Your ERROR message.", new InvalidOperationException("Treatment leads to an error."));

// Result with value and allows custom error object
Result<int, Exception> customResultValue = Result.Ok<int, Exception>(42);
customResultValue = Result.Warn<int, Exception>(12, "Your Warning message.");
customResultValue = Result.Fail<int, Exception>("Your ERROR message.", new InvalidOperationException("Treatment leads to an error."));
```

### Implicit conversion

When you have a result with additional data it is always possible to implicitly convert it to a result with less data.
For example if you have a `Result<T>` (with result value), you can convert it to a simple `Result`. 
Note that implicit conversions work regardless of the kind of result (`Ok`, `Warn` or `Fail`).

See below for allowed implicit conversions:

```csharp
// All examples use an "Ok" result but it works the same for other "Warn" and "Fail"

// Result<T> => Result
Result<int> resultValue = Result.Ok(42);
Result simpleResult = resultValue;


// CustomResult<TError> => Result
CustomResult<Exception> customResult = Result.CustomOk<Exception>();
Result simpleResult = customResult;


// Result<T, TError> => Result
Result<int, Exception> customResultValue = Result.Ok<int, Exception>(42);
Result simpleResult = customResultValue;

// Result<T, TError> => CustomResult<TError>
Result<int, Exception> customResultValue = Result.Ok<int, Exception>(42);
CustomResult<Exception> customResult = customResultValue;

// Result<T, TError> => Result<T>
Result<int, Exception> customResultValue = Result.Ok<int, Exception>(42);
Result<int> resultValue = customResultValue;
```

## Working with a Result

A result at least implements the `IResult` interface which corresponds to 3 flags (`IsSuccess`, `IsWarning` or `IsFailure`) and a message.

If you create an `Ok` or a `Warn` result, both will end in a success result. The difference is that the `Ok` doesn't require a message while warning yes.
So the only to have a failed result is obviously to use the `Fail` construction, and in this case, it also requires a message.

The necessity to have a message for warnings and errors is motivated by the need to force the developer of a treatment to explain error cases.

Then you have `IResult<T>` and `IResultError<TError>` that respectively provide a `Value` and a custom `Error`.