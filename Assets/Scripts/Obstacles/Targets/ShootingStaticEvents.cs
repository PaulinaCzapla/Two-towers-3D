using UnityEngine.Events;

namespace Obstacles.Targets
{
    public static class ShootingStaticEvents
    {
        private static readonly UnityEvent _onTargetHit = new UnityEvent();
        private static readonly UnityEvent<int> _onShootingRangeEntered= new UnityEvent<int>();

        public static void SubscribeToTargetHit(UnityAction subscriber) =>
            _onTargetHit.AddListener(subscriber);
        public static void UnsubscribeFromTargetHit(UnityAction subscriber) =>
            _onTargetHit.RemoveListener(subscriber);
        public static void InvokeTargetHit() => _onTargetHit?.Invoke();


        public static void SubscribeToShootingRangeEntered(UnityAction<int> subscriber) =>
            _onShootingRangeEntered.AddListener(subscriber);
        public static void UnsubscribeFromShootingRangeEnteredd(UnityAction<int> subscriber) =>
            _onShootingRangeEntered.RemoveListener(subscriber);
        public static void InvokeShootingRangeEntered(int num) => _onShootingRangeEntered?.Invoke(num);
    }
}