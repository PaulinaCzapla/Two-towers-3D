using UnityEngine;
using UnityEngine.Events;

namespace Player.Respawn
{
    public static class StaticRespawnEvents
    {
        private static readonly UnityEvent _onPlayerDied = new UnityEvent();
        private static readonly UnityEvent<Vector3> _onRespawnPointChanged = new UnityEvent<Vector3>();

        public static void SubscribeToPlayerDied(UnityAction subscriber) =>
            _onPlayerDied.AddListener(subscriber);
        public static void UnsubscribeFromPlayerDied(UnityAction subscriber) =>
            _onPlayerDied.RemoveListener(subscriber);
        public static void InvokePlayerDied() => _onPlayerDied?.Invoke();


        public static void SubscribeToRespawnPointChange(UnityAction<Vector3> subscriber) =>
            _onRespawnPointChanged.AddListener(subscriber);
        public static void UnsubscribeFromRespawnPointChange(UnityAction<Vector3> subscriber) =>
            _onRespawnPointChanged.RemoveListener(subscriber);
        public static void InvokeRespawnPointChange(Vector3 pos) => _onRespawnPointChanged?.Invoke(pos);
    }
}