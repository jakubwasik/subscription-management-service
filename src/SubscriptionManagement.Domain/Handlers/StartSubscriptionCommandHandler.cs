﻿using MediatR;
using SubscriptionManagement.Domain.Commands;

namespace SubscriptionManagement.Domain.Handlers;

public class StartSubscriptionCommandHandler : IRequestHandler<StartSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public StartSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(StartSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {request.UserId} not found");
        }

        if (user.Subscription == null)
        {
            user.AddSubscription(SubscriptionType.Basic);
        }

        user.ActivateSubscription();
        await _unitOfWork.SaveChangesAsync();
    }
}