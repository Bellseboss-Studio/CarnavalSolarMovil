using UnityEngine;

public class ConfiguracionDelPeronsaje : MonoBehaviour
{
    [SerializeField] private AnimationClip ataqueNormal, ataqueEspecial;

    public AnimationClip AtaqueNormal => ataqueNormal;
    public AnimationClip AtaqueEspecial => ataqueEspecial;
}
