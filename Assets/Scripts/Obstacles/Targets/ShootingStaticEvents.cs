using UnityEngine.Events;

namespace Obstacles.Targets
{
    public static class ShootingStaticEvents
    {
        private static readonly UnityEvent _onTargetHit = new UnityEvent();
        public static void SubscribeToTargetHit(UnityAction subscriber) =>
            _onTargetHit.AddListener(subscriber);
        public static void UnsubscribeFromTargetHit(UnityAction subscriber) =>
            _onTargetHit.RemoveListener(subscriber);
        public static void InvokeTargetHit() => _onTargetHit?.Invoke();
    }
}