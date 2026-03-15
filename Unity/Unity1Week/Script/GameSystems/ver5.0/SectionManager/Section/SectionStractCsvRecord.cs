using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csv.Annotations;

namespace GameCommonSystem_V5{

    [CsvObject]
    public partial class SectionStractCsvRecord : DataBaceRecord{
        [Column(1)]
        public string PearntSectionId {get; set;}

        [Column(2)]
        public string PhaseId {get; set;}

        [Column(3)]
        public bool IsStart {get; set;}

        [Column(4)]
        public bool IsLast {get; set;}

        [Column(5)]
        public string NextSectionId {get; set;}
    }

}