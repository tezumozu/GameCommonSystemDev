using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{
    public class PhaseClearChecker {

        //Phaseが更新されたことを通知 IPhaseUpdateCheckable
        Subject<string> phaseClearSubject = new Subject<string>();
        public IObservable<string> PhaseClearSubject{
            get{ return phaseClearSubject; }
        }

        private Dictionary< string , bool > missionClearDic;

        public PhaseClearChecker(string PhaseId , IFlagReadable flagController){
            var csvReader = new CSVDataBaseSerializer<MissionFlagStractRecord>();
            missionClearDic = new Dictionary<string, bool>();

            //Csv読み込み
            Dictionary<string , MissionFlagStractRecord> csvDic = csvReader.ReadCSVDataBase( "CSV/Story/PhaseMission/" + PhaseId);

            //セクションIDに紐づくPhaseDataをPhaseIdをキーに辞書へ
            //最初に生成するPhaseのIDを取得する
            foreach(var recordId in csvDic.Keys){
                //PhaseIdが自身と同じIDか
                if(csvDic[recordId].PearntPhaseId != PhaseId) continue;

                //リストに追加
                missionClearDic.Add(csvDic[recordId].MissionId , false);
                
                //flgaがすでにtrueなら
                if (flagController.GetFlagStatus(csvDic[recordId].MissionId)){
                    missionClearDic[csvDic[recordId].MissionId] = true;
                }
            }
        }

    }
}