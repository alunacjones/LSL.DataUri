[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-datauri.svg)](https://ci.appveyor.com/project/alunacjones/lsl-datauri)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.DataUri)](https://coveralls.io/github/alunacjones/LSL.DataUri)
[![NuGet](https://img.shields.io/nuget/v/LSL.DataUri.svg)](https://www.nuget.org/packages/LSL.DataUri/)

# LSL.DataUri

A quick and easy data uri parser

## Quickstart

```csharp
using LSL.DataUri;
...
var result = DataUri.Parse("data:a/type;base64,AEWE");

// result.Data contains the byte array of the data
// result.MimeType contains the mime-type of the content. In this case "a/type"

var asAString = result.ToString();

// the result of to string will provide a data URI string of "data:a/type;base64,AEWE"

var uri = new DataUri(new byte[] { 1, 2, 3 }, "a-type").ToString();

// The uri will have a value of "data:a-type;base64,AQID"
```
