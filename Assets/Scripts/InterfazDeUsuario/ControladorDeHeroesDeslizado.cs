using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorDeHeroesDeslizado : MonoBehaviour
{
    [SerializeField] private List<SelectorBaraja> barajas;
    [SerializeField] private Button derecha, izquierda, continuar, jugar;
    [SerializeField] private Animator animator;
    [SerializeField] private float duracion;
    [SerializeField] private int fuerza, vribacion;
    private int index;
    private SelectorBaraja _barajaSeleccionada;
    private bool estaMostrando;

    private void Awake()
    {
        var servicioDeBarajasDisponibles = new ServicioDeBarajasDisponibles();
        ServiceLocator.Instance.RegisterService<IServicioDeBarajasDisponibles>(servicioDeBarajasDisponibles);
    }

    IEnumerator DiSableAndEnalbe()
    {
        derecha.enabled = false;
        izquierda.enabled = false;
        yield return new WaitForSeconds(.4f);
        derecha.enabled = true;
        izquierda.enabled = true;
    }
    private void ContinuarASiguienteEscena()
    {
        if (!_barajaSeleccionada.PuedeSeleccionarse)
        {
            _barajaSeleccionada.transform.DOShakePosition(duracion, fuerza, vribacion);
            return;
        }
        ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>()
            .SetBarajaSeleccionadaId(_barajaSeleccionada);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Start()
    {
        foreach (var baraja in barajas)
        {
            baraja.BotonBarajaSeleccionada.onClick.AddListener(() =>
            {
                _barajaSeleccionada = baraja;
                foreach (var selectorBaraja in barajas)
                {
                    selectorBaraja.AclararImagen();
                }
                baraja.OscurecerImagen();
                MostrarBotonDeSeleccionar();
            });
        }
        derecha.onClick.AddListener(() =>
        {
            if (index >= 0 && index < barajas.Count -1)
            {
                StartCoroutine(DiSableAndEnalbe());
                foreach (var baraja in barajas)
                {
                    baraja.MueveteALaIzquierda(1000);
                }
                //_barajaSeleccionada = barajas[index];
                index++;
                barajas[index].BotonBarajaSeleccionada.onClick.Invoke();
            }
        });
        izquierda.onClick.AddListener(() =>
        {
            if (index > 0 && index <= barajas.Count)
            {
                StartCoroutine(DiSableAndEnalbe());
                foreach (var baraja in barajas)
                {
                    baraja.MueveteALaDerecha(1000);
                }
                index--;
                barajas[index].BotonBarajaSeleccionada.onClick.Invoke();
            }
        });
        
        continuar.onClick.AddListener(ContinuarASiguienteEscena);
        
        jugar.onClick.AddListener(() =>
        {
            animator.SetBool("jugar", true);
        });
        
        barajas[0].BotonBarajaSeleccionada.onClick.Invoke();
    }

    private void MostrarBotonDeSeleccionar()
    {
        if(estaMostrando) return;
        var sequence = DOTween.Sequence();
        var tra = continuar.gameObject.GetComponent<RectTransform>();
        sequence.Insert(0, tra.DOLocalMoveX(tra.localPosition.x + 1000, .4f));
        estaMostrando = true;
    }
}
