# Option

The `Option<T>` is a structure that encapsulates the concept of having a value or not. 
It safely handles this mechanic without you have to worry about having a null value somewhere. It does this for you.
What you just have to do to use it is to put a using like this for basic implementation:

```csharp
using Here;
```

And this one for extensions:

```csharp
using Here.Extensions;
```

## Why Option?

Because it offers a safe handling of null values. It is also a nice alternative to `Nullable` that only works with value type while `Option` handle every types.

---

## Create an option

You can create an `Option<T>` from any type via the extension `ToOption()` or the "constructor".
See examples below:

```csharp
Option<string> optionStr = Option<string>.Some("My string");
Option<string> optionStr2 = "My string2".ToOption();
Option<string> optionStr3 = "My string2";     // Implicit conversion

// Empty option
Option<string> emptyOptionStr = Option<string>.None;
Option<string> emptyOptionStr2 = Option.None;  // Implicit creation
```

It is also possible to use implicit conversion that will fit for calls to API code that your not the owner or simply code you don't want to update.

```csharp
int GetAnInteger()
{
    return 42;
}

// Implicit creation
Option<int> optionInt = GetAnInteger();
```

## Working with Option

When you use an Option you can access to its value if it has one via the `Value` property. You can check before that it has one via the `HasValue` and/or `HasNoValue` properties, or directly by checking the Option variable as it supports the true and false operators.
But you can also simply use extensions that will really help you to keep a clean and functional code.

```csharp
Option<int> optionInt = 42.ToOption();
if (optionInt.HasValue)
    Console.WriteLine(optionInt.Value);

// Equivalent to
if (optionInt)
    Console.WriteLine(optionInt.Value);
```

### Perform a treatment

```csharp
// Empty string
string emptyString = null;

emptyString.ToOption().If(str =>
{
    // Here 'str' is guaranteed to be not null. In this case this statement will never be called.
    Console.WriteLine(str);
});

// Not empty string
"Hello world!".ToOption().If(str =>
{
    // In this case it will output "Hello world!".
    Console.WriteLine(str);
});
```

Here is an example with the `If` (has value), but there is also the `IfElse` that allows you to handle the case the Option has no value, and many others.

### Unwrapping value

#### Extensions

After having wrapped a value you can also safely unwrap it. For this you have multiple methods, for example by using the `Or` operator.
You will be able unwrap with a default value or a factory method like:

```csharp
// With a wrapped value
Option<int> optionInt = 42.ToOption();

int unwrappedValue = optionInt.Or(12);        // 42
int unwrappedValue2 = optionInt.Or(() => 12); // 42
int unwrappedValue3 = optionInt.OrDefault();  // 42
int unwrappedValue3 = optionInt.Unwrap();     // 42

// With no wrapped value
Option<int> emptyOptionInt = Option.None;

int unwrappedValue = emptyOptionInt.Or(12);        // 12
int unwrappedValue2 = emptyOptionInt.Or(() => 42); // 42
int unwrappedValue3 = emptyOptionInt.OrDefault();  // 0
```

You can also consider the case when the option is 'None' as an error and use the `OrThrows`:

```csharp
Option<int> emptyOptionInt = Option.None;

int unwrappedValue = emptyOptionInt.OrThrows(new InvalidOperationException()); // Throws
```

#### Comparisons

It is also possible to perform equality comparison directly on the wrapped value through the `Option` like shown below:

```csharp
Option<int> optionInt = Option<int>.Some(12);
if (optionInt == 12)
{
    // Do something...
}

// Or with reference types
// This code will never throw a null reference exception as option ensure not null value
var testClass = new TestClass();
Option<TestClass> optionClass = Option<TestClass>.Some(new TestClass());
if (optionClass == testClass)
{
    // Do something...
}
```

### Cast value

You can cast the value wrapped by the Option in a safe way by using a converter method or an 'as' cast like following:

```csharp
// For basic types
Option<int> optionInt = 42.ToOption();
Option<float> optionFloat = optionInt.Cast(intValue => (float)intValue);

// For reference types
// Let assume that we have a type TestClass and a type SubTestClass that inherits from TestClass
var testObject = new SubTestClass()
Option<TestClass> optionTestClass = Option<TestClass>.Some(testObject);
Option<SubTestClass> optionSubTestClass = optionTestClass.Cast<SubTestClass>();
```

### Enumerable extensions

#### Basic methods

Methods like `FirstOrDefault()` and `LastOrDefault` have their Option equivalent that are `FirstOrNone` and `LastOrNone` which respectively return an Option with the first or last enumerable element and None if there is not any.

```csharp
var listInts = new List<int> { 1, 2, 3 };

Option<int> optionInt = listInts.FirstOrNone(); // Option with value 1
Option<int> optionInt2 = listInts.FirstOrNone(intValue => intValue == 3); // Option with value 3
Option<int> optionInt3 = listInts.FirstOrNone(intValue => intValue == 4); // None Option

// This is the same for LastOrNone
```

