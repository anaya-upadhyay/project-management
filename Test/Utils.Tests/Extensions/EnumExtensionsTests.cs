using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Utils.Extensions;

namespace ProjectManagement.Utils.Tests.Extensions
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void Should_MapToDefault_When_WrongString()
        {
            var expected = "fake".ToEnum(FakeEnum.Default);

            expected.Should().Be(FakeEnum.Default);
        }

        [TestMethod]
        public void Should_MapToDefault_When_EmptyString()
        {
            var expected = "".ToEnum(FakeEnum.Default);

            expected.Should().Be(FakeEnum.Default);
        }

        [TestMethod]
        public void Should_MapToDefault_When_Null()
        {
            var expected = default(string).ToEnum(FakeEnum.Default);

            expected.Should().Be(FakeEnum.Default);
        }

        [TestMethod]
        public void Should_MapToCorrectValue_When_Valid()
        {
            var expected = "firstvalue".ToEnum(FakeEnum.Default);

            expected.Should().Be(FakeEnum.FirstValue);

        }
    }
}
