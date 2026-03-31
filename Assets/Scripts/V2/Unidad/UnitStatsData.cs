using UnityEngine;

[System.Serializable]
public class UnitStatsData
{
    public float HP;
    public float Ataque;
    public float DistanciaDeAtaque;
    public float CoolDownAttack;

    public UnitStatsData(UnitStatsSO so)
    {
        HP = so.HP;
        Ataque = so.Ataque;
        DistanciaDeAtaque = so.DistanciaDeAtaque;
        CoolDownAttack = so.CoolDownAttack;
    }

    // Constructor vacío para serialización
    public UnitStatsData() { }
}

