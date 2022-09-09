using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{

    // C�digo respons�vel por gerar um instalador apartir do script referenciado
    [SerializeField]
    private Player playerInstall;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(playerInstall);
    }

}
