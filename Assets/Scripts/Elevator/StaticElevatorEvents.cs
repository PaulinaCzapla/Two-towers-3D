using UnityEngine.Events;

namespace Elevator
{
    public static class StaticElevatorEvents
    {
        private static readonly UnityEvent<int> _elevatorButtonClicked = new UnityEvent<int>();
        private static readonly UnityEvent<int> _elevatorButtonTargeted= new UnityEvent<int>();

        public static void SubscribeToElevatorButtonClicked(UnityAction<int> subscriber) =>
            _elevatorButtonClicked.AddListener(subscriber);
        public static void UnsubscribeFromElevatorButtonClicked(UnityAction<int> subscriber) =>
            _elevatorButtonClicked.RemoveListener(subscriber);
        public static void InvokeElevatorButtonClicked(int floor) => _elevatorButtonClicked?.Invoke(floor);


        public static void SubscribeToElevatorButtonTargeted(UnityAction<int> subscriber) =>
            _elevatorButtonTargeted.AddListener(subscriber);
        public static void UnsubscribeFromElevatorButtonTargeted(UnityAction<int> subscriber) =>
            _elevatorButtonTargeted.RemoveListener(subscriber);
        public static void InvokeElevatorButtonTargeted(int floor) => _elevatorButtonTargeted?.Invoke(floor);
    }
}