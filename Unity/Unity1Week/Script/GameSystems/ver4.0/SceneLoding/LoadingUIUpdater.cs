using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week_MainGameSystem_v4{

   [Serializable]
   public abstract class LoadingUIUpdater : MonoBehaviour {
      public abstract void IsActiveLoadingUI(bool flag);
   }

}

