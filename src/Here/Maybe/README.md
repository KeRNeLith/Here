# Maybe

The `Maybe<T>`, also called Option or Monad is a structure that encapsulates the concept of having a value or not. 
It safely handle this mechanic without you have to worry about having a null value somewhere. It does this for you.
What you just have to do to use it is to put a using like this for basic implementation:

```csharp
using Here.Maybes;
```

And this one for extensions:

```csharp
using Here.Maybes.Extensions;
```

## Why Maybe?

Because it offers a safe handling of null values. It also a nice alternative to `Nullable` that only works with value type while `Maybe` handle every types.

---

## Create a maybe

You can create a `Maybe<T>` from any type via the extension `ToMaybe()` or the "constructor".
See examples below:

```csharp
Maybe<string> maybeStr = Maybe<string>.Some("My string");
Maybe<string> maybeStr2 = "My string2".ToMaybe();

// Empty maybe
Maybe<string> emptyMaybeStr = Maybe<string>.None;
Maybe<string> emptyMaybeStr2 = Maybe.None; // Implicit creation
```

It's also possible to use implicit conversion that will fit for calls to API code that your not the owner or simply code you don't want to update.

```csharp
int GetAnInteger()
{
    return 42;
}

// Implicit creation
Maybe<int> maybeInt = GetAnInteger();
```

## Working with Maybe

When you use a Maybe you can access to its value if it has one via the `Value` property. You can check before that it has one via the `HasValue` and `HasNoValue` properties, or directly by checking the Maybe variable as it supports the true and false operators.
But you can also simply use extensions that will really help you to keep a clean and functional code.

```csharp
Maybe<int> maybeInt = 42.ToMaybe();
if (maybeInt.HasValue)
    Console.WriteLine(maybeInt.Value);

// Equivalent to
if (maybeInt)
    Console.WriteLine(maybeInt.Value);
```

### Perform a treatment

```csharp
// Empty string
string emptyString = null;

emptyString.ToMaybe().If(str =>
{
    // Here 'str' is guaranted to be not null. In this case this statement will never be called.
    Console.WriteLine(str);
});

// Not empty string
"Hello world!".ToMaybe().If(str =>
{
    // In this case it will output "Hello world!".
    Console.WriteLine(str);
});
```

Here is an example with the `If` (has value), but there is also the `IfElse` that allows you to handle the case the Maybe has no value.

### Unwrapping value

#### Extensions

After having wrapped a value you can also safely unwrap it. For this you have multiple methods using the `Or` operator.
You will be able unwrap with a default value or a factory method like:

```csharp
// With a wrapped value
Maybe<int> maybeInt = 42.ToMaybe();

int unwrappedValue = maybeInt.Or(12);        // 42
int unwrappedValue2 = maybeInt.Or(() => 12); // 42
int unwrappedValue3 = maybeInt.OrDefault();  // 42
int unwrappedValue3 = maybeInt.Unwrap();     // 42

// With no wrapped value
Maybe<int> emptyMaybeInt = Maybe.None;

int unwrappedValue = emptyMaybeInt.Or(12);        // 12
int unwrappedValue2 = emptyMaybeInt.Or(() => 42); // 42
int unwrappedValue3 = emptyMaybeInt.OrDefault();  // 0
```

You can also consider the case when the maybe is 'None' as an error and use the `OrThrows`:

```csharp
Maybe<int> emptyMaybeInt = Maybe.None;

int unwrappedValue = emptyMaybeInt.OrThrows(new InvalidOperationException()); // Throws
```

#### Comparisons

It is also possible to perform equality comparison directly on the wrapped value through the `Maybe` like showed below:

```csharp
Maybe<int> maybeInt = Maybe<int>.Some(12);
if (maybeInt == 12)
{
    // Do something...
}

// Or with reference types
// This code will never throw a null reference exception as maybe ensure not null value
var testClass = new TestClass();
Maybe<TestClass> maybeClass = Maybe<TestClass>.Some(new TestClass());
if (maybeClass == testClass)
{
    // Do something...
}
```

### Cast value

You can cast the value wrapped by the Maybe in a safe way by using a converter method or an 'as' cast like following:

```csharp
Maybe<int> maybeInt = 42.ToMaybe();
Maybe<float> maybeFloat = maybeInt.Cast(intValue => (float)intValue);

// For reference types
// Let assume that we have a type TestClass and a type SubTestClass that inherits from TestClass
var testObject = new SubTestClass()
Maybe<TestClass> maybeTestClass = Maybe<TestClass>.Some(testObject);
Maybe<SubTestClass> maybeSubTestClass = maybeTestClass.Cast<SubTestClass>();
```

### Enumerable extensions

#### Basic methods

Methods like `FirstOrDefault()` and `LastOrDefault` have their Maybe equivalent that are `FirstOrNone` and `LastOrNone` which respectively return a Maybe with the first or last enumerable element and None if there is not any.

