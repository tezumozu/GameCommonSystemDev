using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCommonSystem_V6{

   [Serializable]
   public abstract class LoadingUIUpdater : MonoBehaviour {
      public abstract void IsActiveLoadingUI(bool flag);
   }

}