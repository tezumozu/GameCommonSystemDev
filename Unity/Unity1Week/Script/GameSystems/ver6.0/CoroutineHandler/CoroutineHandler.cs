using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Zenject;

namespace GameCommonSystem_V6{

    /// <summary>
    /// 非MonoBehaviourでも非同期処理（コルーチン）を実現するためのクラス
    /// </summary>
    public class CoroutineHandler : MonoBehaviour , ICoroutineOrderable{

        //登録されたコルーチンの稼働状況を記録するリスト
        Dictionary<IEnumerator,bool> activeCoroutineDic;
        //監視用コルーチンのリスト
        Dictionary<IEnumerator,Coroutine> checkerCoroutineDic;


        /// <summary>
        /// シーン更新時に呼び出される関数、別の場所で呼び出さない
        /// </summary>
        /// <param name="sceneloadAsync">シーンの読み込み開始を通知するオブジェクト（更新後のゲームマネージャをZenjectで注入）</param>
        [Inject]
        private void InitHandler(ISceneLoadNotifiable sceneloadAsync){

            //シーンの終了を監視
            sceneloadAsync.SceneLoadAsync.Subscribe((_)=>{
                
                var keys = checkerCoroutineDic.Keys;

                //登録された全てのコルーチンに対して
                foreach ( var coroutine in keys ) {

                    //登録されている全てのコルーチンを停止
                    StopCoroutine(coroutine);

                    //監視用のコルーチンを停止
                    StopCoroutine(checkerCoroutineDic[coroutine]);

                }
                
                //辞書を空にする（これで管理していたコルーチンは破棄される？要検証）
                activeCoroutineDic.Clear();
                checkerCoroutineDic.Clear();

            }).AddTo(this);

        }



        /// <summary>
        /// コルーチンを受け取って開始する、
        /// すでに開始したコルーチンを受け取った場合何もしない、
        /// 停止しているコルーチンを受け取った場合再開する
        /// </summary>
        /// <param name="coroutine">開始するコルーチン</param>
        public void OrderStartCoroutine(IEnumerator coroutine){

            //コルーチンが登録されているか？
            if(activeCoroutineDic.ContainsKey(coroutine)) {

                //コルーチンがアクティブか？
                if(activeCoroutineDic[coroutine]){
                    Debug.Log( " CoroutineHandler : This Coroutine is Already Active! " );
                    return ;
                }

                //アクティブでないなら再開する
                StartCoroutine(coroutine);
                activeCoroutineDic[coroutine] = true;
                return ;

            }

            //コルーチンの終了を監視するためのコルーチンを生成する
            Coroutine checkerCoroutine = StartCoroutine ( CheckFinishCoroutine( coroutine ) );
            
            //コルーチンをリストに登録する
            checkerCoroutineDic.Add( coroutine , checkerCoroutine );
        }

        /// <summary>
        /// コルーチンのリストを受け取って全てのコルーチンを登録し開始する、
        /// すでに開始したコルーチンを受け取った場合何もしない、
        /// 停止しているコルーチンを受け取った場合再開する
        /// </summary>
        /// <param name="coroutineList"></param>
        public void OrderStartCoroutine(List<IEnumerator> coroutineList){

            //各要素に対し、OrderStartCoroutineを呼び出す
            foreach( var coroutine in coroutineList ){
                OrderStartCoroutine(coroutine);
            }

        }




        /// <summary>
        /// コルーチンを停止する、
        /// すでにコルーチンが停止していたら何もしない、
        /// コルーチンが登録されていなければ何もしない
        /// </summary>
        /// <param name="coroutine">停止するコルーチン</param>
        public void OrderStopCoroutine(IEnumerator coroutine){
            
            //コルーチンが登録されていない場合リターン
            if(!activeCoroutineDic.ContainsKey(coroutine)){
                Debug.Log( " CoroutineHandler : Coroutine is Not Registration ! " );
                return;
            }
            
            //コルーチンがすでに停止していたらリターン
            if(!activeCoroutineDic[coroutine]){
                Debug.Log( " CoroutineHandler : This Coroutine is Already Stop! " );
                return;
            }
            
            StopCoroutine(coroutine);
            activeCoroutineDic[coroutine] = false;

        }


        

        /// <summary>
        /// コルーチンのリストを受け取り、すべてのコルーチンを停止する、
        /// コルーチンがすでに停止していたら何もしない、
        /// 登録されていないコルーチンに対しては何もしない
        /// </summary>
        /// <param name="coroutineList">停止するコルーチンのリスト</param>
        public void OrderStopCoroutine(List<IEnumerator> coroutineList){
            
            //各要素に対し、OrderStopCoroutineを呼び出す
            foreach( var coroutine in coroutineList ){
                OrderStopCoroutine(coroutine);
            }

        }



        /// <summary>
        /// 受け取ったコルーチンを完全に停止する
        /// </summary>
        /// <param name="coroutine">破棄するコルーチン</param>
        public void OrderKillCoroutine(IEnumerator coroutine){

            //コルーチンが登録されていない場合リターン
            if(!activeCoroutineDic.ContainsKey(coroutine)){
                Debug.Log( " CoroutineHandler : Coroutine is Not Registration ! " );
                return;
            }
            
            //コルーチンがアクティブなら停止
            if(activeCoroutineDic[coroutine]){
                (coroutine as IDisposable)?.Dispose();
                StopCoroutine(coroutine);
            }

            //監視用のコルーチンを停止する
            StopCoroutine(checkerCoroutineDic[coroutine]);

            //終了したので辞書から削除
            activeCoroutineDic.Remove(coroutine);
            checkerCoroutineDic.Remove(coroutine);

        }

        /// <summary>
        /// コルーチンのリストを受け取り、受け取ったコルーチンを破棄する、
        /// 登録されていないコルーチンに対しては何もしない
        /// </summary>
        /// <param name="coroutineList">破棄するコルーチンのリスト</param>
        public void OrderKillCoroutine(List<IEnumerator> coroutineList){

           //各要素に対し、OrderKillCoroutineを呼び出す
            foreach( var coroutine in coroutineList ){
                OrderKillCoroutine(coroutine);
            }

        }



        /// <summary>
        /// 開始したコルーチンの終了を監視するコルーチン
        /// </summary>
        /// <param name="coroutine">呼びだすコルーチン</param>
        /// <returns>コルーチン、null</returns>
        private IEnumerator CheckFinishCoroutine(IEnumerator coroutine){

            //呼び出し元から受け取ったコルーチンを開始する
            activeCoroutineDic.Add( coroutine , true );
            var activeCoroutine = StartCoroutine(coroutine);
            
            //同じフレーム内で辞書の追加と削除を同時に行わないように1フレーム遅延
            yield return null ;

            //呼び出し元のコルーチンの終了を待つ
            yield return activeCoroutine ;

            //終了したので辞書から削除
            activeCoroutineDic.Remove(coroutine);
            checkerCoroutineDic.Remove(coroutine);

        }



        /// <summary>
        /// 受け取ったコルーチンが登録されているか
        /// ＝コルーチンが終了しているか成否を返す。
        /// </summary>
        /// <param name="coroutine">対象のコルーチン</param>
        /// <returns> 
        /// ture：登録されている＝終了していない
        /// false：登録されていない＝コルーチンの終了
        /// </returns>
        public bool IsRegistrationCoroutine(IEnumerator coroutine){
            return activeCoroutineDic.ContainsKey(coroutine);
        }

    }
}


