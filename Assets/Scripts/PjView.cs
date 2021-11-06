using ServiceLocatorPath;
using UnityEngine;

public class PjView : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject personaje;

    public void Configurate(Personaje personajesJugablesElegido)
    {
        var pj = Instantiate(personaje, transform);
    }
}