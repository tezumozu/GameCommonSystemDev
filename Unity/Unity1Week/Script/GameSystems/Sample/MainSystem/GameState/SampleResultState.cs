using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity1Week_MainGameSystem_v4;
using UniRx;

public class SampleResultState : GameState<E_SampleSceneState>{


    public override IEnumerator InitState(){
        
        //長い初期化
        yield return new WaitForSeconds(5f);
        Debug.Log("SampleResultState：Init!");

    }


    public override IEnumerator UpdateState(){
        
        Debug.Log("SampleResultState：Update!");
        yield return new WaitForSeconds(3f);

        finishGameSubject.OnNext(Unit.Default);
    }

}
