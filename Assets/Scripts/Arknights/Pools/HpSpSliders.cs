using System.Collections;
using System.Collections.Generic;
using Arknights;
using Arknights.Pools;
using Arknights.UGUI;
using UnityEngine;
using UnityEngine.Pool;

public class HpSpSliders : PoolParent
{
    public override void CreatePool(GameObject prefab)
    {
        pool = new ObjectPool<GameObject>(
            () => Instantiate(prefab, transform),
            go => go.SetActive(true),
            go => go.SetActive(false),
            Destroy
        );
    }

    public void ShowHpSp(Character character)
    {
        var go = pool.Get();
        character.hpspSlider = go.GetComponent<HpSpSlider>();
        character.hpspSlider.Init(character);
    }

    public void HideHpSp(Character character)
    {
        pool.Release(character.hpspSlider.gameObject);
        character.hpspSlider.character = null;
        character.hpspSlider = null;
    }
}