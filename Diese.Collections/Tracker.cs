using System;

namespace Diese.Collections
{
    public class Tracker<T> : OrderedTrackerBase<T>
        where T : class
    {
        private readonly Func<T, bool> _canRegisterFunc;
        private readonly Action<T, EventHandler> _subscribeAction;
        private readonly Action<T, EventHandler> _unsubscribeAction;

        public Tracker(Func<T, bool> canRegisterFunc, Action<T, EventHandler> subscribeAction, Action<T, EventHandler> unsubscribeAction)
        {
            _canRegisterFunc = canRegisterFunc;
            _subscribeAction = subscribeAction;
            _unsubscribeAction = unsubscribeAction;
        }

        protected override bool CanRegister(T item) => _canRegisterFunc?.Invoke(item) ?? true;
        protected override void Subscribe(T item) => _subscribeAction(item, UnregisterHandler);
        protected override void Unsubscribe(T item) => _unsubscribeAction(item, UnregisterHandler);

        protected virtual void UnregisterHandler(object sender, EventArgs e) => Unregister((T)sender);
    }
}