using UnityEngine;
using Zenject;

public class EventsInstaller : MonoInstaller
{
    // C�digo respons�vel por gerar um instalador apartir do script referenciado
    [SerializeField]
    private EventsManager eventsManager;
    public override void InstallBindings()
    {
        Container.Bind<EventsManager>().FromInstance(eventsManager);
    }
}
