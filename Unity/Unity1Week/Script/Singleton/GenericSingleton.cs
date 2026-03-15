using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSingletonObject<T> 
where T : GenericSingletonObject<T>, new(){
    private static T instance = new T();

    protected GenericSingletonObject() {
        OnInitialize();
    }

    public static T Instance
    {
        get {
            return instance;
        }
    }

    protected abstract void OnInitialize();
}
