using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBusManager : MonoBehaviour
{
    private Hashtable eventsHashtable = new Hashtable();

    private static EventBusManager eventBusManager;

    public static EventBusManager Instance 
    {
        get 
        {
            if (!eventBusManager) 
            {
                eventBusManager = FindObjectOfType(typeof(EventBusManager)) as EventBusManager;

                if(!eventBusManager) 
                {
                    Debug.LogError("There needs to be one active EventBus Manager script on a GameObject in your scene.");
                } 
                else 
                {
                    eventBusManager.Init();
                }
            }
            return eventBusManager;
        }
    }

    private void Init() 
    {
        eventBusManager.eventsHashtable ??= new Hashtable();
    }

    public static void Subscribe<T>(EventBusEnum.EventName eventName, UnityAction<T> listener)
    {
        UnityEvent<T> thisEvent = null;
        string eventKey = GetKey<T>(eventName);

        if(Instance.eventsHashtable.ContainsKey(eventKey)) 
        {
            thisEvent = (UnityEvent<T>)Instance.eventsHashtable[eventKey];
            thisEvent.AddListener(listener);
            Instance.eventsHashtable[eventName] = thisEvent;
        }
        else 
        {
            thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(listener);
            Instance.eventsHashtable.Add(eventKey, thisEvent);
        }
    }

    public static void Unsubscribe<T>(EventBusEnum.EventName eventName, UnityAction<T> listener) 
    {
        if(eventBusManager == null) return;

        UnityEvent<T> thisEvent = null;
        string eventKey = GetKey<T>(eventName);

        if(Instance.eventsHashtable.Contains(eventName)) {
            thisEvent = (UnityEvent<T>)Instance.eventsHashtable[eventName];
            thisEvent.RemoveListener(listener);
            Instance.eventsHashtable[eventName] = thisEvent;
        }
    }

    public static void FireEvent<T>(EventBusEnum.EventName eventName, T parameter) 
    {
        UnityEvent<T> thisEvent = null;
        string eventKey = GetKey<T>(eventName);
        
        if(Instance.eventsHashtable.ContainsKey(eventKey)){
            thisEvent = (UnityEvent<T>)Instance.eventsHashtable[eventKey];
            thisEvent.Invoke(parameter);
        }
    }

    private static string GetKey<T>(EventBusEnum.EventName eventName) {
        Type type = typeof(T);
        string key = type.ToString() + eventName.ToString();
        return key;
    }

    public static void Subscribe(EventBusEnum.EventName eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if(Instance.eventsHashtable.ContainsKey(eventName)) 
        {
            thisEvent = (UnityEvent)Instance.eventsHashtable[eventName];
            thisEvent.AddListener(listener);
            Instance.eventsHashtable[eventName] = thisEvent;
        }
        else 
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventsHashtable.Add(eventName, thisEvent);
        }
    }

    public static void Unsubscribe(EventBusEnum.EventName eventName, UnityAction listener) 
    {
        if(eventBusManager == null) return;

        UnityEvent thisEvent = null;

        if(Instance.eventsHashtable.Contains(eventName)) {
            thisEvent = (UnityEvent)Instance.eventsHashtable[eventName];
            thisEvent.RemoveListener(listener);
            Instance.eventsHashtable[eventName] = thisEvent;
        }
    }

    public static void FireEvent(EventBusEnum.EventName eventName) 
    {
        UnityEvent thisEvent = null;
        
        if(Instance.eventsHashtable.ContainsKey(eventName)){
            thisEvent = (UnityEvent)Instance.eventsHashtable[eventName];
            thisEvent.Invoke();
        }
    }
}
