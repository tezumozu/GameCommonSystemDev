using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csv;
using GameCommonSystem_V5;

public class CsvTestReader : MonoBehaviour{
    void Start(){
        var csvReader = new CSVDataBaseSerializer<TestDataBaseRecord>();
        Dictionary<string , TestDataBaseRecord> daraBase = csvReader.ReadCSVDataBase("TestDataBaceCSV");

        foreach(var recordId in daraBase.Keys){
            Debug.Log(daraBase[recordId].Id);
        }
    }
}
