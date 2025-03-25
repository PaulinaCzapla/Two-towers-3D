using UnityEngine.Events;

namespace UI
{
    public static class UIStaticEvents
    {
        private static readonly UnityEvent<int> _onSecondPassed = new UnityEvent<int>();
        private static readonly UnityEvent _onPlayerJumped = new UnityEvent();

        public static void SubscribeToSecondPassed(UnityAction<int> subscriber) =>
            _onSecondPassed.AddListener(subscriber);

        public static void UnsubscribeFromSecondPassed(UnityAction<int> subscriber) =>
            _onSecondPassed.RemoveListener(subscriber);

        public static void InvokeSecondPassed(int secondsSum) => _onSecondPassed?.Invoke(secondsSum);


        public static void SubscribeToPlayerJumped(UnityAction subscriber) =>
            _onPlayerJumped.AddListener(subscriber);

        public static void UnsubscribeFromPlayerJumped(UnityAction subscriber) =>
            _onPlayerJumped.RemoveListener(subscriber);

        public static void InvokePlayerJumped() => _onPlayerJumped?.Invoke();
    }
}