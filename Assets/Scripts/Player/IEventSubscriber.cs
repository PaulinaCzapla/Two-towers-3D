namespace Player
{
    public interface IEventSubscriber
    {
        public void SubscribeToEvents();
        public void UnsubscribeFromAllEvents();

    }
}