```csharp
var listInts = new List<int> { 1, 2, 3 };

Maybe<int> maybeInt = listInts.FirstOrNone(); // Maybe with value 1
Maybe<int> maybeInt2 = listInts.FirstOrNone(intValue => intValue == 3); // Maybe with value 3
Maybe<int> maybeInt3 = listInts.FirstOrNone(intValue => intValue == 4); // None Maybe

// This is the same for LastOrNone
```

#### Wrapped enumerables

There are also extensions that allow to perform treatments directly on a wrapped enumerable (or other derived collection).

For example you can use ForEach or Where like this, all these methods are suffixed by "Item" or "Items":

```csharp
var listInts = new List<int> { 1, 2, 3 };

Maybe<IList<int>> maybeListInts = listInts;
maybeListInts.ForEachItems((int item) => Console.WriteLine(item));

IEnumerable<int> enumerableInts = maybeListInts.WhereItems((int item) => item >= 2);	// 2 3
```

#### Enumerable to unwrapped values

If you have an enumerable of `Maybe<T>` then you may want to only keep data that really have a value.

For this you can simply extract values via the TODO

### Linq extensions

There is also a support of common Linq extensions like `Any`, `All`, `Contains`, `Select`, `Where`, `ForEach` and `Aggregate`.

See example below:

```csharp
// Dummy examples
Maybe<int> maybeInt = 12.ToMaybe();

if (maybeInt.Any())
{
    bool result = maybeInt.Aggregate(10, (initial, value) => initial + value) // 22
                          .Where(intValue => intValue > 20)                   // Is true
                          .Contains(22);                                      // Is true
}

Maybe<Type> maybeType = typeof(string);
maybeType.Select(type => type.Name)
         .ForEach(name => Console.WriteLine(name));
```

### Lookup and Parsing

Maybe also allows you to safely lookup in a dictionary or parsing of basic types from string. It provides an implementation that fit most common usages.

```csharp
// Lookup in a dictionary
var dictionary = new Dictionary<int, string>
{
    [11] = "string 11",
    [12] = "string 12"
};

Maybe<string> maybeString = dictionary.TryGetValue(11);	// string 11
Maybe<string> maybeString2 = dictionary.TryGetValue(14);	// None maybe

// Parsing
Maybe<int> maybeInt = "12".parseInt(); // 12
Maybe<int> maybeInt2 = "1.5".parseInt(); // None Maybe
Maybe<float> maybeFloat = "1.5".parseFloat(); // 1.5
```

Lookup and parsing are features that can be completed easily if you have a method that match the following delegates:

```csharp
// For TryGet
public delegate bool TryGet<in TInput, TValue>([CanBeNull] TInput input, out TValue value);

// For TryParse
public delegate bool TryParse<in TInput, TValue>([CanBeNull] TInput input, NumberStyles style, IFormatProvider culture, out TValue value);
````

The library provide a default implementation for them:

```csharp
// Try Get
public static Func<TInput, Maybe<TValue>> CreateGet<TInput, TValue>([NotNull] TryGet<TInput, TValue> tryGetFunc)
{
    return input => tryGetFunc(input, out TValue result)
        ? result.ToMaybe()
        : Maybe.None;
}

// TryParse
public static Func<TInput, Maybe<TValue>> CreateParse<TInput, TValue>([NotNull] TryParse<TInput, TValue> tryParseFunc, NumberStyles style, IFormatProvider culture)
{
    return input => tryParseFunc(input, NumberStyles.Any, culture, out TValue result)
        ? result.ToMaybe()
        : Maybe.None;
}
```

And use them for each basic type, but you can also create you own implementation in the same way.

### Numeric Types casts

Each numeric type, which means `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `decimal`, `float` and `double` have support of the conversion to another numeric type and also to `bool`.

This means that you can easily convert `Maybe<TNumeric>` to a `Maybe<TOtherNumeric>` or `Maybe<bool>`. Each `Maybe<TNumeric>` have extensions to do this. Here are some examples:

```csharp
Maybe<double> maybeDouble = Maybe<double>.Some(42.2d);

// Double to bool
Maybe<bool> maybeBool = maybeDouble.ToBool();   // True

// Double to int
Maybe<int> maybeInt = maybeDouble.ToInt();      // 42

// Etc.

// As a consequence you can chain calls like this:
string myString = "51.52";
Maybe<double> maybeDouble = myString.TryParseInt().ToDouble();  // 51
```

### Bridge to Result

It is possible to convert a `Maybe<T>` to a `Result`, `Result<T>`, `CustomResult<TError>` or `Result<T, TError>`.

For the first two conversions, both also support implicit conversion.

```csharp
var maybeInt = Maybe<int>.Some(42);

Result result = maybeInt.ToResult();    // Explicit => Result.OK
Result result = maybeInt;               // Implicit => Result.OK


var emptyMaybeInt = Maybe<int>.None;

Result result = emptyMaybeInt.ToResult();    // Explicit => Result.Fail
Result result = emptyMaybeInt.ToResult("Custom failure message");    // Explicit => Result.Fail
Result result = emptyMaybeInt;               // Implicit => Result.Fail
```