using System.Collections;
using System.Collections.Generic;
using Arknights;
using Arknights.Pools;
using Arknights.UGUI;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Pool;

public class HpSpSliders : MonoBehaviour
{
    public HpSpSlider[] prefabs;
    public Dictionary<string, ObjectPool<HpSpSlider>> pools = new();

    public void CreatePool()
    {
        foreach (HpSpSlider prefab in prefabs)
        {
            pools.Add(prefab.name, new ObjectPool<HpSpSlider>(
                () => Instantiate(prefab, transform),
                slider => slider.gameObject.SetActive(true),
                slider =>
                {
                    slider.gameObject.SetActive(false);
                    slider.unit = null;
                },
                Destroy
            ));
        }
    }

    public void ShowHp(Unit unit)
    {
        var slider = (HpSlider)pools["HpSlider"].Get();
        unit.hpSlider = slider;
        slider.Init(unit);
    }

    public void HideHp(Character character)
    {
        pools["HpSlider"].Release(character.hpSlider);
        character.hpSlider = null;
    }
}