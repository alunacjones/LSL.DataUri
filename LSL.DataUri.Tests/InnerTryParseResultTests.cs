using System;
using FluentAssertions;
using NUnit.Framework;

namespace LSL.DataUri.Tests
{
    public class InnerTryParseResultTests
    {
        [Test]
        public void Equals_GivenAResultAndItIsComparedToAnotherInstanceThatIsSetupTheSame_ThenItShouldReturnTrue()
        {
            new InnerTryParseResult(true, string.Empty)
             .Equals(new InnerTryParseResult(true, string.Empty))
             .Should()
             .BeTrue();
        }

        [TestCase(false, "my-string")]
        [TestCase(true, "my-string")]
        public void Equals_GivenAResultAndItIsComparedToAnotherInstanceThatIsSetupDifferently_ThenItShouldReturnFalse(bool success, string error)
        {
            new InnerTryParseResult(true, string.Empty)
             .Equals(new InnerTryParseResult(success, error))
             .Should()
             .BeFalse();
        }

        [Test]
        public void Equals_GivenAResultAndItIsComparedToNull_ThenItShouldReturnFalse()
        {
            new InnerTryParseResult(true, string.Empty)
                .Equals(null)
                .Should()
                .BeFalse();
        }

        [Test]
        public void GetHashCode_GivenAResult_ItShouldProduceTheExpectedHashCode()
        {
            new Action(() => new InnerTryParseResult(true, string.Empty).GetHashCode())
                .Should()
                .NotThrow();
        }
    }
}
