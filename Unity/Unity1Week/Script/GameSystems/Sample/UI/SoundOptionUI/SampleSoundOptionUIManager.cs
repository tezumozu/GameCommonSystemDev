using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity1Week_MainGameSystem_v4;

public class SampleSoundOptionUIManager : SoundOptionManager{
    
    [SerializeField]
    Slider SoundSlider;

    [SerializeField]
    Slider SESlider;

    [SerializeField]
    Slider BGMSlider;

    //サウンドプレイヤー
    [SerializeField]
    SoundPlayer soundPlayer;

    [SerializeField]
    AudioClip desitionSE;

    void Start(){
        //UIの値を現在の数値に直す
        SoundSlider.value = soundOptionData.Sound;
        SESlider.value = soundOptionData.SE;
        BGMSlider.value = soundOptionData.BGM;
    }

    public void SetActiveUI(bool flag){
        gameObject.SetActive(flag);

        //現在の数値に直す
        SoundSlider.value = soundOptionData.Sound;
        SESlider.value = soundOptionData.SE;
        BGMSlider.value = soundOptionData.BGM;
    }

    public void UpdateOption(){
        soundOptionData = new S_SoundOptionData(SoundSlider.value , SESlider.value , BGMSlider.value);
        UpdateOptionSubject.OnNext(soundOptionData);
        soundPlayer.PlaySE(desitionSE);
    }

    public void TestSE(){
        soundPlayer.PlaySE(desitionSE);
    }

}
