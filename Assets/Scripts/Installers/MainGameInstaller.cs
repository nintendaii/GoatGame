using Abilities;
using Combat;
using Commands;
using Signals;
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
            Container.Bind<AbilityCooldownSystem>().AsSingle();
            Container.Bind<StatusEffectSystem>().AsSingle().NonLazy();
        }

        private void InstallFactories()
        {
        }

        private void InstallSignals()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<AdvanceNextTurnSignal>();
            Container.BindSignal<AdvanceNextTurnSignal>().ToMethod<AdvanceNextTurnCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<ProcessAbilitySignal>();
            Container.BindSignal<ProcessAbilitySignal>().ToMethod<ProcessAbilityCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<DealAttackDamageUnitSignal>();
            Container.BindSignal<DealAttackDamageUnitSignal>().ToMethod<DealAttackDamageUnitCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<DealEffectDamageUnitSignal>();
            Container.BindSignal<DealEffectDamageUnitSignal>().ToMethod<DealEffectDamageUnitCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<HealEffectUnitSignal>();
            Container.BindSignal<HealEffectUnitSignal>().ToMethod<HealEffectUnitCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<DispelStatusEffectSignal>();
            Container.BindSignal<DispelStatusEffectSignal>().ToMethod<DispelStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<StunStatusEffectSignal>();
            Container.BindSignal<StunStatusEffectSignal>().ToMethod<StunStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<PhysicalDamageImmunityStatusEffectSignal>();
            Container.BindSignal<PhysicalDamageImmunityStatusEffectSignal>().ToMethod<PhysicalDamageImmunityStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<SpellImmunityStatusEffectSignal>();
            Container.BindSignal<SpellImmunityStatusEffectSignal>().ToMethod<SpellImmunityStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<DisarmStatusEffectSignal>();
            Container.BindSignal<DisarmStatusEffectSignal>().ToMethod<DisarmStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<SilenceStatusEffectSignal>();
            Container.BindSignal<SilenceStatusEffectSignal>().ToMethod<SilenceStatusEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<DeathEffectSignal>();
            Container.BindSignal<DeathEffectSignal>().ToMethod<DeathEffectCommand>(command => command.Execute).FromNew();
            Container.DeclareSignal<ResurrectionEffectSignal>();
            Container.BindSignal<ResurrectionEffectSignal>().ToMethod<ResurrectionEffectCommand>(command => command.Execute).FromNew();
        }
    }
}