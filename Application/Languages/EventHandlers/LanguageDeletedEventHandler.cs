using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Languages.EventHandlers
{
    internal class LanguageDeletedEventHandler : INotificationHandler<LanguageDeletedEvent>
    {
        private readonly ILogger<LanguageDeletedEventHandler> _logger;

        public LanguageDeletedEventHandler(ILogger<LanguageDeletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(LanguageDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("dev-study-catelogue Domain Event: {DomainEvent}", notification.GetType().Name);
            _logger.LogCritical("deleted language with id of : {Id}",notification.Id);  
            return Task.CompletedTask;
        }
    }
}
