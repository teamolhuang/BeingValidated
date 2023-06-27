using System;
using System.Threading.Tasks;
using BeingValidated;
using NUnit.Framework;

namespace BeingValidated_Tests
{
    public class BeingValidatedEnumerableTests
    {
        [Test]
        public void Validate_WillCheckOnExceptionIsNull_AndSetToDefaultBehaviour()
        {
            // Make sure that when OnException is passed as null,
            // exceptions are thrown instead of silently ignored.

            // Arrange
            string testString = "Test test";
            int exceptionContent = new Random().Next();

            // Act
            Exception actual = Assert.Catch<Exception>(() => testString.StartValidateElements()
                .Validate(_ => throw new Exception(exceptionContent.ToString())));

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<Exception>(actual);
            Assert.AreEqual(exceptionContent.ToString(), actual.Message);
        }

        private Task ValidateAsync_DummyValidationMethod(string message)
        {
            throw new Exception(message);
        }

        [Test]
        public async Task ValidateAsync_WillCheckOnExceptionIsNull_AndSetToDefaultBehaviour()
        {
            // Make sure that when OnException is passed as null,
            // exceptions are thrown instead of silently ignored.

            // Arrange
            string testString = "Test test";
            int exceptionContent = new Random().Next();

            // Act
            Exception actual = Assert.CatchAsync<Exception>(async () => await testString.StartValidateElements()
                .ValidateAsync(async _ => await ValidateAsync_DummyValidationMethod(exceptionContent.ToString())));

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<Exception>(actual);
            Assert.AreEqual(exceptionContent.ToString(), actual.Message);
        }
    }
}