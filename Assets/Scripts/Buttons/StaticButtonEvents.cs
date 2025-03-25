using UnityEngine.Events;

namespace Buttons
{
    public class StaticButtonEvents
    {
        private static readonly UnityEvent<string> _buttonClicked = new UnityEvent<string>();
        private static readonly UnityEvent<string> _buttonTargeted= new UnityEvent<string>();

        public static void SubscribeToButtonClicked(UnityAction<string> subscriber) =>
            _buttonClicked.AddListener(subscriber);
        public static void UnsubscribeFromButtonClicked(UnityAction<string> subscriber) =>
            _buttonClicked.RemoveListener(subscriber);
        public static void InvokeButtonClicked(string guid) => _buttonClicked?.Invoke(guid);


        public static void SubscribeToButtonTargeted(UnityAction<string> subscriber) =>
            _buttonTargeted.AddListener(subscriber);
        public static void UnsubscribeFromButtonTargeted(UnityAction<string> subscriber) =>
            _buttonTargeted.RemoveListener(subscriber);
        public static void InvokeButtonTargeted(string guid) => _buttonTargeted?.Invoke(guid);
    }
}