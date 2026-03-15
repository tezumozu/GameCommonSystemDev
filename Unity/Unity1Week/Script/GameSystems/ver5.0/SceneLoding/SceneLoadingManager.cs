using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace GameCommonSystem_V5{

    /// <summary>
    /// シーンのロードに関する処理を扱うクラス
    /// </summary>
    public class SceneLoadingManager {

        private AsyncOperation asyncLoad;
        
        /// <summary>
        /// 次に読み込むシーンを表すフィールド
        /// </summary>
        private ESceneName nextScene;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="SceneLoadingManager">シーンのロードを管理するクラス</param>
        public SceneLoadingManager(ESceneName nextScene){
            this.nextScene = nextScene;
        }


        /// <summary>
        /// シーンを読み込むコルーチン
        /// </summary>
        /// <returns>コルーチン</returns>
        public IEnumerator LoadScene(){

            //シーン名を文字列へ変換
            string str = Enum.GetName(typeof(ESceneName),nextScene);

            //シーン読み込み開始
            asyncLoad = SceneManager.LoadSceneAsync(str);

            asyncLoad.allowSceneActivation = false;

            //ローディング中は待機
            while( asyncLoad.progress < 0.9f ){
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }

    }

}
