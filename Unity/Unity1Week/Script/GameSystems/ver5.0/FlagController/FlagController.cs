using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{

    public abstract class FlagController : MonoBehaviour , IFlagReadable , IFlagUpdatable {

        protected Dictionary<string , FlagData> flagList;
        protected Subject<string> flagCheckSubject = new Subject<string>();
        public IObservable<string> FlagCheckSubject {
            get { return flagCheckSubject; }
        }


        // コンストラクタ 初期化状態で読み込む場合
        protected void LoadFlagListCsv (string flagFilePath){
            flagList = new Dictionary<string, FlagData>();

            CSVDataBaseSerializer<FlagDataBaseRecord> csvReader = new CSVDataBaseSerializer<FlagDataBaseRecord>();
            Dictionary<string , FlagDataBaseRecord> flagCsvRecordList = csvReader.ReadCSVDataBase(flagFilePath);

            //レコードを元にFlag管理用の辞書を作成
            foreach(var key in flagCsvRecordList.Keys){
                //データ作成
                var data = new FlagData();
                data.FlagName = flagCsvRecordList[key].FlagName;
                data.FlagValue = false;

                flagList.Add(key , data);
            }
        }


        // フラグを読み込む
        public bool GetFlagStatus(string flagId){
            return flagList[flagId].FlagValue;
        }


        //フラグを更新する
        public void SetFlag(string flagId, bool value){
            flagList[flagId].FlagValue = value;
            flagCheckSubject.OnNext(flagId);
        }

    }

}