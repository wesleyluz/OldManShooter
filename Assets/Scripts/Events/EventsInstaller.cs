using UnityEngine;
using Zenject;

public class EventsInstaller : MonoInstaller
{
    [SerializeField]
    private EventsManager eventsManager;
    public override void InstallBindings()
    {
        Container.Bind<EventsManager>().FromInstance(eventsManager);
    }
}
