using UnityEngine.Events;

namespace Obstacles.Targets
{
    public static class ShootingStaticEvents
    {
        private static readonly UnityEvent<int> _onTargetHit = new UnityEvent<int>();
        private static readonly UnityEvent<int> _onShootingRangeEntered= new UnityEvent<int>();

        public static void SubscribeToTargetHit(UnityAction<int> subscriber) =>
            _onTargetHit.AddListener(subscriber);
        public static void UnsubscribeFromTargetHit(UnityAction<int> subscriber) =>
            _onTargetHit.RemoveListener(subscriber);
        public static void InvokeTargetHit(int floor) => _onTargetHit?.Invoke(floor);


        public static void SubscribeToShootingRangeEntered(UnityAction<int> subscriber) =>
            _onShootingRangeEntered.AddListener(subscriber);
        public static void UnsubscribeFromShootingRangeEnteredd(UnityAction<int> subscriber) =>
            _onShootingRangeEntered.RemoveListener(subscriber);
        public static void InvokeShootingRangeEntered(int num) => _onShootingRangeEntered?.Invoke(num);
    }
}