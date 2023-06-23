using System;
using System.Collections.Generic;
using System.Linq;
using Arknights.Skills;
using Arknights.UGUI;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Spine.Unity;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arknights
{
    public class Character : Unit
    {
        public Data.Character loadData;

        public string[] avatarURLs;

        public string[] dragImgURLs;

        public string[] tachieURLs;


        public Skills.Skill[] skills;


        private float _attackDuration;

        public float attackDuration
        {
            get => _attackDuration;

            set
            {
                //bugtotest
                var a = skeletonAnimation;
                var b = a.skeleton;
                var c = b.Data;
                var d = c.FindAnimation(loadData.attack_anim_name);
                var e = d.Duration;
                skeletonAnimation.skeleton.Data.FindAnimation(loadData.attack_anim_name).Duration = attackDuration;
                _attackDuration = value;
            }
        } //普攻时间


        public float skillDuration; //技能时间 //注意这个技能持续时间是放哪个技能时动态设置的

        public int skinIdx;

        [FormerlySerializedAs("skillIdx")]
        public int carryingSkillIdx;

        ///以下是战斗中临时的数据
        public int cardListIdx; //在卡盒中的索引

        //之前是朝左还是朝右,不保存上下,默认朝右,选择UI用的的，不是真正的方向，不要用来判断朝向
        private 方向 oldSelectDir = 方向.右;


        public Skill curSkill;
        public int curSp;
        public int maxSp;
        public float coolDown;
        public float lastCoolDown; //剩余冷却时间

        public Unit target;
        public Vector2Int targetLogicPos; //记录一下目标的位置，用来判断是否需要重新索敌

#if UNITY_EDITOR
        public void Load(Data.Character data)
        {
            loadData = data;
            int skinNum = loadData.avatar_name.Length;

            avatarURLs = new string[skinNum];
            dragImgURLs = new string[skinNum];
            tachieURLs = new string[skinNum];
            for (int i = 0; i < skinNum; i++)
            {
                avatarURLs[i] = UIPackage.GetItemURL("Arknights", loadData.avatar_name[i]);
                dragImgURLs[i] = UIPackage.GetItemURL("Arknights", loadData.drag_img_name[i]);
                tachieURLs[i] = UIPackage.GetItemURL("Arknights", loadData.tachie_name[i]);
                var sda = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>(
                    $"Assets/Prefabs/Spines/{loadData.spine_path_正面[i]}.asset");
                skeletonAnimation = SkeletonAnimation.NewSkeletonAnimationGameObject(sda);
                skeletonAnimation.AnimationName = "Idle";
                skeletonAnimation.transform.localPosition = new Vector3(0, 0, 0);
                skeletonAnimation.transform.rotation = Quaternion.Euler(Settings.ROTATIONANGLE, 0, 0);
                skeletonAnimation.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                skeletonAnimation.skeleton.Data.FindAnimation("Start").Duration = 0.5f;
                skeletonAnimation.transform.SetParent(transform);
            }


            Data.SkillRange skillRange = Data.Database.SkillRange.First(x => x.id == loadData.攻击范围);
            attackRange = skillRange.Range;

            var list = new List<Skills.Skill>();
            foreach (string skillId in loadData.skills)
            {
                Data.Skill skillData = Data.Database.Skill.First(s => s.id == skillId);
                var script = Type.GetType(skillData.script);
                var skill = (Skills.Skill)gameObject.AddComponent(script);
                skill.Load(skillData);
                list.Add(skill);
            }

            skills = list.ToArray();
        }
#endif

        public void Init(Player p)
        {
            //tmp
            skinIdx = 0;

            if (loadData.id == "1")
            {
                carryingSkillIdx = 2;
            }

            if (loadData.id == "2")
            {
                carryingSkillIdx = 3;
            }
            

            foreach (var s in skills)
            {
                if (s.loadData.permanent)
                {
                    s.carrying = true;
                }
                else
                {
                    if (carryingSkillIdx == int.Parse(s.loadData.id))
                    {
                        s.carrying = true;
                    }
                }

                s.level = 1;
            }
            //tmp end

            player = p;

            attackDuration = 1f / loadData.攻击速度;

            attack = loadData.攻击力;
            maxHp = loadData.生命上限;
            curHp = maxHp;
            coolDown = loadData.再部署时间;
            lastCoolDown = 0;

            curSkill = skills[carryingSkillIdx];

            skillDuration = curSkill.loadData.duration[curSkill.level - 1];
            
            //bugtotest
            Debug.Log(curSkill);
            Debug.Log(curSkill.loadData);
            Debug.Log(curSkill.loadData.cost_e.Length);
            Debug.Log(curSkill.level);
            maxSp = curSkill.loadData.cost_e[curSkill.level - 1];
            curSp = curSkill.loadData.start_e[curSkill.level - 1];

            fsmSystem = new FSMSystem(this);
            Debug.Log("init");
            fsmSystem.SwitchState(EFSMState.Card);
            EventManager.LogicUpdate += LogicUpdate;
        }


        public void FixedPos(int logicX, int logicZ)
        {
            logicPos = new Vector2Int(logicX, logicZ);
            var grid_type = Map.Instance.GetGrid(logicX, logicZ)?.type;
            transform.position = new Vector3(logicX + 0.5f, grid_type == GridType.站人高台 ? 0.6f : 0, logicZ + 0.3f);
        }


        //这里不接收事件而是手动调用，因为方向会影响攻击范围，改变了实际逻辑。
        public void ChangeDirection(方向 dir)
        {
            switch (dir)
            {
                case 方向.右:
                    attackDir = dir;
                    oldSelectDir = dir;
                    break;
                case 方向.左:
                    attackDir = dir;
                    oldSelectDir = dir;
                    break;
                case 方向.上:
                    attackDir = dir;
                    //切换成背面的spine，暂时只有正面的
                    break;
                case 方向.下:
                    attackDir = dir;
                    break;
            }

            skeletonAnimation.skeleton.ScaleX = oldSelectDir switch
            {
                方向.右 => 1,
                方向.左 => -1,
                _ => 1
            };
        }

        public void Enter()
        {
            Map.Instance.AddUnit(this);
            Game.Instance.PoolManager.hpSpSliders.ShowHp(this);
            if (loadData.部署类型 == 部署类型.Both)
            {
                var grid_type = Map.Instance.GetGrid(logicPos).type;
                当前部署类型 = grid_type switch
                {
                    GridType.站人地面 => 部署类型.地面,
                    GridType.站人高台 => 部署类型.高台,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                当前部署类型 = loadData.部署类型;
            }

            fsmSystem.SwitchState(EFSMState.Start);
        }

        private void OnMouseUpAsButton()
        {
            if (player.team != Game.Instance.me.team) return;
            Game.Instance.ui_battle.CancelSelect();
            //必须先设置当前操作角色，否则directionSelect会找不到该在哪显示
            Game.Instance.CharacterManager.curCharacter = this;
            Game.Instance.ui_directionSelect.ShowCtrl(this);
        }

        public void Exit()
        {
            lastCoolDown = coolDown;
            fsmSystem.SwitchState(EFSMState.Card);
            Map.Instance.RemoveUnit(this);
            Hide();
            Game.Instance.PoolManager.hpSpSliders.HideHp(this);
            if (player.playerId == Game.Instance.me.playerId)
            {
                Game.Instance.ui_battle.回收(cardListIdx);
            }
        }

        public void Hide()
        {
            transform.position = new Vector3(1000, 0, 0);
            logicPos = new Vector2Int(1000, 0);
        }

        private void LogicUpdate()
        {
            fsmSystem.states[fsmSystem.curState].LogicUpdate();
        }

        public void DoAttack()
        {
            skills[0].Use(this, target);
        }

        public void Skill()
        {
            fsmSystem.SwitchState(EFSMState.Skill);
        }
        
        public void DoSkill()
        {
            curSkill.Use(this, target);
        }

        public void SeekTarget()
        {
            foreach (Vector2Int range in attackRange)
            {
                Vector2Int logicGrid = Map.Instance.CalculPos(logicPos, attackDir, range);
                target = Map.Instance.CheckGrid(logicGrid, player.team);
                if (!target) continue;
                targetLogicPos = target.logicPos;
                break;
            }
        }
    }
}