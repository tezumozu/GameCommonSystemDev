using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameCommonSystem_V5;
using Csv.Annotations;

[CsvObject]
public partial class TestDataBaseRecord : DataBaceRecord{

    [Column(1)]
    public int value {get; set;}

    [Column(2)]
    public bool flag {get; set;}

}
