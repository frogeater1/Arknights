using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Arknights
{
    public class FSMSystem
    {
        public Character character;
        public EFSMState curState;

        public Dictionary<EFSMState, FSMState> states = new();

        public FSMSystem(Character character)
        {
            this.character = character;
            states.Add(EFSMState.Start, new StartState(this));
            states.Add(EFSMState.Idle, new IdleState(this));
            states.Add(EFSMState.Move, new MoveState(this));
            states.Add(EFSMState.Attack, new AttackState(this));
            states.Add(EFSMState.Die, new DieState(this));
            states.Add(EFSMState.Skill, new SkillState(this));
            states.Add(EFSMState.Card, new CardState(this));
        }

        public void SwitchState(EFSMState state)
        {
            Debug.Log("SwitchState:" + state);
            curState = state;
            states[curState].Action();
        }
    }
}