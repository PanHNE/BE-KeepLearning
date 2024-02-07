using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Exam.EventHandlers;

public class SaveExamCountryEventHandler : INotificationHandler<SaveExamCountryEvent>
{
  private readonly ILogger<SaveExamCountryEventHandler> _logger;

  public SaveExamCountryEventHandler(ILogger<SaveExamCountryEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(SaveExamCountryEvent notification, CancellationToken cancellationToken)
  {
    _logger.LogInformation("BE-KeepLearning Domain Event: {DomainEvent}", notification.GetType().Name);

    return Task.CompletedTask;
  }
}