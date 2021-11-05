using UnityEditor.Animations;
using UnityEngine;

public class PjView : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip ataqueNormal, ataqueEspecial;

    public void Animacion1()
    {
        anim.Play(ataqueNormal.name);
    }

    public void Animacion2()
    {
        anim.runtimeAnimatorController.animationClips[0] = ataqueEspecial;
        anim.Play(ataqueEspecial.name);
    }
    public void Configurate()
    {
        
    }
}