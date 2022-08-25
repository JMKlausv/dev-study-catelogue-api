using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Languages.EventHandlers
{
    public class LanguageCreatedEventHandler : INotificationHandler<LanguageCreatedEvent>
    {
       
        private readonly ILogger _logger;

        public LanguageCreatedEventHandler(ILogger<LanguageCreatedEventHandler> logger)
        {
           _logger = logger;
        }
     
        public Task Handle(LanguageCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("dev-study-catelogue Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
