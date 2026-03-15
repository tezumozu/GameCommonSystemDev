using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Unity1Week_MainGameSystem_v4{
    public abstract class SoundOptionManager : MonoBehaviour{

        //サウンド設定の初期値
        [SerializeField]
        protected S_SoundOptionData InitSoundOptionData;

        protected static S_SoundOptionData soundOptionData;
        protected static bool isInit = false;

        protected static Subject<S_SoundOptionData> UpdateOptionSubject = new Subject<S_SoundOptionData>();
        public static IObservable<S_SoundOptionData> UpdateOptionAsync => UpdateOptionSubject;

        public static S_SoundOptionData GetSoundOptionData => soundOptionData;

        /// <summary>
        /// Optionの初期化処理
        /// </summary>
        void Awake(){
            if(!isInit){
                isInit = true;
                Debug.Log("SoundOptionManager：Init");
                soundOptionData = InitSoundOptionData;
            }
        }
    }
}
