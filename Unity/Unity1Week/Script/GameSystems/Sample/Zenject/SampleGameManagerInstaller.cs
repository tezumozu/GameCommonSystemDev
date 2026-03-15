using UnityEngine;
using Zenject;

using Unity1Week_MainGameSystem_v4;

public class SampleGameManagerInstaller : MonoInstaller{

    public override void InstallBindings(){
        var gameManager = new SampleGameManager();

        //GameManager
        Container
            .Bind<I_SceneLoadNoticable>()
            .To<SampleGameManager>()
            .FromInstance(gameManager);	

        Container
            .Bind<SceneGameManager<E_SampleSceneState>>()
            .To<SampleGameManager>()
            .FromInstance(gameManager);

    }
}