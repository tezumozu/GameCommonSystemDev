using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{

    public interface IFlagReadable{

        public IObservable<string> FlagCheckSubject{get;}
        public bool GetFlagStatus(string flagId);
    
    }
    
}