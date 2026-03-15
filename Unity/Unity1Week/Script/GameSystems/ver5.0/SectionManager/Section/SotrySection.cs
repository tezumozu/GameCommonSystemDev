using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UniRx;

namespace GameCommonSystem_V5{

    //フェーズごとのお題のクリア条件を監視するオブジェクト
    public class StorySection : IPhaseUpdateCheckable , ISectionFinishCheckable , IDisposable{

        string sectionId;

        // このセクションに含まれるPhaseの構造データを格納する辞書
        Dictionary<string , SectionStractCsvRecord> phaseDataDic = new Dictionary<string, SectionStractCsvRecord>();


        //フラグコントローラ
        IFlagReadable flagController;


        //現在進行しているPhaseを取得する
        PhaseClearChecker currentPhase;
        IDisposable currentPhaseDisposable;


        //Phaseが更新されたことを通知 IPhaseUpdateCheckable
        Subject<string> phaseUpdateSubject = new Subject<string>();
        public IObservable<string> PhaseUpdateSubject{
            get{ return phaseUpdateSubject; }
        }


        //Sectionがクリアされたことを通知 ISectionFinishCheckable
        Subject<string> finishSectionSubject = new Subject<string>();
        public IObservable<string> FinishSectionSubject{
            get{ return finishSectionSubject; }
        }


        //コンストラクタ
        public StorySection(string SectionId , string FilePath , IFlagReadable flagController){

            sectionId = SectionId;

            //セクションIDに紐づくPhaseDataを取得
            var csvReader = new CSVDataBaseSerializer<SectionStractCsvRecord>();
            Dictionary<string , SectionStractCsvRecord> csvDic= csvReader.ReadCSVDataBase(FilePath + "/" + SectionId);

            //セクションIDに紐づくPhaseDataをPhaseIdをキーに辞書へ
            //最初に生成するPhaseのIDを取得する
            string startPhaseId = "";
            foreach(var recordId in csvDic.Keys){
                phaseDataDic.Add(csvDic[recordId].PhaseId , csvDic[recordId]);

                if (csvDic[recordId].IsStart){
                    startPhaseId = csvDic[recordId].PhaseId;
                }
            }

            //最初のPhaseを開始する
            currentPhase = new PhaseClearChecker(startPhaseId , flagController);

            currentPhaseDisposable = currentPhase.PhaseClearSubject
            .Subscribe(PhaseId => {
                CheckSectionClear(PhaseId);
            });
        }


        //セクションの完了を監視・確認
        public void CheckSectionClear(string PhaseId){

            //購読の停止
            currentPhaseDisposable.Dispose();
            string nextId = phaseDataDic[PhaseId].NextSectionId;

            if(nextId != null){
                //phaseの切り替え
                currentPhase = new PhaseClearChecker(nextId , flagController);

                currentPhaseDisposable = currentPhase.PhaseClearSubject
                .Subscribe(PhaseId => {
                    CheckSectionClear(PhaseId);
                });

                phaseUpdateSubject.OnNext(nextId);

            } else {

                finishSectionSubject.OnNext(sectionId);
                
            }

        }


        //システム終了時の処理
        public void Dispose(){
            //購読の停止
            currentPhaseDisposable.Dispose();
        }
    }
}