using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Unity1Week_MainGameSystem_v4{

    /// <summary>
    /// 各シーン内における状態のふるまいを定義するクラス
    /// </summary>
    /// <typeparam name="T">各シーン毎の状態を表すEnum</typeparam>
    public abstract class GameState<T> : IDisposable 
    where T : Enum
    {

        /// <summary>
        /// Stateが終了したことを通知するサブジェクト
        /// </summary>
        protected Subject<T> finishStateSubject = new Subject<T>();

        /// <summary>
        /// 読み込み専用、Stateが終了したことを通知するサブジェクト
        /// </summary>
        public IObservable<T> FinishStateAsync => finishStateSubject;


        /// <summary>
        /// ゲームが終了したことを通知するサブジェクト
        /// </summary>
        protected static Subject<Unit> finishGameSubject = new Subject<Unit>();

        /// <summary>
        /// 読み込み専用、ゲームが終了したことを通知するサブジェクト
        /// </summary>
        public static IObservable<Unit> FinishGameAsync => finishGameSubject;


        /// <summary>
        /// 購読しているSubjectのリスト
        /// </summary>
        protected List<IDisposable> disposableList = new List<IDisposable>();


        /// <summary>
        /// 状態の初期化処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator InitState();

        /// <summary>
        /// 状態の処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator UpdateState();


        /// <summary>
        /// 購読しているSubjectの購読を停止する
        /// </summary>
        public virtual void Dispose(){

            //何も購読していなければリターン
            if(disposableList.Count == 0) return;

            //リスト内すべての購読を終了する
            foreach(var disposable in disposableList){
                disposable.Dispose();
            }

        }
    }
}
