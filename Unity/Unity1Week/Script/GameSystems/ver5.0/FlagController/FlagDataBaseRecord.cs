using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Csv.Annotations;

namespace GameCommonSystem_V5 {

    [CsvObject]
    public partial class FlagDataBaseRecord : DataBaceRecord {

        [Column(1)]
        public string FlagName ;

        [Column(2)]
        public bool DefaultFlagValue ;
    }

}
