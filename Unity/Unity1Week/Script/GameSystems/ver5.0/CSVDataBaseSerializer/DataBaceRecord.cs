using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Csv.Annotations;

namespace GameCommonSystem_V5 {

    [CsvObject]
    public partial class DataBaceRecord{
        [Column(0)]
        public string Id { get; set;}
    }
    
}

