using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace GameCommonSystem_V5{

    public class GamePhaseManager : MonoBehaviour{
        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void SetPhaseCheckable(IPhaseUpdateCheckable phaseCheckable){
            phaseCheckable.PhaseUpdateSubject.Subscribe( PhaseId => {
                
            }).AddTo(this);
        }
    }

}
