using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csv.Annotations;

namespace GameCommonSystem_V5{

    [CsvObject]
    public partial class MissionFlagStractRecord : DataBaceRecord{
        [Column(1)]
        public string PearntPhaseId {get; set;}

        [Column(2)]
        public string MissionId {get; set;}

    }

}