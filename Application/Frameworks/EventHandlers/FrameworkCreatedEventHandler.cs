using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Frameworks.EventHandlers
{
    public class FrameworkCreatedEventHandler : INotificationHandler<FrameworkCreatedEvent>
    {
        private readonly ILogger<FrameworkCreatedEventHandler> _logger;

        public FrameworkCreatedEventHandler(ILogger<FrameworkCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(FrameworkCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("dev-study-catelogue Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
