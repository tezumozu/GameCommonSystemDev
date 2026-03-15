using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{

    public class TestFlagController : FlagController {

        [SerializeField]
        string FlagCsvFolder = "";

        [SerializeField]
        string FlagCsvFilePhat = "";

        // スタート関数
        public void Start(){
            LoadFlagListCsv( FlagCsvFolder + "/" + FlagCsvFilePhat);
        }

    }

}