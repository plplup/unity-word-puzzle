using System.Collections.Generic;
using System;

public static class MessageSender<TMessageType> where TMessageType : Enum
{
    //TO DO - implement no typed listeners
    //private static Dictionary<TMessageType, List<Action>> noTypeListeners = new Dictionary<TMessageType, List<Action>>();

    //TO DO - improve action to register correct data type
    private readonly static Dictionary<TMessageType, Action<object>> listeners = new Dictionary<TMessageType, Action<object>>();

    public static void Subscribe(TMessageType messageId, Action<object> callback)
    {
        if (listeners.ContainsKey(messageId) == false)
            listeners[messageId] = callback;
        else
            listeners[messageId] += callback;
    }

    public static void Unsubscribe(TMessageType messageId, Action<object> callback)
    {
        if (listeners.ContainsKey(messageId))
            listeners[messageId] -= callback;
    }

    public static void Send(TMessageType messageId, object data = null)
    {
        if (listeners.ContainsKey(messageId))
            listeners[messageId]?.Invoke(data);
    }

}
