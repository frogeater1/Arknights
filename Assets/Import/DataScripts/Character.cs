using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arknights.Data
{
    [Serializable]
    public partial class Character
    {
        
        public string id;
        
        public string name;
        
        public string[] avatar_name;
        
        public string[] drag_img_name;
        
        public string[] tachie_name;
        
        public string[] spine_path_正面;
        
        public string[] spine_path_背面;
        
        public string attack_anim_name;
        
        public string[] skills;
        
        public 职业 职业;
        
        public 部署类型 部署类型;
        
        public int 攻击力;
        
        public int 生命上限;
        
        public int 防御;
        
        public int 法术防御;
        
        public float 再部署时间;
        
        public int 部署费用;
        
        public float 攻击间隔;
        
        public string 攻击范围;

        public void CopyTo(Character target)
        {
            target.id = id;
            target.name = name;
            target.avatar_name = avatar_name;
            target.drag_img_name = drag_img_name;
            target.tachie_name = tachie_name;
            target.spine_path_正面 = spine_path_正面;
            target.spine_path_背面 = spine_path_背面;
            target.attack_anim_name = attack_anim_name;
            target.skills = skills;
            target.职业 = 职业;
            target.部署类型 = 部署类型;
            target.攻击力 = 攻击力;
            target.生命上限 = 生命上限;
            target.防御 = 防御;
            target.法术防御 = 法术防御;
            target.再部署时间 = 再部署时间;
            target.部署费用 = 部署费用;
            target.攻击间隔 = 攻击间隔;
            target.攻击范围 = 攻击范围;
        }
    }
}
