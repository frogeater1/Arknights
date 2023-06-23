using Spine.Unity;
using UnityEngine;

namespace Arknights
{
    public abstract class FSMState
    {
        public FSMSystem fsmSystem;
        protected int logicFrame;
        protected int spFrame; //用来计算回蓝的帧点，存活状态下每帧+1，死亡状态下清零

        protected FSMState(FSMSystem fsmSystem)
        {
            this.fsmSystem = fsmSystem;
        }


        public virtual void OnEnter()
        {
        }

        public abstract void LogicUpdate();

        public abstract void Action();

        public virtual void Exit()
        {
        }
    }

    public class IdleState : FSMState
    {
        public IdleState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            var character = fsmSystem.character;
            logicFrame++;
            spFrame++;

            if (character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }

            if (spFrame >= Settings.FPS)
            {
                character.curSp++;
                spFrame = 0;
            }

            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }

            if (!character.target)
                character.SeekTarget();
            else
            {
                //目标移动了的话重新索敌
                if (character.targetLogicPos != character.target.logicPos)
                {
                    character.SeekTarget();
                }
            }

            if (character.target)
            {
                fsmSystem.SwitchState(EFSMState.Attack);
            }
            else
            {
                // if (character.当前部署类型 == 部署类型.地面)
                // {
                //     fsmSystem.SwitchState(EFSMState.Move);
                // }
            }
        }

        public override void Action()
        {
            logicFrame = 0;
            fsmSystem.character.skeletonAnimation.state.SetAnimation(0, "Idle", true);
        }
    }

    public class StartState : FSMState
    {
        public StartState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            logicFrame++;
            spFrame++;

            if (logicFrame >= Settings.FPS * 0.5f)
            {
                fsmSystem.SwitchState(EFSMState.Idle);
            }

            var character = fsmSystem.character;

            if (character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }

            if (spFrame >= Settings.FPS)
            {
                character.curSp++;
                spFrame = 0;
            }

            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }
        }

        public override void Action()
        {
            logicFrame = 0;
            spFrame = 0;
            fsmSystem.character.skeletonAnimation.state.SetAnimation(0, "Start", false);
        }
    }

    public class DieState : FSMState
    {
        public DieState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            logicFrame++;
            if (logicFrame >= Settings.FPS)
            {
                fsmSystem.character.Exit();
            }
        }

        public override void Action()
        {
            logicFrame = 0;
            fsmSystem.character.skeletonAnimation.state.SetAnimation(0, "Die", false);
        }
    }

    public class AttackState : FSMState
    {
        public bool done = false;

        public AttackState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            logicFrame++;
            spFrame++;
            var character = fsmSystem.character;
            if (character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }

            if (spFrame >= Settings.FPS)
            {
                character.curSp++;
                spFrame = 0;
            }

            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }


            if (done == false && logicFrame >= Settings.FPS * character.attackDuration * 0.5f)
            {
                done = true;
                character.DoAttack();
            }

            if (done && logicFrame >= Settings.FPS * character.attackDuration)
            {
                fsmSystem.SwitchState(EFSMState.Idle);
            }
        }

        public override void Action()
        {
            var character = fsmSystem.character;
            logicFrame = 0;
            done = false;
            fsmSystem.character.skeletonAnimation.state.SetAnimation(0, character.loadData.attack_anim_name, false);
        }
    }

    public class MoveState : FSMState
    {
        public MoveState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            spFrame++;

            if (fsmSystem.character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }

            var character = fsmSystem.character;
            if (spFrame >= Settings.FPS)
            {
                character.curSp++;
                spFrame = 0;
            }

            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }
        }

        public override void Action()
        {
        }
    }

    public class SkillState : FSMState
    {
        public bool done = false;
        public GameObject skillEffect;

        public SkillState(FSMSystem fsm) : base(fsm)
        {
        }

        public override void LogicUpdate()
        {
            logicFrame++;
            var character = fsmSystem.character;
            if (character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }


            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }


            if (done == false && logicFrame >= Settings.FPS * character.skillDuration * 0.5f)
            {
                done = true;
                character.DoSkill();
            }

            if (done && logicFrame >= Settings.FPS * character.skillDuration)
            {
                fsmSystem.SwitchState(EFSMState.Idle);
            }
        }

        public override void Action()
        {
            var character = fsmSystem.character;
            logicFrame = 0;
            done = false;
            character.curSp = 0;
            fsmSystem.character.skeletonAnimation.state.SetAnimation(0, "Skill", false);

            Game.Instance.PoolManager.skillEffects.ShowEffect("effect_" + character.curSkill.loadData.name, character.logicPos, 1000).Forget();
        }
    }

    public class CardState : FSMState
    {
        public CardState(FSMSystem fsmSystem) : base(fsmSystem)
        {
        }

        public override void LogicUpdate()
        {
            //如果不是自己的角色就不用管
            if (fsmSystem.character.player.playerId != Game.Instance.me.playerId)
                return;

            logicFrame++;
            if (logicFrame >= Settings.FPS)
            {
                fsmSystem.character.lastCoolDown -= 1;
                logicFrame = 0;
            }
        }

        public override void Action()
        {
            logicFrame = 0;
            //可能需要移除所以倒序遍历
            for (int i = fsmSystem.character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = fsmSystem.character.buffs[i];
                buff.Remove();
            }
        }
    }

    public class StiffState : FSMState
    {
        public StiffState(FSMSystem fsmSystem) : base(fsmSystem)
        {
        }

        public override void LogicUpdate()
        {
            var character = fsmSystem.character;
            spFrame++;

            if (character.curHp <= 0)
            {
                fsmSystem.SwitchState(EFSMState.Die);
                return;
            }

            if (spFrame >= Settings.FPS)
            {
                character.curSp++;
                spFrame = 0;
            }

            //可能需要移除所以倒序遍历
            for (int i = character.buffs.Count - 1; i >= 0; i--)
            {
                var buff = character.buffs[i];
                buff.LogicUpdate();
            }
        }

        public override void Exit()
        {
            fsmSystem.character.skeletonAnimation.timeScale = 1;
        }

        public override void Action()
        {
            //停止动画
            fsmSystem.character.skeletonAnimation.timeScale = 0;
        }
    }
}