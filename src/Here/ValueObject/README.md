# Value Object

The `ValueObject` is an object that represents something whose equality is not based on identity but on value.
Meaning two objects are equal when they have the same value and not necessarily being the same object.
The `ValueObject` provided by **Here** is the base class that encapsulates the boilerplate code required to compare objects on their values rather than their references. 
It gives a common logic to manage the comparison of **immutable** objects. It acts as an useful basis to create comparable/interchangeable objects.
What you just have to do to use it is to put a using like this and inherits from this class:

```csharp
using Here.ValueObjects;
```

## Why Value object?

Because it offers an easy and simple manner to create immutable objects that are comparable/interchangeable in a elegant way.
It factorizes the logic in a single point rather than duplicating this boilerplate code for all new classes you create.

---

## Create a ValueObject

You just have to inherit from `ValueObject` and implement the `GetEqualityElements` method and that's all!

The `GetEqualityElements` is the method that yield all fields that should be taken into account while comparing 2 value objects.
With this method you can also specify how objects are compared by using some custom behaviors.

## Examples

Because it's easier to understand how to create your `ValueObject` by looking at example, following are some dummy classes that help to demonstrate some features.

Simple example:

```csharp
class Address : ValueObject
{
    public string Street { get; }

    public string City { get; }

    public Address(string street, string city)
    {
        Street = street;
        City = city;
    }

    protected override IEnumerable<object> GetEqualityElements()
    {
        // Compare these fields to check value equals
        yield return Street;
        yield return City;
    }
}

// Check
var address1 = new Address("Champs Elysées", "Paris");
var address2 = new Address("Champs Elysées", "Paris");
var address3 = new Address("Montmartre", "Paris");

address1.Equals(address2);    // True
address1.Equals(address3);    // False
```

Example with inheritance:

```csharp
class FullAddress : Address
{
    public string Country { get; }

    public FullAddress(string street, string city, string country)
        : base(street, city)
    {
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityElements()
    {
        // For derived classes
        foreach (object element in base.GetEqualityElements())
            yield return element;

        yield return Country;
    }
}

// Check
var address1 = new Address("Champs Elysées", "Paris");
var address2 = new FullAddress("Champs Elysées", "Paris", "France");
var address3 = new FullAddress("Champs Elysées", "Paris", "France");
var address4 = new FullAddress("Montmartre", "Paris", "France");

address1.Equals(address2);    // False
address2.Equals(address3);    // True
address2.Equals(address4);    // False
```

Example with custom comparison:

```csharp
class Wallet : ValueObject
{
    public string Currency { get; }

    public double Amount { get; }

    public Wallet(string currency, double amount)
    {
        Currency = currency;
        Amount = amount;
    }
            
    protected override IEnumerable<object> GetEqualityElements()
    {
        // To compare data stored in different formats
        yield return Currency.ToLower();
        yield return Math.Round(Amount);
    }
}

// Check
var wallet1 = new Wallet("Eur", 100.25111);
var wallet2 = new Wallet("EUR", 100.25);
var wallet3 = new Wallet("USD", 100.25);

wallet1.Equals(wallet2);    // True
wallet1.Equals(wallet3);    // False
```

Example with structure that contains a collection:

```csharp
class Coin : ValueObject
{
    public enum Value
    {
        OneCent,
        TwoCents,
        FiveCents,
        TenCents,
        TwentyCents,
        FiftyCents,
        OneEuro,
        TwoEuros
    };
    
    public Value CoinValue { get; }

    public Coin(Value value)
    {
        CoinValue = value;
    }

    protected override IEnumerable<object> GetEqualityElements()
    {
        yield return CoinValue;
    }
}

class Wallet : ValueObject
{
    public List<Coin> Coins { get; }

    public Wallet([NotNull] IEnumerable<Coin> coins)
    {
        Coins = coins.ToList();
    }

    protected override IEnumerable<object> GetEqualityElements()
    {
        // To compare lists
        foreach (Coin coin in Coins)
            yield return coin;
    }
}

// Check
var wallet1 = new CoinWallet(new []{ new Coin(Coin.Value.OneEuro), new Coin(Coin.Value.TenCents) });
var wallet2 = new CoinWallet(new []{ new Coin(Coin.Value.OneEuro), new Coin(Coin.Value.TenCents) });
var wallet3 = new CoinWallet(new []{ new Coin(Coin.Value.TwoEuros), new Coin(Coin.Value.TenCents) });

wallet1.Equals(wallet2);    // True
wallet1.Equals(wallet3);    // False
```

Example ignoring fields:

```csharp
class SumObject : ValueObject
{
    public int Operand1 { get; }

    public int Operand2 { get; }

    public int Sum { get; }

    public SumObject(int operand1, int operand2)
    {
        Operand1 = operand1;
        Operand2 = operand2;
        Sum = Operand1 + Operand2;
    }

    protected override IEnumerable<object> GetEqualityElements()
    {
        // Ignore "Sum" field
        yield return Operand1;
        yield return Operand2;
    }
}

// Check
var sumObject1 = new SumObject(1, 2);
var sumObject2 = new SumObject(1, 2);
var sumObject3 = new SumObject(10, 20);

sumObject1.Equals(sumObject2);    // True
sumObject1.Equals(sumObject3);    // False
```