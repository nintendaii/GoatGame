using System;
using System.Collections.Generic;
using Data.Abilities;
using Data.Dmage;
using Formulas;
using Unit;
using Zenject;

namespace Abilities.Processors
{
    public class DamageEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            var value = effectData.value;
            if (targets!=null)
            {
                switch (effectData.damageType)
                {
                    case DamageType.Physical:
                        foreach (var t in targets)
                        {
                            var damage = DamageCalculator.CalculatePhysicalDamage(value, t.unitEntityData.CoreStats.Armor.Value);
                            t.DealDamage(damage);
                        }
                        break;
                    case DamageType.Elemental:
                        foreach (var t in targets)
                        {
                            var fR = t.unitEntityData.CoreStats.ElementalResistances.Fire.Value;
                            var iR = t.unitEntityData.CoreStats.ElementalResistances.Ice.Value;
                            var dR = t.unitEntityData.CoreStats.ElementalResistances.Dark.Value;
                            var lR = t.unitEntityData.CoreStats.ElementalResistances.Lightning.Value;
                            var fD = DamageCalculator.CalculateElementalDamage(effectData.elementalDamage.Fire.Value, fR);
                            var iD = DamageCalculator.CalculateElementalDamage(effectData.elementalDamage.Ice.Value, iR);
                            var dD = DamageCalculator.CalculateElementalDamage(effectData.elementalDamage.Dark.Value, dR);
                            var lD = DamageCalculator.CalculateElementalDamage(effectData.elementalDamage.Lightning.Value, lR);
                            t.DealDamage(fD+iD+dD+lD);
                        }
                        break;
                    case DamageType.Pure:
                        foreach (var t in targets)
                        {
                            t.DealDamage(value);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
        }
    }
}