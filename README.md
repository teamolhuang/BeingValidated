# BeingValidated
---
A small tool, mainly for input object (request model, etc.) validation, I personally wrote and used in business projects. 
Its main purpose is to have validation codes more uniformed, and can be also used to construct a main method running multiple minor methods.

## Usage
---
To start validating with an IBeingValidated, use `.StartValidate()` or `.StartValidateElements()`.
For example, let's say we already defined a class `GetUserRequest`, and receives it on an endpoint we're writing. We then can validate the input like this

```csharp
GetUserRequest input = ...; // accepted from client
GetUserResponse response = new();

bool isValid = input.StartValidate()
                    .Validate(i => i.UserId > 0,
                              i => response.AddError($"ID ({i}) not valid!"))
                    .Validate(i => i.MustProvide != null,
                              _ => response.AddError("MustProvide must be provided!")
                    .IsValid();
```

There is also `ValidateAsync()` for running async methods.

```csharp
bool isValid = await input.StartValidate()
                    .Validate(i => i.UserId > 0,
                              i => response.AddError($"ID ({i}) not valid!"))
                    .ValidateAsync(async i => await dbContext.User.AnyAsync(u => u.Id == i.UserId),
                                         i => response.AddError($"ID ({i}) not found!",
                                         e => response.AddError($"DB Query failed: {e.Message}")
                    .IsValid();
```

A `Validate()`, or `ValidateAsync()` provides 3 parameters, listed in the order of examples above:
* Validation method
* OnFail - method to execute when validation failed (optional)
* OnException - method to execute when validation throws (optional)

## Usage - enumerable
---
To validate an enumerable, you can either use the `.StartValidate()` above to validate the emuerable object, or use `.StartValidateElements()` to validate every element.

Let's say we defined a `GetUsersList`, and there's a `IEnumerable<int> IdList` in it.
```csharp
GetUsersRequest input = ...; // accepted from client
GetUsersResponse response = new();

bool isValid = input.IdList.StartValidateElements()
                    .Validate(i => i > 0,
                              i => response.AddError($"ID ({i}) not valid!"))
                    .IsValid();
```

Say there are 5 integers in the list, and 3 of them are invalid - `i > 0` will be called 5 times, and `AddError()` will be called 3 times.

`ValidateAsync()` also supports it.
