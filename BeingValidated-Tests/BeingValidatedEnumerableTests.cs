using System;
using System.Threading.Tasks;
using BeingValidated;
using NUnit.Framework;

namespace BeingValidated_Tests
{
    public class BeingValidatedEnumerableTests
    {
        [Test]
        public async Task Validate_WillCheckOnExceptionIsNull_AndSetToDefaultBehaviour()
        {
            // Make sure that when OnException is passed as null,
            // exceptions are thrown instead of silently ignored.

            // Arrange
            string testString = "I am funny!";
            int exceptionContent = new Random().Next();

            // Act
            Exception actual = Assert.Catch<Exception>(() => testString.StartValidateElements()
                .Validate(s => throw new Exception(exceptionContent.ToString())));

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOf<Exception>(actual);
            Assert.AreEqual(exceptionContent.ToString(), actual.Message);
        }
    }
}