using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace LSL.DataUri.Tests
{
    public class DataUriTests
    {
        [TestCase("data:a/type;base64,AEWE", "004584", "a/type")]
        [TestCase("data:base64,AEWE", "004584", "")]
        public void GivenAValidDataUri_ItShouldReturnTheExpectedBytes(string uri, string expectedHexString, string expectedContentType)
        {
            using var scope = new AssertionScope();

            var result = DataUri.Parse(uri);

            Convert.ToHexString(result.Data).Should().Be(expectedHexString);
            result.MimeType.Should().Be(expectedContentType);
        }

        [TestCase("data2:a/type;base64,AEWE")]
        [TestCase("data:AEWE")]
        public void GivenAnInvalidDataUri_ItShouldThrowAnArgumetnException(string uri)
        {
            using var scope = new AssertionScope();

            new Action(() => DataUri.Parse(uri)).Should().Throw<ArgumentException>();
        }        
    }
}
