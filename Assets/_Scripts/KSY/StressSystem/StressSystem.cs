using System;
using UnityEngine;

using _Scripts.Core.Utility;

namespace Ksy.Scripts.StressSystem
{
    public class StressSystem : MonoSingleton<StressSystem>
    {
        //current value, parm value
        public event Action<int,int> StressIncreased;
        public event Action<int,int> StressDecreased;
        public event Action<int,int> StressReachedMax;
        public event Action<int,int> StressReachedMin;

        [field : SerializeField] public int CurrentStress {get; private set;}
        [field : SerializeField] public int InitStress {get; private set;} = 0;
        [field : SerializeField] public int MaxStress {get; private set;} = 0;
        [field : SerializeField] public int MinStress {get; private set;} = 0;

        #region UnityEvent
        #endregion
        public void Initialize()
        {
            CurrentStress = Mathf.Clamp(InitStress,MinStress,MaxStress);
        }
        public void IncreaseStress(int value)
        {
            if(value <= 0) return;
            if(CurrentStress >= MaxStress) return;

            int before = CurrentStress;
            int after = 0;
            int increaseValue = 0;

            CurrentStress = Mathf.Clamp(CurrentStress + value, MinStress, MaxStress);
            after = CurrentStress;
            increaseValue = after - before;

            StressIncreased.Invoke(CurrentStress, increaseValue);
            if(CurrentStress == MaxStress) StressReachedMax.Invoke(CurrentStress, increaseValue);
        }   
        public void DecreaseStress(int value)
        {
            if(value <= 0) return;
            if(CurrentStress <= MinStress) return;

            int before = CurrentStress;
            int after = 0;
            int decreaseValue = 0;

            CurrentStress = Mathf.Clamp(CurrentStress - value, MinStress, MaxStress);
            after = CurrentStress;
            decreaseValue = before - after;

            StressDecreased.Invoke(CurrentStress, decreaseValue);
            if(CurrentStress == MinStress) StressReachedMin.Invoke(CurrentStress, decreaseValue);
        }
    }
}
