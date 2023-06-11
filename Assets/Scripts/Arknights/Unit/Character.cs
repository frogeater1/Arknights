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

namespace Arknights
{
    public class Character : Unit
    {
        public Data.Character loadData;

        public string[] avatarURLs;

        public string[] dragImgURLs;

        public string[] tachieURLs;

        public List<Vector2Int> attackRange;

        public Skills.Skill[] skills;

        public SkeletonAnimation skeletonAnimation;

        public float attackDuration; //普攻时间
        public float skillDuration; //技能时间 //注意这个技能持续时间是放哪个技能时动态设置的

        public int skinIdx;
        public int skillIdx;

        ///以下是战斗中临时的数据
        public int cardListIdx; //在卡盒中的索引
        
        public 部署类型 当前部署类型;

        //之前是朝左还是朝右,不保存上下,默认朝右,用来选择的
        private 方向 oldSelectDir = 方向.右;


        private 方向 attackDir = 方向.右; //这个是角色攻击朝向，用来计算攻击范围的，跟上面那个没关系,高台单位在下场时即固定，地面单位为实际移动的方向，注意跟动画方向无关。

        private CharacterState state = CharacterState.手牌;

        //以下两个bool的作用是,移动时，不能正在普攻或放技能时且不能有攻击目标，普攻时，不能正在普攻或放技能，放技能时，不能正在放技能
        private bool isSkilling = false; //正在放技能中
        private bool isAttacking = false; //正在普攻中

        public Vector2Int logicPos;

        public Skill curSkill;
        public int curSp;
        public int maxSp;
        public int curHp;
        public int maxHp;

        private Unit target;
        public HpSpSlider hpspSlider;

        public float spTiming = 0;
        public float attackTiming = 0;
        public float skillTiming = 0;

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
                skeletonAnimation.transform.rotation = Quaternion.Euler(60, 0, 0);
                skeletonAnimation.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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


        //这里不接收事件而是手动调用，因为方向会影响攻击范围，改变了实际逻辑。
        public void ChangeDirection(方向 dir)
        {
            switch (dir)
            {
                case 方向.右:
                    attackDir = dir;
                    skeletonAnimation.skeleton.ScaleX = 1;
                    oldSelectDir = dir;
                    break;
                case 方向.左:
                    attackDir = dir;
                    oldSelectDir = dir;
                    skeletonAnimation.skeleton.ScaleX = -1;
                    break;
                case 方向.上:
                    attackDir = dir;
                    //切换成背面的spine，暂时只有正面的
                    skeletonAnimation.skeleton.ScaleX = oldSelectDir == 方向.右 ? 1 : -1;
                    break;
                case 方向.下:
                    attackDir = dir;
                    skeletonAnimation.skeleton.ScaleX = oldSelectDir == 方向.右 ? 1 : -1;
                    break;
            }
        }

        public void Init()
        {
            //tmp 暂时写死的
            skinIdx = 0;

            if (loadData.id == "1")
            {
                team = Team.Blue;
                skillIdx = 2;
            }

            if (loadData.id == "2")
            {
                team = Team.Red;
                skillIdx = 3;
            }

            foreach (var s in skills)
            {
                if (s is 主动)
                {
                    var 主动 = (主动)s;
                    主动.level = 1;
                }
            }

            attackDuration = loadData.攻击间隔;

            skeletonAnimation.skeleton.Data.FindAnimation(loadData.attack_anim_name).Duration = attackDuration;

            maxHp = loadData.生命上限;
            curHp = maxHp;

            curSkill = skills[skillIdx];

            maxSp = curSkill.loadData.cost_e[curSkill.level - 1];
            curSp = curSkill.loadData.start_e[curSkill.level - 1];
        }

        public void 下场()
        {
            state = CharacterState.下场;
            EventManager.LogicUpdate += LogicUpdate;
            Map.Instance.AddUnit(this);
            Game.Instance.hpSpSliders.ShowHpSp(this);
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


            skeletonAnimation.state.SetAnimation(0, "Start", false);
            //播放完毕后切到idle
            skeletonAnimation.state.Complete += (trackEntry) => { skeletonAnimation.state.SetAnimation(0, "Idle", true); };
        }

        private void OnMouseUpAsButton()
        {
            if (state != CharacterState.下场) return;
                //todo: if (team != Game.Instance.team) return;
            //必须先设置当前操作角色，否则directionSelect会找不到该在哪显示
            Game.Instance.CharacterManager.curCharacter = this;
            Game.Instance.ui_battle.CancelSelect();
            Game.Instance.ui_directionSelect.ShowCtrl(this);
        }

        public void 回收()
        {
            EventManager.LogicUpdate -= LogicUpdate;
            Map.Instance.RemoveUnit(this);
            Hide();
            Game.Instance.hpSpSliders.HideHpSp(this);
            state = CharacterState.手牌;
        }

        public void Hide()
        {
            transform.position = new Vector3(1000, 0, 0);
            logicPos = new Vector2Int(1000, 0);
        }

        private void LogicUpdate()
        {
            //更新sp数据
            if (curSp < maxSp)
            {
                //每60逻辑帧即1秒钟增加1
                if (Game.Instance.logicFrame - spTiming >= 60)
                {
                    curSp++;
                    spTiming = Game.Instance.logicFrame;
                }
            }

            //刷新血条蓝条
            if (hpspSlider)
                hpspSlider.Refresh();


            //判断攻击和移动
            if (isSkilling)
            {
                if (Game.Instance.logicFrame - skillTiming >= 60 * skillDuration)
                {
                    isSkilling = false;
                }
            }
            else if (isAttacking)
            {
                if (Game.Instance.logicFrame - attackTiming >= 60 * attackDuration)
                {
                    isAttacking = false;
                }
            }

            if (isSkilling || isAttacking) return;
      
            if (!target)
                SeekTarget();
            if (target)
            {
                Attack();
            }
            else
            {
                if (当前部署类型 == 部署类型.地面)
                {
                    Move();
                }
            }
        }

        private void Move()
        {
            //todo
        }

        private void Attack()
        {
            skeletonAnimation.state.SetAnimation(0, loadData.attack_anim_name, false);
            attackTiming = Game.Instance.logicFrame;
            isAttacking = true;
            skills[0].Use(target);
        }

        private void SeekTarget()
        {
            foreach (var range in attackRange)
            {
                var logicGrid = Map.Instance.CalculPos(logicPos, attackDir, range);
                target = Map.Instance.CheckGrid(logicGrid, team == Team.Blue ? Team.Red : Team.Blue);
                if (target)
                    break;
            }
        }

        public void FixedPos(int logicX, int logicZ)
        {
            logicPos = new Vector2Int(logicX, logicZ);
            var grid_type = Map.Instance.GetGrid(logicX, logicZ)?.type;
            transform.position = new Vector3(logicX + 0.5f, grid_type == GridType.站人高台 ? 0.6f : 0, logicZ + 0.3f);
        }
    }
}