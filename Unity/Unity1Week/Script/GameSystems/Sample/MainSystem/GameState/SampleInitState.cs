using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity1Week_MainGameSystem_v4;

public class SampleInitState : GameState<E_SampleSceneState>{

    /// <summary>
    /// 状態を初期化するコルーチン
    /// </summary>
    /// <returns>コルーチン</returns>
    public override IEnumerator InitState(){

        //長い初期化
        yield return new WaitForSeconds(3f);
        Debug.Log("SampleInitState：Init!");

    }

    /// <summary>
    /// 状態の処理を実行するコルーチン
    /// </summary>
    /// <returns>コルーチン</returns>
    public override IEnumerator UpdateState(){
        
        Debug.Log("SampleInitState：Update!");
        yield return new WaitForSeconds(1f);

        finishStateSubject.OnNext(E_SampleSceneState.InGame);
    }

}
