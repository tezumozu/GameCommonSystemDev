using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Csv;


namespace GameCommonSystem_V5 {

    public class CSVDataBaseSerializer<T> where T : DataBaceRecord {

        public Dictionary<string , T> ReadCSVDataBase(string filePath){

            Dictionary<string , T> resultDataBase = new Dictionary<string , T>();
            
            //一度読み込んだCSVを格納する配列
            T[] dataList = {};

            TextAsset textAsset = new TextAsset();
            
            //CSV読み込み
            textAsset = Resources.Load(filePath, typeof(TextAsset)) as TextAsset;

            //読み込んだCSVからデータに変換
            //データがないとき・シリアライズに失敗するとき
            try{
                dataList = CsvSerializer.Deserialize<T>(textAsset.text);
            }
            catch (System.NullReferenceException){
                Debug.Log("CSVDataBaseSerializer : 読み込みに失敗 " + filePath + ".csv");
            }
            
            
            //辞書に変換
            foreach ( var record in dataList){
                resultDataBase[record.Id] = record;
            }

            return resultDataBase;

        }


        public void WriteCSVDataBase( Dictionary<string,T> dataBace){

        }

    }
}