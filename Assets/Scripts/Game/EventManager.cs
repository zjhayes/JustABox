using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : EventBus
{
    public void Alert()
    {
        Publish(EventType.OnAlert);
    }

    public void AllClear()
    {
        Publish(EventType.OnAllClear);
    }
}
