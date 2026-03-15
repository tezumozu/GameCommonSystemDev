using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{
    public interface IPhaseUpdateCheckable{
        public IObservable<string> PhaseUpdateSubject{get;}
    }
}

