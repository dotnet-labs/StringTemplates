# How to Create String Templates in CSharp

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I3I63W4OK)

![string template](./string-template.png)

## [Medium Article](https://codeburst.io/formatting-strings-using-templates-in-c-ba74ca53c07f)

C# has a feature, String Interpolation, to format strings in a flexible and readable way. The following example is usually being used when we have data beforehand then pass data to the template string.

```csharp
var name = "April";
var dob = new DateTime(2000, 4, 10);
Console.WriteLine($"Her name is {name} and her birthday is on {dob:MM/dd/yyyy}, which is in {dob:MMMM}.");
// Console Output:
// Her name is April and her birthday is on 04/10/2000, which is in April.
```

In some scenarios, we want to format certain strings following predefined templates. The string can be, for example, a log message, an alert, an Email template, and so on. Then the String Interpolation doesn't work in these cases.

There are NuGet packages (e.g., [Nustache](https://github.com/jdiamond/Nustache/), [Stubble](https://github.com/StubbleOrg/Stubble), [mustache#](https://github.com/jehugaleahsa/mustache-sharp)) for rendering templates by substituting parameters that are wrapped in specific syntaxes with desired values. Those libraries work like view engines and are able to compile templates with complex logic.

In this article, instead of using those libraries, I will show you two simple ways to substitute parameters with values in a string template.

## `string.Format()`

The most common way to format strings is using the `string.Format()` method. This method takes a template which contains indexes for parameters and takes a list of objects for stubbing out indexed placeholders. Let's take a look at the following example.

```csharp
const string template = "Her name is {0} and her birthday is on {1:MM/dd/yyyy}, which is in {1:MMMM}.";
var s = string.Format(template, "April", new DateTime(2000, 4, 10);
Console.WriteLine(s);
// Console Output:
// Her name is April and her birthday is on 04/10/2000, which is in April.

Console.WriteLine(template, "April", new DateTime(2000, 4, 10));
// Console Output:
// Her name is April and her birthday is on 04/10/2000, which is in April.
```

## Template Modeling

In case when a string template contains many parameters, we probably want to use named parameters instead of indexes to improve the readability. Then we need to utilize template syntax like mustache or handlebar. Rather than installing a third-party library, we can simply implement something on our own.

Let's take a look at the following example.

```csharp
var s = new MyString("April", new DateTime(2000, 4, 10));
Console.WriteLine(s);

public class MyString
{
    private const string Template = @"Her name is {name} and her birthday is on {dob}, which is in {month}.";
    private readonly Dictionary<string, string> _parameters = new();

    public MyString(string name, DateTime dob)
    {
        // validate parameters before assignments

        _parameters.Add(@"{name}", name);
        _parameters.Add(@"{dob}", $"{dob:MM/dd/yyyy}");
        _parameters.Add(@"{month}", $"{dob:MMMM}");
    }

    public override string ToString()
    {
        return _parameters.Aggregate(Template, (s, kv) => s.Replace(kv.Key, kv.Value));
    }
}
```

In the code snippet above, we defined a class `MyString` to model the construction and rendering of the defined string template. In the `MyString` class, a private dictionary `_parameters` is used to store the string placeholders and their values. The constructor can be served as a validator for parameters so that they are ensured to be correct or have some fallback values, and can be served as value transformer so that each template segment can be replaced with a transformed value based on business logic. Lastly, the public `ToString()` method simply replaces all template segment with stored values.

## License

Feel free to use the code in this repository as it is under MIT license.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I3I63W4OK)
