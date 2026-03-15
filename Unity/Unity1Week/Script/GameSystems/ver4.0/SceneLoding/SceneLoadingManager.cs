using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Unity1Week_MainGameSystem_v4{

    /// <summary>
    /// シーンのロードに関する処理を扱うクラス
    /// </summary>
    public class SceneLoadingManager {

        private AsyncOperation asyncLoad;
        
        /// <summary>
        /// 次に読み込むシーンを表すフィールド
        /// </summary>
        public E_SceneName NextScene;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loadingUI">ロード時に表示するUIを管理するクラス</param>
        public SceneLoadingManager(){
            NextScene = E_SceneName.SampleScene;
        }


        /// <summary>
        /// シーンを読み込むコルーチン
        /// </summary>
        /// <returns>コルーチン</returns>
        public IEnumerator LoadScene(){

            //シーン名を文字列へ変換
            string str = Enum.GetName(typeof(E_SceneName),NextScene);

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
