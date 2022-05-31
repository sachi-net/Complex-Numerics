# Complex-Library
Complex-Library is .NET 5.0 based reusable library designed perform algebraic arithmetic on complex numeric. Any .NET 5.0 or above application can utilize the functionalities of Complex-Library by referring it in the project dependencies.

## Prerequisites
Currently, Complex-Library dependency is available for local installation only.  
Complex-Library requires [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) (v5.#.#) or [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (v6.#.#).

## Get Started
Add `ComplexLibrary.dll` assembly to project dependencies. 
Refer the content from the code by adding namespace `using ComplexLibrary;` at the top.

## Implementation Overview
Following tables indicate the implementation structure of Complex-Library.

### Fields
|Name|Symbol|Type|Description|
|---|---|---|---|
|`E`|e|`double`|Mathematical constant (Euler's number) e \[value = `2.718282...`\]|
|`Pi`|π|`double`|Mathematical constant π \[value = `3.141593...`\]|

### Properties
|Name|Type|Description|
|---|---|---|
|Argument|`double`|Argument (phase) of this complex number represented in Arg-Secondary (0 ≤ arg(Z) < 2π) convention|
|Imaginary|`double`|Imaginary component of this complex number|
|Modulus|`double`|Modulus (magnitude or radius) of this complex number|
|Real|`double`|Real component of this complex number|

### Enums
`ComplexFormat` enumeration defines the complex number format to be used in string notation.
|Option|Description|
|---|---|
|`Cartesian`|Cartesian format as Z = x + iy|
|`Exponential`|Exponential format as Z = re<sup>iφ</sup>|
|`Polar`|Polar format as Z = r\[Cos(φ) + i.Sin(φ)\]|

`PhaseMode` enumeration defines the method of sign-convention when defining argument.
|Option|Description|
|---|---|
|`ArgPrimary`|Restrict phase in argument of complex number Z within range -π < Arg(Z) ≤ π|
|`ArgSecondary`|Angle phase in argument of complex number Z within range 0 ≤ arg(Z) < 2π|

### Constructors
|Name|Description|
|---|---|
|`Complex(double real, double imaginary)`|Initializes an instance of new Complex object with cartesian (Argand) coordinates with cartisian coordinates as `real` and `imaginary`|
|`Complex(double radius, double argument, [PhaseMode phaseMode])`|Initializes an instance of new Complex object with polar coordinates `radius`, `argument` and `phaseMode` convention.|

### Methods
|Name|Return Type|Description|
|---|---|---|
|`Conjugate()`|`Complex`|Calculate the reciprocal (multiplicative inverse) of this complex number|
|`Equals(Complex)`|`bool`|Evaluate the equality of this complex number with the given complex number|
|`Equals(object)`|`bool`|Evaluate the equality of this complex number with the given object|
|`GetHashCode()`|`int`|Calculate the hashcode of this complex number|
|`Negate()`|`Complex`|Calculate the conjugate of this complex number|
|`Reciprocal()`|`Complex`|Calculate the reciprocal (multiplicative inverse) of this complex number|
|`Sqrt()`|`Complex`|Calculate the square root of this complex number|
|`ToComplex(double)`|`Complex`|Convert double-precision real number to a complex number|
|`ToReal()`|`double`|Convert this Complex to double precision floating-point number|
|`ToString()`|`string`|Convert the complex number into a string notation|
|`ToString(byte)`|`string`|Convert the complex number into a string notation with given decimal precision|
|`ToString(ComplexFormat, [byte])`|`string`|Convert the complex number into the given standard notation with decimal precision|

### Operators
|Name|Symbol|Operands|Description|
|---|---|---|---|
|Add|+|`Complex x`, `Complex y`|Add two complex numbers|
|Add|+|`Complex x`, `double n`|Add complex number to a real number|
|Add|+|`double n`, `Complex x`|Add real number to complex number|
|Conjugate|~|`Complex x`|Calculate the conjugate of this complex number|
|Divide|/|`Complex x`, `Complex y`|Divide complex number by another|
|Divide|/|`Complex x`, `double n`|Divide complex number by a real number|
|Equals|==|`Complex x`, `Complex y`|Evaluate the equality of two complex numbers|
|Multiply|* |`Complex x`, `Complex y`|Multiply two complex numbers|
|Multiply|* |`Complex x`, `double n`|Multiply a real number by complex number|
|Multiply|* |`double n`, `Complex x`|Multiply complex number by a real number|
|Not Equals|!=|`Complex x`, `Complex y`|Evaluate the inequality of two complex numbers|
|Power|^ |`Complex x`, `int e`|Raise the complex number to the power of provided real number|
|Subtract|-|`Complex x`, `Complex y`|Subtract two complex numbers|
|Subtract|-|`Complex x`, `double n`|Subtract a real number from complex number|
|Subtract|-|`double n`, `Complex x`|Subtract complex number from a real number|

## Usage
Following section describe the general usage of `ComplexLibrary` to perform complex arithmetic operations.

### Initialize New Complex number
To initialize a complex number `z` with cartesian coordinates such that `z = 5 -3i`,
```C#
Complex z = new(1, -3);
```

To initialize a complex number `z` with polar coordinates such that `radius` is `2` and `argument` (angle) is `240°` (the argument should be defined with radians instead of degrees),  
Using  `-π < Arg(Z) ≤ π` convention or `PhaseMode.ArgPrimary`
```C#
Complex z = new(2, -2 * Complex.Pi / 3, PhaseMode.ArgPrimary);
```

Using  `0 ≤ arg(Z) < 2π` convention or `PhaseMode.ArgSecondary`
```C#
Complex z = new(2, 4 * Complex.Pi / 3, PhaseMode.ArgSecondary);
```
The constructor throws `ArgumentException` exception when the `argument` parameter is out of the range specified by the `phaseMode` parameter.

### Properties of Complex Number
Complex number object has four accessible properties such that the `Real` component, `Imaginary` component, `Modulus` and `Argument` values. These values can be read as follows.
```C#
Complex z = (2, -3);
double Re = z.Real;
double Im = z.Imaginary;
double Mod = z.Modulus;
double Arg = z.Argument;
```
Note that the `Argument` is represented according to the `PhaseMode.ArgSecondary` convention with range of `0 ≤ Argument < 2π`.

### Represent Complex Number
A complex number can be represented as a `string` using `ToString()` functions. This function has several overloads to support different notation formats and decimal precisions.
```C#
Complex z = new(2, -3);
Complex w = new(2, Complex.Pi / 3, PhaseMode.ArgPrimary);

string result1 = z.ToString(); // (2, -3)
string result2 = w.ToString(4); // (1, 1.7320)
```
To represent in cartesian notation,
```C#
string result = new Complex(2, -3).ToString(ComplexFormat.Cartesian); 
// Represent as 2 - 3i
```

To represent in polar notation,
```C#
string result = new Complex(2, Complex.Pi / 3, PhaseMode.ArgPrimary).ToString(ComplexFormat.Polar); 
// Represent as 2[Cos(1.0472) + i.Sin(1.0472)]
```

To represent in exponential notation,
```C#
string result = new Complex(2, Complex.Pi / 3, PhaseMode.ArgPrimary).ToString(ComplexFormat.Exponential); 
// Represent as 2e1.0472i
```

### Complex Number Conversion
A complex number can be converted to a double-precison real number if there is no imaginary component. The function throws `InvalidCastException` exception when try to convert a complex number having a non-zero imaginary component.
```C#
Complex z = new(-5, 0);
Complex w = new(1, -1);
double value1 = z.ToReal();
double value2 = w.ToReal(); // This throws an exception
```

A double precision real number can be converted to equivalent complex number using following `static` function.
```C#
double k = 3;
Complex z = Complex.ToComplex(k);
string result = z.ToString(); // (3, 0)
```

### Complex Conjugate
The conjugate pair of complex number `z = x + iy` is represented by `~z = x - iy`. The conjugate of complex number `z` can be calculated as follows.
```C#
Complex z = new(2, 5);
Complex conjugate;
conjugate = z.Conjugate()
// Or
conjugate = ~z;
```
This operation will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Complex Negation
Negation of complex number represents the negative version of itself. The negation of complex number `z` can be calculated as follows.
```C#
Complex z = new(5, -3);
Complex negate;
negate = z.Negate()
// Or
negate = -z;
```
This operation will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Complex Reciprocal Division
The reciprocal of complex number represents the multiplicative inverse of itself. For a complex number `z`, the reciprocal is represented by `1/z`. The reciprocal of complex number `z` can be calculated as follows.
```C#
Complex z = new(-2, 3);
Complex reciprocal = z.Reciprocal();
```
This operation will throw `ComplexNotInitializedException` exception when required complex parameters are `null` and `DivideByZeroException` exception if the modulus of the complex number is zero which causes the zero division.

## Complex Operations
Complex object can perform following arithmetic operations.

### Addition
To add two complex numbers `x` and `y`,
```C#
Complex x = new(1, 5);
Complex y = new(2, Complex.Pi / 3, PhaseMode.ArgPrimary);
Complex result = x + y;
```

To add double-precision real number `n` to the complex number `z` and then value `3`,
```C#
double k = 2;
double result = z + k + 3;
```

Addition will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Subtraction
To subtract complex numbers `y` from `x`,
```C#
Complex x = new(1, 5);
Complex y = new(2, Complex.Pi / 3, PhaseMode.ArgPrimary);
Complex result = x - y;
```

To substract double-precision real number `n` from the complex number `z` and then value `3`,
```C#
double k = 2;
double result = z - k - 3;
```

Subtraction will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Multiplication
To multiply complex numbers `x` by `y`,
```C#
Complex x = new(1, 5);
Complex y = new(2, Complex.Pi / 3, PhaseMode.ArgPrimary);
Complex result = x * y;
```

To multiply complex numbers `x` by a double-precision real number `n`,
```C#
Complex x = new(1, 5);
double n = 3;
Complex result = x * n;
```

Multiplication will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Division
To divide complex numbers `x` by `y`,
```C#
Complex x = new(1, 5);
Complex y = new(2, Complex.Pi / 3, PhaseMode.ArgPrimary);
Complex result = x / y;
```
This will throws `DivideByZeroException` exception when the `modulus` of `y` is `0`.

To multiply complex numbers `x` by a double-precision real number `n`,
```C#
Complex x = new(1, 5);
double n = 3;
Complex result = x / n;
```
This will throw `DivideByZeroException` exception when the value of `n` is `0`.
Division will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Power or Exponent
To raise the complex numbers `x` to the power of integer real number `e`,
```C#
Complex x = new(1, 5);
int e = 4;
Complex result = x ^ e;
```
Power will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

### Square Root
To calculate the square root of complex number `x`,
```C#
Complex x = new(1, 5);
Complex result = x.Sqrt();
```
Square root operation will throw `ComplexNotInitializedException` exception when required complex parameters are `null`.

## Complex Comparison
Two complex numbers can be compared using following comparison methods.

### Equality
To evaluate the equality of two complex numbers,
```C#
Complex w = new(1, 3);
bool result;

result = w.Reciprocal() == (~w / Math.Pow(w.Modulus, 2)); // True
// Or
result = w.Reciprocal().Equals(~w / Math.Pow(w.Modulus, 2)); // True

result = w == ~w; // False
// Or
result = w.Equals(~w); // False
```

### Inequality
To evaluate the inequality of two complex numbers,
```C#
Complex w = new(1, 3);
bool result;

result = w != ~w; // True
// Or
result = !w.Equals(~w); // True
```
