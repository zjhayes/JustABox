using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{   
    private static readonly IDictionary<EventType, UnityEvent> Events = new Dictionary<EventType, UnityEvent>();

    public static void Subscribe(EventType eventType, UnityAction listener)
    {
        UnityEvent eventOut;

        if(Events.TryGetValue(eventType, out eventOut))
        {
            eventOut.AddListener(listener);
        }
        else
        {
            eventOut = new UnityEvent();
            eventOut.AddListener(listener);
            Events.Add(eventType, eventOut);
        }
    }

    public static void Unsubscribe(EventType eventType, UnityAction listener)
    {
        UnityEvent eventOut;

        if(Events.TryGetValue(eventType, out eventOut))
        {
            eventOut.RemoveListener(listener);
        }
    }

    public static void Publish(EventType eventType)
    {
        UnityEvent eventOut;

        if(Events.TryGetValue(eventType, out eventOut))
        {
            eventOut.Invoke();
        }
    }

}
