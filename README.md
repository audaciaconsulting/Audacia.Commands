# Audacia.Commands.Validation
#### **NOTE**: This library is intended to make validation commands easier, but you can use it for any class object.

## Pre-Installation
Make sure you have setup the Audacia VSTS nuget registry.

## Installation
```powershell
# installation with nuget
Install-Package Audacia.Commands
```

## Example usage:
```csharp
using Audacia.Commands.Validation;
...
var model = new ValidationModel<FooPostCommand>();

if(!model.IsModelNull())
{
     model.Property(m => m.Name)
          .IsRequired()
          .HasMaxLength(25);

     model.Property(m => m.SomeProperty)
          .Property(c => c.SomeChildProperty)
          .MustBeGreaterThan(3);
}

return await model.ToCommandResultAsync();
```

---

## IValidationModel
A validation model is a collection of validation results for provided class properties and validation logic.

### Properties
- **AllErrors** `IEnumerable<string>`
All validation errors regardless of property.

- **ModelErrors** `IDictionary<string, IEnumerable<string>>`
Key value pairs of Property Name and Validation Errors, 
Any errors against the Model uses a blank string as the Property Name.

- **IsValid** `bool`
Is true if there are no errors.

- **ModelName** `string`
User friendly model name.

### Methods
- **IsModelNull** `bool IsModelNull()`
Checks if the model is null,
Adds a validation error when null.

- **Property** `ValidationProperty Property(Expression<Func<TModel, TProperty>> property, string displayName);`
Creates a validation property from which you can fluently attach validation extensions.

- **AddModelError** `void AddModelError(string propertyName, string errorMessage)`
Adds a validation error for a property.

- **ToCommandResultAsync** `Task<CommandResult> ToCommandResultAsync()`
Appends any validation errors to the command result.

---

## Validation Property
A validation property is a model to validate a single property.

### Properties
- **ModelName** `string`
User friendly model name.

- **PropertyName** `string`
Name of the property.

- **DisplayName** `string`
User friendly property name.

- **Value** `TProperty`
Property value.

### Methods
- **AddError** `void AddError(string message)`
Adds a validation error to the property.

- **Property** `Property(Expression<Func<TProperty, TChild>> property, string displayName)`
Creates a child validation property on a property object.

---

## Extensions Methods

- **AlreadyExists** `AlreadyExists(bool exists)`
Adds a model error explaining the value already exists.
This only adds a message, you need to provide the logic for if your unique value exists or not.

- **IsRequired** `IsRequired()`
Validates that the property has been filled in.

- **IsRequired** `IsRequired()` (collections only)
Validates that there is at least one element on a collection.

- **HasMaxLength** `HasMaxLength(int length)` (strings only)
Validates that the property is not longer than the max length.

- **HasMaxLength** `HasMaxLength(int length)` (collections only)
Validates that the collection is not longer than the max length.

- **HasMinLength** `HasMinLength(int length)` (strings only)
Validates that the property is not shorter than the min length.

- **HasMinLength** `HasMinLength(int length)` (collections only)
Validates that the collection is not shorter than the min length.

- **MatchesPattern** `MatchesPattern(string pattern, params string[] conditions)` (strings only)
Validates that the property matches a given regex pattern.

- **MustBeAValidId** `MustBeAValidId()` (int?)
Validates that the Id is not null or less than zero.

- **MustBeGreaterThan** `MustBeGreaterThan(TProperty number)`
Validates that the property is greater than the given value. (IComparable)

- **MustBeGreaterThanOrEqualTo** `MustBeGreaterThanOrEqualTo(TProperty number)` (IComparable)
Validates that the property is greater than or equal to the given value.

- **MustBeLessThan** `MustBeLessThan(TProperty number)` (IComparable)
Validates that the property is less than the given value.

- **MustBeLessThanOrEqualTo** `MustBeLessThanOrEqualTo(TProperty number)` (IComparable)
Validates that the property is less than or equal to the given value.
