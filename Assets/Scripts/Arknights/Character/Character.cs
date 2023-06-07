using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using FairyGUI;
using Spine.Unity;
using UnityEditor;
using UnityEngine;

namespace Arknights
{
    public class Character : MonoBehaviour
    {
        public Data.Character loadData;

        public string[] avatarURLs;

        public string[] dragImgURLs;

        public string[] tachieURLs;

        public List<Vector2Int> attackRange;

        public Skills.Skill[] skills;

        //tmp 当前在用哪套皮肤
        public int skinIdx;
        
        //之前是朝左还是朝右,不保存上下,默认朝右
        private 方向 oldDir = 方向.右;

        public SkeletonAnimation skeletonAnimation;

#if UNITY_EDITOR
        public void Load(Data.Character data)
        {
            skinIdx = 0; //暂时只有一套衣服
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
                var sda = AssetDatabase.LoadAssetAtPath<SkeletonDataAsset>($"Assets/Prefabs/Spines/{loadData.spine_path_正面[i]}.asset");
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
                list.Add(skill);
            }
            skills = list.ToArray();
        }
#endif


        private void OnEnable()
        {
            EventManager.ChangeDirection += OnChangeDirection;
        }

        private void OnDisable()
        {
            EventManager.ChangeDirection -= OnChangeDirection;
        }

        private void OnChangeDirection(方向 dir)
        {
            switch (dir)
            {
                case 方向.右:
                    skeletonAnimation.skeleton.ScaleX = 1;
                    oldDir = dir;
                    break;
                case 方向.左:
                    oldDir = dir;
                    skeletonAnimation.skeleton.ScaleX = -1;
                    break;
                case 方向.上:
                    //切换成背面的spine，暂时只有正面的
                    skeletonAnimation.skeleton.ScaleX = oldDir == 方向.右 ? 1 : -1;
                    break;
                case 方向.下:
                    skeletonAnimation.skeleton.ScaleX = oldDir == 方向.右 ? 1 : -1;
                    break;
            }
        }


        public void 下场()
        {
            skeletonAnimation.AnimationName = "Start";
            skeletonAnimation.state.SetAnimation(0,"Start",false);
            //播放完毕后切到idle
            skeletonAnimation.state.Complete += (trackEntry) =>
            {
                skeletonAnimation.AnimationName = "Idle";
                skeletonAnimation.state.SetAnimation(0,"Idle",true);
            };
        }
    }
}