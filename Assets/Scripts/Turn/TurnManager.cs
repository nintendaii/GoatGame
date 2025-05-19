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
        private int _currentTurn;
        private float _actionThreshold;


        public void Init(List<UnitEntityController> units)
        {
            _actionThreshold = GlobalConstants.Balance.ACTION_THRESHOLD;
            _currentTurn = 0;
            foreach (var u in units)
            {
                var turnData = new TurnUnitData
                {
                    UnitEntityController = u,
                    ActionPoints = 0f,
                    Speed = (int)u.unitEntityData.CoreStats.Speed.Value
                };
                unitsInGame.Add(turnData);
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
                    _currentTurn++;
                    return nextUnit;
                }
            }
        }
        
        public TurnUnitData GetNextUnit()
        {
            //REPLACE LATER
            //CheckAliveStatus();
            return SimulateUntilNextTurn();
        }

        private void CheckAliveStatus()
        {
            foreach (var u in _unitsContainer.UnitEntityContainer)
            {
                if (!u.IsAlive)
                {
                    var unitToRemove = unitsInGame.Find(x => x.UnitEntityController.unitEntityData.Id == u.unitEntityData.Id);
                    unitsInGame.Remove(unitToRemove);
                }
            }
        }

        public List<TurnUnitData> GenerateTurnOrder(int numTurns)
        {
            turnQueue.Clear();
            // Save initial action points
            var originalAp = unitsInGame.ToDictionary(u => u.UnitEntityController.unitEntityData.Id, u => u.ActionPoints);
            
            for (int i = 0; i < numTurns; i++)
            {
                var nextUnit = GetNextUnit();
    
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

            return turnQueue;
        }
        
        public void Reset()
        {
            // Reset action points for all units
            foreach (var unit in unitsInGame)
            {
                unit.ActionPoints = 0f;
            }
            _currentTurn = 0;
            turnQueue.Clear();
        }

        public void RemoveUnitFromQueue(UnitEntityController currentTargetEntity)
        {
            var unitToRemove = unitsInGame.Find(x =>
                x.UnitEntityController.unitEntityData.Id == currentTargetEntity.unitEntityData.Id);
            unitsInGame.Remove(unitToRemove);
        }
    }
}