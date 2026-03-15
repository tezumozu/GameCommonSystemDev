using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity1Week_MainGameSystem_v4;

public class SampleInGameState : GameState<E_SampleSceneState>{


    public override IEnumerator InitState(){
        
        //長い初期化
        yield return new WaitForSeconds(7f);

        Debug.Log("SampleInGameState：Init!");

    }


    public override IEnumerator UpdateState(){
        
        Debug.Log("SampleInGameState：Update!");
        yield return new WaitForSeconds(5f);

        finishStateSubject.OnNext(E_SampleSceneState.Result);
    }

}
