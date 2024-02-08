//using MediatR;
//using My.AppHandlers.Notifications;

//namespace My.AppHandlers.Handlers;

//public class EmailHandler : INotificationHandler<AccountAddedNotification>
//{
//    private readonly FakeDataStore _fakeDataStore;

//    public EmailHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

//    public async Task Handle(AccountAddedNotification notification, CancellationToken cancellationToken)
//    {
//        await _fakeDataStore.EventOccured(notification.Account, "Email sent");
//        await Task.CompletedTask;
//    }
//}
