using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Unity1Week_MainGameSystem_v4{

    public class SoundPlayer : MonoBehaviour{

        [SerializeField]
        AudioSource audioSource;

        protected S_SoundOptionData option;

        /// <summary>
        /// Unityのスタート関数
        /// </summary>
        private void Start() {
            option = SoundOptionManager.GetSoundOptionData;

            //オプションが更新されたら数値を更新
            SoundOptionManager.UpdateOptionAsync
            .Subscribe(data => {
                option = data;
                audioSource.volume = option.Sound * option.BGM;
            })
            .AddTo(this);
        }


        /// <summary>
        /// SEを鳴らす
        /// </summary>
        /// <param name="se">鳴らすSE</param>
        public void PlaySE(AudioClip se){
            //音量を
            audioSource.volume = option.Sound * option.SE;
            audioSource.PlayOneShot(se);
        }


        /// <summary>
        /// BGMを鳴らす
        /// </summary>
        /// <param name="bgm">鳴らすBGM</param>
        /// <param name="isLoop">BGMをループするかどうか</param>
        public void PlayBGM(AudioClip bgm , bool isLoop){
            StopSound();
            //音量を
            audioSource.volume = option.Sound * option.BGM;
            audioSource.clip = bgm;
            audioSource.loop = isLoop;
            audioSource.Play();
        }



        /// <summary>
        /// 鳴らしているサウンドを停止する
        /// </summary>
        public void StopSound(){
            audioSource.Stop();
        }

    }

}


