using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using Unity1Week_MainGameSystem_v4;

public class SampleGameManager : SceneGameManager<E_SampleSceneState>{

    public SampleGameManager():base(){
        currentState = E_SampleSceneState.Init;
    }


    public override IEnumerator InitGame(){

        Debug.Log("GameManager : Init");

        //ゲームの終了を監視する
        var disposable = GameState<E_SampleSceneState>.FinishGameAsync
        .Subscribe(_=>{
            SceneLoadSubject.OnNext(E_SceneName.SampleScene);
        });

        disposableList.Add(disposable);

        //ゲームの状態を生成、初期化する
        gameStateDic[E_SampleSceneState.Init] = new SampleInitState();
        gameStateDic[E_SampleSceneState.InGame] = new SampleInGameState();
        gameStateDic[E_SampleSceneState.Result] = new SampleResultState();

        var initList = new List<IEnumerator>();

        foreach(var state in gameStateDic.Keys){

            //各状態の終了を購読する
            disposable = gameStateDic[state].FinishStateAsync
            .Subscribe( NextState => {
                currentState = NextState;
                var coroutine = gameStateDic[currentState].UpdateState();
                CoroutineHandler.OrderStartCoroutine(coroutine);
            });

            disposableList.Add(disposable);

            //各状態の初期化処理をリストにまとめる
            var coroutine = gameStateDic[state].InitState();
            initList.Add(coroutine);
        }

        CoroutineHandler.OrderStartCoroutine(initList);

        //全ての初期化が終了するまで待機
        while(CoroutineHandler.isRegistrationCoroutine(initList)){
            yield return null;
        }
        
    }


    public override IEnumerator StartGame(){
        Debug.Log("GameManager : StartGame");

        //最初のゲームの状態の処理を呼び出す
        var coroutine = gameStateDic[currentState].UpdateState();

        CoroutineHandler.OrderStartCoroutine(coroutine);

        yield return null;
    }


    public override void ReleaseObject(){
        Debug.Log("GameManager : ReleaseObject");
    }
}
