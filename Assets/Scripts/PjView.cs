using System.Collections;
using ServiceLocatorPath;
using UnityEngine;

public class PjView : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip danioAnim, idle, muerte;
    [SerializeField] private GameObject personaje;
    private Personaje _personaje;
    private ConfiguracionDelPeronsaje _configuracionDelPeronsaje;

    public void Configurate(Personaje personajesJugablesElegido, ControladorDeBatallaParaPersonajes controladorDeBatallaParaPersonajes)
    {
        var pj = Instantiate(personaje, transform);
        anim = pj.GetComponent<Animator>();
        _configuracionDelPeronsaje = pj.GetComponent<ConfiguracionDelPeronsaje>();
        if (controladorDeBatallaParaPersonajes.DebeConfigurar)
        {
            controladorDeBatallaParaPersonajes.AtaqueNormal.OnDropInTarget += OnDropInTargetInAtacaqueNormal;
            controladorDeBatallaParaPersonajes.AtaqueEspecial.OnDropInTarget += OnDropInTargetAtaqueEspecial;
        }
        _personaje = personajesJugablesElegido;
    }

    private void OnDropInTargetAtaqueEspecial(PjView target)
    {
        //Animar el propio personaje
        anim.Play(_configuracionDelPeronsaje.AtaqueEspecial.name);
        Debug.Log($"El ataque especial para {target.PJ.nombre}");
        StartCoroutine(LuegoDeLaAnimacionRecibir(target, _configuracionDelPeronsaje.AtaqueEspecial));
    }

    private void OnDropInTargetInAtacaqueNormal(PjView target)
    {
        anim.Play(_configuracionDelPeronsaje.AtaqueNormal.name);
        Debug.Log($"El ataque normal para {target.PJ.nombre}");
        StartCoroutine(LuegoDeLaAnimacionRecibir(target, _configuracionDelPeronsaje.AtaqueNormal));
    }

    IEnumerator LuegoDeLaAnimacionRecibir(PjView target, AnimationClip waitTime)
    {
        yield return new WaitForSeconds(waitTime.length);
        anim.Play(idle.name);
        target.AplicaDanoDe(_personaje.ataque);
    }

    private void AplicaDanoDe(float danio)
    {
        anim.Play(danioAnim.name);
        //probablemente esto defina cuando se muestra la pantalla de ganaste o perdiste
        Debug.Log($"Vida antes {_personaje.vida}");
        _personaje.vida -= danio;
        Debug.Log($"Vida Despues {_personaje.vida}");
        StartCoroutine(ComeBackIdle(danioAnim));
    }

    IEnumerator ComeBackIdle(AnimationClip concurrentAnimation)
    {
        yield return new WaitForSeconds(concurrentAnimation.length);
        anim.Play(_personaje.vida <= 0 ? muerte.name : idle.name);
    }

    public Personaje PJ => _personaje;

    public bool EstaVivo()
    {
        return _personaje.vida > 0;
    }

    public void DesConfigure(ControladorDeBatallaParaPersonajes controladorDeBatallaParaPersonajes)
    {
        controladorDeBatallaParaPersonajes.AtaqueNormal.OnDropInTarget -= OnDropInTargetInAtacaqueNormal;
        controladorDeBatallaParaPersonajes.AtaqueEspecial.OnDropInTarget -= OnDropInTargetAtaqueEspecial;
    }
    
}