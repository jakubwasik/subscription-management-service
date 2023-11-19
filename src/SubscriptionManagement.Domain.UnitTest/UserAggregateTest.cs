using FluentAssertions;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain.UnitTest
{
    public class UserAggregateTest
    {
        [Fact]
        public void ShouldBeAbleToAddSubscriptionIfNotExists()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };

            //Act
            user.AddSubscription(SubscriptionType.Basic);

            // Assert
            user.Subscription.Should().NotBeNull();
        }

        [Fact]
        public void SubscriptionShouldBeActiveWhenAddedByUser()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };

            //Act
            user.AddSubscription(SubscriptionType.Basic);

            // Assert
            user.Subscription.Should().NotBeNull();
            user.Subscription!.IsActive.Should().BeTrue();
            user.Subscription!.ActiveTo.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void UserShouldNotBeAbleToAddSubscriptionIfAlreadyHasOne()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };

            //Act
            user.AddSubscription(SubscriptionType.Basic);
            var ex = Assert.Throws<InvalidOperationException>(() => user.AddSubscription(SubscriptionType.Basic));

            // Assert
            ex.Should().NotBeNull();
        }

        [Fact]
        public void UserShouldBeAbleToPauseSubscriptionIfExists()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };

            //Act
            user.AddSubscription(SubscriptionType.Basic);
            user.PauseSubscription();

            // Assert
            user.Subscription.Should().NotBeNull();
            user.Subscription.AutoRenewal.Should().BeFalse();
        }

        [Fact]
        public void SubscriptionStillShouldBeActiveWhenUserStopsButSubscriptionPeriodDidntTerminate()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };

            //Act
            user.AddSubscription(SubscriptionType.Basic);
            user.PauseSubscription();

            // Assert
            user.Subscription.Should().NotBeNull();
            user.Subscription.ActiveTo.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void SubscriptionShouldNotBeAutoRenewedIfStoppedAndPeriodTerminated()
        {
            // Arrange
            var user = new User() { Name = "test", Email = "test@gmail.com", Id = 1 };
            user.AddSubscription(SubscriptionType.Basic);
            user.PauseSubscription();
            user.Subscription.ActiveTo = DateTimeOffset.MinValue;

            //Act
            var shouldBeRenewed = user.Subscription.ShouldBeRenewed();

            // Assert
            shouldBeRenewed.Should().BeFalse();
        }
    }
}