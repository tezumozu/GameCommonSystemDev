using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommonSystem_V5;
using UniRx;

public class SectionTestMono : MonoBehaviour{

    // Start is called before the first frame update
    StorySection testSection ;
    IFlagReadable flagController;

    void Start(){

        //GameObject
        flagController = GameObject.Find("FlagController").GetComponent<IFlagReadable>();
        testSection = new StorySection( "sc_test0001" , "Csv/Story/Section" , flagController );

        //セクションの終了を監視
        testSection.FinishSectionSubject
        .Subscribe( Id => {
            Debug.Log("SectionTestMono : "+ Id +" is Finish!");
        })
        .AddTo(this);

        //Phaseの切り替わりを監視
        testSection.PhaseUpdateSubject
        .Subscribe(Id => {
            Debug.Log("SectionTestMono : Phase "+ Id +" Start!");
        })
        .AddTo(this);

    }

}