using UnityEngine;
using Zenject;

public class EventsInstaller : MonoInstaller
{
    // Código responsável por gerar um instalador apartir do script referenciado
    [SerializeField]
    private EventsManager eventsManager;
    public override void InstallBindings()
    {
        Container.Bind<EventsManager>().FromInstance(eventsManager);
    }
}
