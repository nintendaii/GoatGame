using Abilities;
using Combat;
using Turn;
using UI;
using Unit;
using Zenject;

namespace Installers
{
    public class MainGameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            InstallComponents();
            InstallSignals();
            InstallFactories();
        }

        private void InstallComponents()
        {
            Container.Bind<UnitsContainer>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<TurnManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.Bind<CombatManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UnitsTurnOrderScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UnitControlsScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UnitItemsUIContainer>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UnitEntityController>().FromComponentInHierarchy().AsTransient(); // TEMPORARY FOR TESTING ONLY
            Container.Bind<AbilityProcessorSystem>().AsSingle();
        }

        private void InstallFactories()
        {
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}