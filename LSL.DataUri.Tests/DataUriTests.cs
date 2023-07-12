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
        public void DateUri_Parse_GivenAValidDataUri_ItShouldReturnTheExpectedBytes(string uri, string expectedHexString, string expectedContentType)
        {
            using var scope = new AssertionScope();

            var result = DataUri.Parse(uri);

            Convert.ToHexString(result.Data).Should().Be(expectedHexString);
            result.MimeType.Should().Be(expectedContentType);
        }

        [TestCase("data2:a/type;base64,AEWE")]
        [TestCase("data:a/type;base64,not-base-64!")]
        [TestCase("data:AEWE")]
        public void DateUri_Parse_GivenAnInvalidDataUri_ItShouldThrowAnArgumetnException(string uri)
        {
            using var scope = new AssertionScope();

            new Action(() => DataUri.Parse(uri)).Should().Throw<FormatException>();
        }

        [TestCase(new byte[] { 1, 2, 3 }, "a-type", "data:a-type;base64,AQID")]
        [TestCase(new byte[] { 1, 2, 3 }, "", "data:;base64,AQID")]
        public void DateUri_ToString_GivenDataAndAMimeType_ItShouldReturnTheExpectedResult(byte[] data, string mimeType, string expectedResult)
        {
            using var scope = new AssertionScope();

            new DataUri(data, mimeType).ToString().Should().Be(expectedResult);
        }

        [TestCase("data:a/type;base64,AEWE", "004584", "a/type")]
        [TestCase("data:base64,AEWE", "004584", "")]
        public void DateUri_TryParse_GivenAValidDataUri_ItShouldReturnTheExpectedBytes(string uri, string expectedHexString, string expectedContentType)
        {
            using var scope = new AssertionScope();

            var succeeded = DataUri.TryParse(uri, out var result);

            Convert.ToHexString(result.Data).Should().Be(expectedHexString);
            result.MimeType.Should().Be(expectedContentType);
        }

        [TestCase("data2:a/type;base64,AEWE")]
        [TestCase("data:a/type;base64,not-base-64!")]
        [TestCase("data:AEWE")]
        public void DateUri_TryParse_GivenAnInvalidDataUri_ItShouldThrowAnArgumetnException(string uri)
        {
            using var scope = new AssertionScope();

            DataUri.TryParse(uri, out var result).Should().BeFalse();
        }        
    }
}
