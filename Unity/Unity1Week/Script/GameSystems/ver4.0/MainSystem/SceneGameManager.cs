using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;


namespace Unity1Week_MainGameSystem_v4{
    public abstract class SceneGameManager<T> : I_SceneLoadNoticable 
    where T : Enum
    {
        /// <summary>
        /// 現在の状態を表すEnum
        /// </summary>
        protected T currentState;
        
        /// <summary>
        /// シーンが終了し、次のシーンの読み込みが必要になったことを通知するサブジェクト
        /// </summary>
        protected Subject<E_SceneName> SceneLoadSubject;
        
        /// <summary>
        /// 読み取り専用、次のシーンの読み込みが必要になったことを通知するサブジェクト
        /// </summary>
        public IObservable<E_SceneName> SceneLoadAsync => SceneLoadSubject;

        /// <summary>
        /// ゲームの状態を整理するための辞書
        /// </summary>
        protected Dictionary< T , GameState<T> > gameStateDic;

        /// <summary>
        /// 購読しているサブジェクトをまとめるリスト
        /// </summary>
        protected List<IDisposable> disposableList;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneGameManager (){

            SceneLoadSubject = new Subject<E_SceneName>();
            gameStateDic = new Dictionary< T , GameState<T> >();
            disposableList = new List<IDisposable>();

        }


        /// <summary>
        /// ゲームに必要な非MonoBehaviour初期化処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator InitGame();

        /// <summary>
        /// ゲームを開始する
        /// ゲーム開始時の初期化処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator StartGame();

        /// <summary>
        /// シーン遷移時、
        /// 不具合がないよういらないオブジェクトを開放する
        /// </summary>
        public virtual void ReleaseObject(){
            //各状態をDisposeさせる
            foreach(var key in gameStateDic.Keys){
                gameStateDic[key].Dispose();
            }

            //自身の購読を終了する
            foreach(var disposable in disposableList){
                disposable.Dispose();
            }
        }

    }
}


