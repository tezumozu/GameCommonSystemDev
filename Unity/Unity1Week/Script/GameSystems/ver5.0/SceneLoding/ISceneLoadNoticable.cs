using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace GameCommonSystem_V5{
    public interface ISceneLoadNoticable{
        public IObservable<ESceneName> SceneLoadAsync { get; }
    }
}

