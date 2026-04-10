using System.Collections.Generic;
using DG.Tweening;
using Gameplay.NewGameStates;
using ServiceLocatorPath;
using UnityEngine;

public class ColocacionCartasOffline : MonoBehaviour, IColocacionCartas
{
    [SerializeField] private List<GameObject> posicionesDeCartas;
    [SerializeField] private GameObject panelQueContieneCartas;
    private List<bool> _hayCartaEnPosicion;
    private Stack<string> _barajaDeCartasId;
    private List<string> _idCartasEnMano;
    private Dictionary<int, string> _cartasEnMano;
    private int _posicionDeUltimaCartaIntanciada;
    private string _ultimaCartaInstanciada;

    public void Configurate()
    {
        _hayCartaEnPosicion = new List<bool>(){false,false,false,false,false,false,false,false,false,false};
        _barajaDeCartasId = new Stack<string>();
        _idCartasEnMano = new List<string>();
        _barajaDeCartasId = ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().GetPrimeras10Cartas();
        _cartasEnMano = new Dictionary<int, string>();
    }
        
    public List<GameObject> GetPosicionesDeCartas()
    {
        return posicionesDeCartas;
    }

    public int GetNumeroDePosiciones()
    {
        return posicionesDeCartas.Count;
    }

    public int GetPosicionDeUltimaCartaInstanciada()
    {
        return _posicionDeUltimaCartaIntanciada;
    }

    public bool PuedoSacarOtraCarta()
    {
        return _cartasEnMano.Count < 5 && _barajaDeCartasId.Count > 0;
    }

    public GameObject GetSiguientePosicionDeCarta()
    {
        for (int i = 0; i < _hayCartaEnPosicion.Count; i++)
        {
            if (!_hayCartaEnPosicion[i])
            {
                _posicionDeUltimaCartaIntanciada = i;
                return posicionesDeCartas[i];
            }
        }
        return null;
    }

        
        
    public string GetNextCartaId()
    {
        if (_barajaDeCartasId.Count <= 9)
        {
            _barajaDeCartasId = ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().GetPrimeras10Cartas();
        }
        var carta = _barajaDeCartasId.Pop();
        _ultimaCartaInstanciada = carta;
        //_posicionDeUltimaCartaIntanciada = _cartasEnMano.Count;
        //_cartasEnMano.Add(_cartasEnMano.Count, carta);
        return carta;
    }

    public void HayCartaEnPosicionInstanciada()
    {
        _hayCartaEnPosicion[_posicionDeUltimaCartaIntanciada] = true;
        _cartasEnMano.Add(_posicionDeUltimaCartaIntanciada, _ultimaCartaInstanciada);
    }
    public void HayCartaEnPosicion(int posicionSinCarta)
    {
        _hayCartaEnPosicion[posicionSinCarta] = true;
        if (!_cartasEnMano.ContainsKey(posicionSinCarta))
        {
            _cartasEnMano.Add(posicionSinCarta, _ultimaCartaInstanciada);
        }
        else
        {
            _cartasEnMano[posicionSinCarta] = _ultimaCartaInstanciada;
        }

        _posicionDeUltimaCartaIntanciada = posicionSinCarta;
    }

    public string GetHeroe()
    {
        return ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().GetHeroe();
    }

    public string GetHeroeEnemigo()
    {
        return ServiceLocator.Instance.GetService<IServicioDeBarajasDisponibles>().GetHeroeEnemigo();
    }

    public void YaNoHayCartaEnPosicion(int pos)
    {
        _hayCartaEnPosicion[pos] = false;
    }

    public List<int> ObtenerPocisionesSinCartas()
    {
        var listaDePocisionesSinCartas = new List<int>();
        for (var i = 0;i<_hayCartaEnPosicion.Count; i++)
        {
            if(_hayCartaEnPosicion[i] == false) listaDePocisionesSinCartas.Add(i);
        }
        return listaDePocisionesSinCartas;
    }

    public GameObject GetPosicionDeCarta(int posicionSinCarta)
    {
        return posicionesDeCartas[posicionSinCarta];
    }

    public void OcultarCartas()
    {

        var sequence = DOTween.Sequence();
        var tra = panelQueContieneCartas.GetComponent<RectTransform>();
        sequence.Insert(0, tra.DOLocalMoveY(tra.localPosition.y - 1000, .4f));
    }

    public void MostrarCartas()
    {
        var sequence = DOTween.Sequence();
        var tra = panelQueContieneCartas.GetComponent<RectTransform>();
        sequence.Insert(0, tra.DOLocalMoveY(tra.localPosition.y + 1000, .4f));
    }
}