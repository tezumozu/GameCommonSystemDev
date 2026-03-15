using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace GameCommonSystem_V6{
    public interface ISceneLoadNotifiable{
        public IObservable<ESceneName> SceneLoadAsync { get; }
    }
}

