using UnityEngine;
using Zenject;

public class PoolingInstaller : MonoInstaller
{

    // C�digo respons�vel por gerar um instalador apartir do script referenciado
    [SerializeField]
    private BulletsPooling poolingInstaller;

    public override void InstallBindings() 
    {
        Container.Bind<BulletsPooling>().FromInstance(poolingInstaller);
    }
}

