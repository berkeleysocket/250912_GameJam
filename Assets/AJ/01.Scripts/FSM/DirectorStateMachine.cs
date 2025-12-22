using System.Collections.Generic;
using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public enum DirectorStateType
    {
        Idle,
        Chase,
    }

    public class DirectorStateMachine
    {
        private Dictionary<DirectorStateType, DirectorState> _stateDictionary = new Dictionary<DirectorStateType, DirectorState>();

        public DirectorState CurrentState { get; private set; }

        public void AddState(DirectorStateType type, DirectorState director)
        {
            _stateDictionary.Add(type, director);
        }
        public void Initialized(DirectorStateType type)
        {
            CurrentState = _stateDictionary[type];
            CurrentState?.Enter();
        }
        public void ChangeState(DirectorStateType state)
        {
            CurrentState?.Exit();
            CurrentState = _stateDictionary[state];
            CurrentState?.Enter();
        }
    }
}