#### Wrapped enumerables

There are also extensions that allow to perform treatments directly on a wrapped enumerable (or other derived collection).

For example you can use ForEach or Where like this, all these methods are suffixed by "Item" or "Items":

```csharp
var listInts = new List<int> { 1, 2, 3 };

Option<IList<int>> optionListInts = listInts;
optionListInts.ForEachItems((int item) => Console.WriteLine(item));

IEnumerable<int> enumerableInts = optionListInts.WhereItems((int item) => item >= 2);	// 2 3
```

#### Enumerable to unwrapped values

If you have an enumerable of `Option<T>` then you may want to only keep data that really have a value.

For this you can simply extract values via the `ExtractValues` extension. Note that you can also generate a List, Dictionary in the same way.

```csharp
IEnumerable<Option<float>> GetData()
{
    // Do something and yield results
}

IEnumerable<float> relevantValues = GetData().ExtractValues();
float[] relevantArray = GetData().ToArray();
List<float> relevantList = GetData().ToList();
Dictionary<string, float> relevantDictionary = GetData().ToDictionary((float val) => val.ToString());
```

### Linq extensions

There is also a support of common Linq extensions like `Any`, `All`, `Contains`, `Select`, `Where`, `ForEach` and `Aggregate`.

See example below:

```csharp
// Dummy examples
Option<int> optionInt = 12.ToOption();

if (optionInt.Any())
{
    bool result = optionInt.Aggregate(10, (initial, value) => initial + value) // 22
                          .Where(intValue => intValue > 20)                   // Is true
                          .Contains(22);                                      // Is true
}

Option<Type> optionType = typeof(string);
optionType.Select(type => type.Name)
         .ForEach(name => Console.WriteLine(name));
```

### Lookup and Parsing

Option also allows you to safely lookup in a dictionary or parsing of basic types from string. It provides an implementation that fit most common usages.

```csharp
// Lookup in a dictionary
var dictionary = new Dictionary<int, string>
{
    [11] = "string 11",
    [12] = "string 12"
};

Option<string> optionString = dictionary.TryGetValue(11);   // string 11
Option<string> optionString2 = dictionary.TryGetValue(14);  // None option

// Parsing
Option<int> optionInt = "12".parseInt();   // 12
Option<int> optionInt2 = "1.5".parseInt(); // None Option
Option<float> optionFloat = "1.5".parseFloat(); // 1.5
```

Lookup and parsing are features that can be completed easily if you have a method that match the following delegates:

```csharp
// For TryGet
public delegate bool TryGet<in TInput, TValue>([CanBeNull] TInput input, out TValue value);

// For TryParse
public delegate bool TryParse<TValue>([CanBeNull] string input, NumberStyles style, IFormatProvider culture, out TValue value);
````

The library provide a default implementation for them:

```csharp
// Try Get
public static Func<TInput, Option<TValue>> CreateGet<TInput, TValue>([NotNull] TryGet<TInput, TValue> tryGetFunc)
{
    return input => tryGetFunc(input, out TValue result)
        ? result.ToOption()
        : Option.None;
}

// TryParse
public static Func<string, Option<TValue>> CreateParse<TValue>([NotNull] TryParse<TValue> tryParseFunc, NumberStyles style, IFormatProvider culture)
{
    return input => tryParseFunc(input, NumberStyles.Any, culture, out TValue result)
        ? result.ToOption()
        : Option.None;
}
```

And use them for each basic type, but you can also create you own implementation in the same way.

### Numeric Types casts

Each numeric type, which means `byte`, `sbyte`, `short`, `ushort`, `int`, `uint`, `long`, `ulong`, `decimal`, `float` and `double` have support of the conversion to another numeric type and also to `bool`.

This means that you can easily convert `Option<TNumeric>` to an `Option<TOtherNumeric>` or `Option<bool>`. Each `Option<TNumeric>` have extensions to do this. Here are some examples:

```csharp
Option<double> optionDouble = Option<double>.Some(42.2d);

// Double to bool
Option<bool> optionBool = optionDouble.ToBool();   // True

// Double to int
Option<int> optionInt = optionDouble.ToInt();      // 42

// Etc.

// As a consequence you can chain calls like this:
string myString = "51.52";
Option<double> optionDouble = myString.TryParseInt().ToDouble();  // 51
```

### Bridge to Result

It is possible to convert an `Option<T>` to a `Result`, `Result<T>`, `CustomResult<TError>` or `Result<T, TError>`.

For the first two conversions, both also support implicit conversion.

```csharp
var optionInt = Option<int>.Some(42);

Result result = optionInt.ToResult();    // Explicit => Result.OK
Result result = optionInt;               // Implicit => Result.OK


var emptyOptionInt = Option<int>.None;

Result result = emptyOptionInt.ToResult();    // Explicit => Result.Fail
Result result = emptyOptionInt.ToResult("Custom failure message");    // Explicit => Result.Fail
Result result = emptyOptionInt;               // Implicit => Result.Fail
```