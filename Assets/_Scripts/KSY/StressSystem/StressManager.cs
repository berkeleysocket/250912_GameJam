using System;
using UnityEngine;

using _Scripts.Core.Utility;

namespace Ksy.Scripts.StressSystem
{
    public class StressManager : MonoSingleton<StressManager>
    {
        //current value, parm value
        public event Action<StressEventArgs> StressIncreased;
        public event Action<StressEventArgs> StressDecreased;
        public event Action<StressEventArgs> StressReachedMax;
        public event Action<StressEventArgs> StressReachedMin;

        [field : SerializeField] public int CurrentStress {get; private set;}
        [field : SerializeField] public int InitStress {get; private set;} = 0;
        [field : SerializeField] public int MaxStress {get; private set;} = 0;
        [field : SerializeField] public int MinStress {get; private set;} = 0;

        #region UnityEvent
        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }
        #endregion
        public void Initialize()
        {
            CurrentStress = Mathf.Clamp(InitStress,MinStress,MaxStress);
        }
        public void IncreaseStress(int value)
        {
            if(value <= 0) return;
            if(CurrentStress >= MaxStress) return;

            Debug.Log("IncreaseStress");

            int before = CurrentStress;
            int after = 0;
            int increaseValue = 0;

            CurrentStress = Mathf.Clamp(CurrentStress + value, MinStress, MaxStress);
            after = CurrentStress;
            increaseValue = after - before;

            var args = new StressEventArgs(CurrentStress, increaseValue);

            StressIncreased?.Invoke(args);
            if(CurrentStress == MaxStress) StressReachedMax?.Invoke(args);
        }   
        public void DecreaseStress(int value)
        {
            if(value <= 0) return;
            if(CurrentStress <= MinStress) return;

            Debug.Log("DecreaseStress");

            int before = CurrentStress;
            int after = 0;
            int decreaseValue = 0;

            CurrentStress = Mathf.Clamp(CurrentStress - value, MinStress, MaxStress);
            after = CurrentStress;
            decreaseValue = before - after;

            var args = new StressEventArgs(CurrentStress, decreaseValue);

            StressDecreased?.Invoke(args);
            if(CurrentStress == MinStress) StressReachedMin?.Invoke(args);
        }
    }
}
