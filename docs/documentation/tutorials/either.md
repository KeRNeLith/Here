# Either

The `Either<TLeft, TRight>` is an object that represents a value that can be one of two possible types.
It is composed of a "Left" or a "Right" value (either). Generally the left value is considered as the error state and the right as the good one.
As a consequence `Either<TLeft, TRight>` in left state acts as an `Option<T>` in a None state, but the `Either<TLeft, TRight>` have an additional information on the error encountered.
Where an `Option<T>` will only cancel operations being executed while in None state, the `Either<TLeft, TRight>` will help more by allowing to get more details on the error root cause.

What you just have to do to use it is to put a using like this for basic implementation:

```csharp
using Here;
```

And this one for extensions:

```csharp
using Here.Extensions;
```

## Why Either?

Because it's a structure that will improve the error management by clearly making a distinction between normal and error cases.
You will have to handle both cases in a more natural and simple way.

---

## Create an Either

You can create an `Either<TLeft, TRight>` from any type via the extension `ToEither()` or the "constructors".
See examples below:

```csharp
// Left <=> error state
Either<string, int> eitherLeft = Either.Left<string, int>("Error message");
Either<string, int> eitherLeft = Either.Left("Error message");
Either<string, int> eitherLeft = "Error message".ToEither<string, int>();

// Right <=> good state
Either<string, int> eitherRight = Either.Right<string, int>(12);
Either<string, int> eitherRight = Either.Right(42);
Either<string, int> eitherRight = 12.ToEither<string, int>();

// Empty either
Either<string, int> eitherNone = Either<string, int>.None;
```

Note that there are also EitherRight<TRight> and EitherLeft<TLeft> structures that can be intermediate structures to later create a full Either<TLeft, TRight> structure.

## Working with Either

When you use an Either you can access to its right value (`RightValue` property), or its left value (`LeftValue` property) depending on the. You can check before that it has one or the other value (or neither) via the `IsRight` and/or `IsLeft` properties.
But you can also simply use extensions that will really help you to keep a clean and functional code.

```csharp
Either<string, int> either = Either.Left<string, int>("Error message");
if (either.IsLeft && !either.IsNone)
    Console.WriteLine(either.LeftValue);

Either<string, int> either = Either.Right<string, int>(42);
if (either.IsRight && !either.IsNone)
    Console.WriteLine(either.RightValue);

```

### Perform a treatment

// Following just a few quick examples:

```csharp
"Error message".ToEither<string, int>().IfLeft(leftValue =>
{
    // Here 'leftValue' is guaranteed to be not null is left state.
    Console.WriteLine(leftValue);
});

42.ToEither<string, int>().IfLeft(leftValue =>
{
    // Not called
    Console.WriteLine(leftValue);
});

42.ToEither<string, int>().OnSuccessOrFailure(
    rightValue =>
    {
        // Called
        Console.WriteLine(rightValue);
    },
    leftValue =>
    {
        // Not called
        Console.WriteLine(leftValue);
    });
```

Here are some examples with the `IfLeft`, `OnSuccessOrFailure`, but there are many others.

### Unwrapping value

#### Extensions

After having wrapped a value you can also safely unwrap it. For this you have multiple methods, for example by using the `RightOr` or `LeftOr` operators.
You will be able unwrap with a default value or a factory method like:

```csharp
// With a wrapped value
Either<string, int> eitherStrInt = 42.ToEither<string, int>();

int unwrappedValue = eitherStrInt.RightOr(15);        // 42
int unwrappedValue2 = eitherStrInt.RightOr(() => 12); // 42
int unwrappedValue3 = eitherStrInt.RightOrDefault();  // 42

Either<string, int> eitherStrInt = "Error".ToEither<string, int>();

int unwrappedValue = eitherStrInt.RightOr(15);        // 15
int unwrappedValue2 = eitherStrInt.RightOr(() => 12); // 12
int unwrappedValue3 = eitherStrInt.RightOrDefault();  // 0

// There are equivalent for Left.
```

#### Comparisons

It is also possible to perform equality comparison directly on the wrapped value through the `Option` like shown below:

```csharp
Either<string, int> eitherStrInt = 12.ToEither<string, int>();
if (eitherStrInt == 12)
{
    // Do something...
}
```

### Linq extensions

There is also a support of common Linq extensions like `Any`, `All`, `Contains`, `Select`, `Where`, `ForEach` and `Aggregate`.

See example below:

```csharp
// Dummy examples
Either<string, int> eitherStrInt = 12.ToEither<string, int>();

if (eitherStrInt.Any())
{
    // code
}

int result = eitherStrInt.Aggregate(10, (initial, value) => initial + value); // 22

bool result = eitherStrInt.Where(intValue => intValue > 10) // Is true
                          .Contains(12);                    // Is true

Either<string, Type> eitherStrType = typeof(string).ToEither<string, Type>();
eitherStrType.Select(type => type.Name)
             .ForEach(name => Console.WriteLine(name));
```

### Bridge to Option

It is possible to convert an `Either<TLeft, TRight>` to an `Option<TRight>`.

```csharp
Either<string, int> eitherStrInt = Either.Right(12);
Option<int> optionInt = eitherStrInt.ToOption();    // optionInt = 12

Either<string, int> eitherStrInt = Either.Left("Error");
Option<int> optionInt = eitherStrInt.ToOption();    // optionInt is empty

Either<string, int> eitherStrInt = Either<string, int>.None;
Option<int> optionInt = eitherStrInt.ToOption();    // optionInt is empty
```

### Bridge to Result

It is possible to convert an `Either<TLeft, TRight>` to a `Result`, `Result<TRight>`, `CustomResult<TLeft>` or `Result<TRight, TLeft>`.

```csharp
// Result without custom error type
Either<string, int> eitherStrInt = Either.Right(12);
Result<int> resultInt = eitherStrInt.ToValueResult();    // resultInt = 12

Either<string, int> eitherStrInt = Either.Left("Error");
Result<int> resultInt = eitherStrInt.ToValueResult();    // resultInt.IsFailure

Either<string, int> eitherStrInt = Either<string, int>.None;
Result<int> resultInt = eitherStrInt.ToValueResult();    // resultInt.IsFailure

// Result with custom error type
Either<string, int> eitherStrInt = Either.Right(12);
Result<int, string> resultInt = eitherStrInt.ToValueCustomResult();    // resultInt = 12

Either<string, int> eitherStrInt = Either.Left("Error");
Result<int, string> resultInt = eitherStrInt.ToValueCustomResult();    // resultInt.IsFailure

Either<string, int> eitherStrInt = Either<string, int>.None;
Result<int, string> resultInt = eitherStrInt.ToValueCustomResult();    // throw InvalidOperationException
```