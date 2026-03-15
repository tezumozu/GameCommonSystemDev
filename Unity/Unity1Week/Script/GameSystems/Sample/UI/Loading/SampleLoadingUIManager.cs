using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity1Week_MainGameSystem_v4;

public class SampleLoadingUIManager : LoadingUIUpdater{
    [SerializeField]
    Animator LoadingAnim;

    private void Start(){
    }

    public override void IsActiveLoadingUI(bool flag){
        gameObject.SetActive(flag);

        if(flag){
            Debug.Log("StartLoadingAnim");
        }else{
            Debug.Log("StopLoadingAnim");
        }

        return;
    }
}
