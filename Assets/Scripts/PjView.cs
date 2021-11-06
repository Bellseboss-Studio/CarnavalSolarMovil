using ServiceLocatorPath;
using UnityEngine;

public class PjView : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject personaje;
    private Personaje _personaje;

    public void Configurate(Personaje personajesJugablesElegido)
    {
        var pj = Instantiate(personaje, transform);
        _personaje = personajesJugablesElegido;
    }

    public Personaje PJ => _personaje;
}