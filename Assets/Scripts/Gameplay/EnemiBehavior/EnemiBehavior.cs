using System.Collections;
using System.Collections.Generic;
using Gameplay.NewGameStates;
using Gameplay.UsoDeCartas;
using ServiceLocatorPath;
using UnityEngine;

public class EnemiBehavior : MonoBehaviour, IEnemyBehavior
{
    [SerializeField] private FactoriaPersonaje _factory;

    public void InstantiateEnemies()
    {
        try
        {
            while (true)
            {
                ServiceLocator.Instance.GetService<IEnemyInstantiate>().InstanciateEnemy(_factory);
            }
        }
        catch (EnergiaInsuficienteException e)
        {
            Debug.Log(e.Message);
        }
    }
}
