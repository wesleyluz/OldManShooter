using UnityEngine;
using Zenject;

public class PoolingInstaller : MonoInstaller
{

    // Código responsável por gerar um instalador apartir do script referenciado
    [SerializeField]
    private BulletsPooling poolingInstaller;

    public override void InstallBindings() 
    {
        Container.Bind<BulletsPooling>().FromInstance(poolingInstaller);
    }
}

