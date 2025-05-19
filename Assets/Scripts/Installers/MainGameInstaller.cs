using Combat;
using Turn;
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
        }

        private void InstallFactories()
        {
        }

        private void InstallSignals()
        {
        }
    }
}