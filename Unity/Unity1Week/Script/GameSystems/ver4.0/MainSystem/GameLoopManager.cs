using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using UniRx;

namespace Unity1Week_MainGameSystem_v4{
    public abstract class GameLoopManager<T> : MonoBehaviour 
    where T : Enum
    {
        [SerializeField]
        private int GameFrameLate = 60;

        [SerializeField]
        private LoadingUIUpdater loadingUI;
        private bool isHaveToLoading;
        private SceneLoadingManager sceneLoadingManager;
        private List<IDisposable> disposeList = new List<IDisposable>();
        private SceneGameManager<T> gameManager;
        private static bool isFirst = true;


        [Inject]
        /// <summary>
        /// ゲームシーン開始時にZenjectが呼び出す関数
        /// 勝手に呼び出さない
        /// </summary>
        /// <param name="gameManager">ゲームマネージャ</param>
        /// <param name="loadingManager">ローディングマネージャ</param>
        private void InjectObject(SceneGameManager<T> gameManager){

            this.gameManager = gameManager;

            var sceneLoadDispose = this.gameManager.SceneLoadAsync
            .Subscribe( (sceneName) => {
                activeIsHaveToLoading(sceneName);
            });

            disposeList.Add(sceneLoadDispose);

            sceneLoadingManager = new SceneLoadingManager();
        }


        /// <summary>
        /// MonoBehaviourのStart関数
        /// </summary>
        private void Start(){

            //ゲーム開始時1度だけ実行したい
            if(isFirst){
                #if !UNITY_EDITOR && UNITY_WEBGL
                WebGLInput.captureAllKeyboardInput = false;
                #endif

                isFirst = false;
            }

            //フレームレートを設定
            Application.targetFrameRate = GameFrameLate;

            //ローディングのフラグをオフ
            isHaveToLoading = false;

            //ローディングUIを表示する
            loadingUI.IsActiveLoadingUI(true);

            //各マネージャごとの固有の初期化処理
            InitManager();

            //ゲームの開始
            var coroutine = GameUpdate();
            StartCoroutine(coroutine);

        }



        /// <summary>
        /// 各種シーンごとに固有で初期化するものがあればオーバライドする
        /// </summary>
        protected virtual void InitManager(){}



        /// <summary>
        /// ゲームループを開始しするコルーチン
        /// </summary>
        /// <returns>コルーチン</returns>
        private IEnumerator GameUpdate(){

            //ゲームの初期化
            yield return gameManager.InitGame();

            //ローディングUIを非表示にする
            loadingUI.IsActiveLoadingUI(false);

            //シーンの開始
            var coroutine = gameManager.StartGame();
            StartCoroutine(coroutine);

            while(!isHaveToLoading){
                //ゲームのローディングが必要になるまで待機
                yield return null;
                
            }


            //シーン終了

            //不要なオブジェクトを開放する
            gameManager.ReleaseObject();

            //ローディングUIを表示する
            loadingUI.IsActiveLoadingUI(true);

            //読み込みを開始する(コルーチン)
            coroutine = sceneLoadingManager.LoadScene();
            StartCoroutine(coroutine);

            //購読の終了
            foreach (var disposable in disposeList){
                disposable.Dispose();
            }
        }



        /// <summary>
        /// シーンの切り替えを受け取った際の処理
        /// </summary>
        /// <param name="sceneName">遷移先のシーン</param>
        private void activeIsHaveToLoading(E_SceneName sceneName){
            
            //ローディングが必要であるフラグを立てる
            isHaveToLoading = true;

            //次のシーンを指定する
            sceneLoadingManager.NextScene = sceneName;

        }

    }


    
}


