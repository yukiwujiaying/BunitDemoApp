using Bunit;
using Bunit.TestDoubles;
using BunitDemoApp.Components.Pages;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BunitDemoApp.Tests
{
    public class ProfileTests : TestContext
    {
        [Fact]
        public void Profile_Should_Render_With_UserId_Parameter()
        {
            // Arrange
            var userId = "user123";

            // Act
            var cut = RenderComponent<Profile>(parameters => parameters
                .Add(p => p.UserId, userId));

            // Assert
            cut.Instance.UserId.Should().Be(userId);
        }

        [Fact]
        public void Profile_Should_Display_No_User_Found_When_UserId_Is_Null()
        {
            // Arrange
            var cut = RenderComponent<Profile>(parameters => parameters
                .Add(p => p.UserId, null));

            // Act
            cut.WaitForState(() =>
                (bool)cut.Instance.GetType().GetField("isLoading", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cut.Instance) == false);

            // Assert
            var message = cut.Find("p").TextContent;
            message.Should().Be("No user found.");
        }

        [Fact]
        public void Profile_Should_Navigate_On_Button_Click()
        {
            // Arrange
            var navManager = Services.GetRequiredService<FakeNavigationManager>();
            var cut = RenderComponent<Profile>(parameters => parameters
             .Add(p => p.UserId, "user123"));

            cut.WaitForState(() =>
                (bool)cut.Instance.GetType().GetField("isLoading", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(cut.Instance) == false);

            // Act
            cut.Find("button").Click();

            // Assert
            navManager.Uri.Should().EndWith("/new-profile");
        }
    }
}
