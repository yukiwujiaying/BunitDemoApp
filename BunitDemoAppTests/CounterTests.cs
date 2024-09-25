using Bunit;
using BunitDemoApp.Components;
using FluentAssertions;

namespace BunitDemoAppTests
{
    public class CounterTests : TestContext
    {
        [Fact]
        public void Counter_Should_Start_With_Zero()
        {
            // Arrange
            var cut = RenderComponent<Counter>();

            // Act
            var count = cut.Find("h1").TextContent;

            // Assert
            count.Should().Be("Current Count: 0");
        }

        [Fact]
        public void Counter_Should_Increment_When_Button_Clicked()
        {
            // Arrange
            var cut = RenderComponent<Counter>();

            // Act
            cut.Find("button").Click();

            // Assert
            var count = cut.Find("h1").TextContent;
            count.Should().Be("Current Count: 1");
        }
    }
}