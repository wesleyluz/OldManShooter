using UnityEngine;
using Zenject;

public class PoolingInstaller : MonoInstaller
{
    [SerializeField]
    private BulletsPooling poolingInstaller;

    public override void InstallBindings() 
    {
        Container.Bind<BulletsPooling>().FromInstance(poolingInstaller);
    }
}

