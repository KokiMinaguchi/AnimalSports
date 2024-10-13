using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public class HitPointModel : MonoBehaviour
{
    public readonly int _maxHP = 100;
    
    private readonly SerializableReactiveProperty<int> _hp = new(0);
    public ReadOnlyReactiveProperty<int> HP => _hp;

    // Start is called before the first frame update
    void Start()
    {
        _hp.Value = _maxHP;
    }

    private void OnDestroy()
    {
        _hp.Dispose();
    }

    public void AddDamage(int damage)
    {
        _hp.Value = Mathf.Clamp(_hp.Value - damage, 0, _maxHP);
    }

    public void AddHeal(int heal)
    {
        _hp.Value = Mathf.Clamp(_hp.Value + heal, 0, _maxHP);
    }
}
