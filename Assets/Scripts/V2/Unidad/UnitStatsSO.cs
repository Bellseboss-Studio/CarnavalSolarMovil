using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatsSO", menuName = "Unidades/UnitStatsSO", order = 0)]
public class UnitStatsSO : ScriptableObject
{
    [Header("Stats básicos")]
    public float HP = 100f;
    public float Ataque = 10f;
    public float DistanciaDeAtaque = 2f;
    public float CoolDownAttack = 1f;
}