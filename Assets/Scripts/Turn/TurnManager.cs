using System;
using System.Collections.Generic;
using System.Linq;
using Data.Turn;
using Global;
using Unit;
using UnityEngine;
using Zenject;

namespace Turn
{
    public class TurnManager: MonoBehaviour
    {
        public List<TurnUnitData> unitsInGame = new();
        public List<TurnUnitData> turnQueue = new();
        [Inject] private readonly UnitsContainer _unitsContainer;
        public int CurrentTurn;
        private float _actionThreshold;
        private int _currentTick;


        public void Init(List<UnitEntityController> units)
        {
            _actionThreshold = GlobalConstants.Balance.ACTION_THRESHOLD;
            CurrentTurn = 0;
            foreach (var u in units)
            {
                AddUnitToQueue(u);
            }
        }
        
        private TurnUnitData SimulateUntilNextTurn()
        {
            while (true)
            {
                foreach (var unit in unitsInGame)
                {
                    unit.ActionPoints += unit.Speed / 100f; // slow fill, as % of threshold
                }

                var readyUnits = unitsInGame.Where(u => u.ActionPoints >= _actionThreshold).ToList();

                if (readyUnits.Any())
                {
                    var nextUnit = readyUnits
                        .OrderByDescending(u => u.ActionPoints)
                        .ThenByDescending(u => u.Speed)
                        .First();

                    nextUnit.ActionPoints -= _actionThreshold;
                    _currentTick++;
                    return nextUnit;
                }
            }
        }

        private void AdvanceTurn()
        {
            CurrentTurn++;
        }
        
        public TurnUnitData ExecuteTurn()
        {
            AdvanceTurn();
            return SimulateUntilNextTurn();
        }

        public List<TurnUnitData> GenerateTurnOrder(int numTurns)
        {
            turnQueue.Clear();
            // Save initial action points
            var originalTicks = _currentTick;
            var originalAp = unitsInGame.ToDictionary(u => u.UnitEntityController.unitEntityData.Id, u => u.ActionPoints);
            
            for (int i = 0; i < numTurns; i++)
            {
                var nextUnit = SimulateUntilNextTurn();
    
                // Add a copy to the turn queue to avoid reference issues
                turnQueue.Add(new TurnUnitData
                {
                    UnitEntityController = nextUnit.UnitEntityController,
                    Speed = nextUnit.Speed,
                    ActionPoints = nextUnit.ActionPoints
                });
            }

            // Restore original action points
            foreach (var unit in unitsInGame)
            {
                unit.ActionPoints = originalAp[unit.UnitEntityController.unitEntityData.Id];
            }
            _currentTick = originalTicks;

            return turnQueue;
        }
        
        public void Reset()
        {
            // Reset action points for all units
            foreach (var unit in unitsInGame)
            {
                unit.ActionPoints = 0f;
            }
            CurrentTurn = 0;
            turnQueue.Clear();
        }

        public void AddUnitToQueue(UnitEntityController unitEntityController)
        {
            if (unitsInGame.Find(x => x.UnitEntityController == unitEntityController) != null) return;
            var turnData = new TurnUnitData
            {
                UnitEntityController = unitEntityController,
                ActionPoints = 0f,
                Speed = (int)unitEntityController.unitEntityData.CoreStats.Speed.Value
            };
            unitsInGame.Add(turnData);
        }

        public void RemoveUnitFromQueue(UnitEntityController currentTargetEntity)
        {
            //TODO move this to single call (e.g the unit will check if he is dead and send signal)
            var unitToRemove = unitsInGame.Find(x =>
                x.UnitEntityController.unitEntityData.Id == currentTargetEntity.unitEntityData.Id);
            unitsInGame.Remove(unitToRemove);
        }
    }
}