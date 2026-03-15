using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week_MainGameSystem_v4 {

    [Serializable]
    public struct S_SoundOptionData{
        [Range(0.0f,1.0f)]
        public float Sound ;
        [Range(0.0f,1.0f)]
        public float SE ;
        [Range(0.0f,1.0f)]
        public float BGM ;

        public S_SoundOptionData(float sound , float se , float bgm){
            Sound = sound;
            SE = se;
            BGM = bgm;
        }
    }
}
