using NSubstitute;
using SubscriptionManagement.Domain.Commands;
using FluentAssertions;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain.UnitTest.Commands
{
    public class StartSubscriptionCommandHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StartSubscriptionCommandHandler _sut;

        public StartSubscriptionCommandHandlerTests()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _sut = new StartSubscriptionCommandHandler(_unitOfWork);
        }

        [Fact]
        public async Task HandlerShouldBeAbleToCreateSubscriptionIfNotExists()
        {
            // Arrange
            var user = new User() { Name = "test", Id = 1 };
            _unitOfWork.UserRepository
                .GetByIdAsync(Arg.Any<int>(), includeSubscription: true)
                .Returns(user);
            var startSubscriptionCommand = new StartSubscriptionCommand(userId: 1);

            // Act
            await _sut.Handle(startSubscriptionCommand, CancellationToken.None);

            // Assert
            user.Subscription.Should().NotBeNull();
        }

        [Fact]
        public async Task SubscriptionShouldBeActiveWhenReceivedStartCommand()
        {
            // Arrange
            var user = new User() { Name = "test", Id = 1 };
            _unitOfWork.UserRepository
                .GetByIdAsync(Arg.Any<int>(), includeSubscription: true)
                .Returns(user);
            var startSubscriptionCommand = new StartSubscriptionCommand(userId: 1);

            // Act
            await _sut.Handle(startSubscriptionCommand, CancellationToken.None);

            // Assert
            user.Subscription.AutoRenewal.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldThrowExceptionAndNotSaveChangesWhenRequestedUserDoesntExist()
        {
            // Arrange
            _unitOfWork.UserRepository
                .GetByIdAsync(Arg.Any<int>(), includeSubscription: true)
                .Returns(null as User);
            var startSubscriptionCommand = new StartSubscriptionCommand(userId: 1);

            // Act - Assert
            Assert.ThrowsAsync<Exception>(async () => await _sut.Handle(startSubscriptionCommand, CancellationToken.None));
            await _unitOfWork.DidNotReceive().SaveChangesAsync();
        }
    }
}