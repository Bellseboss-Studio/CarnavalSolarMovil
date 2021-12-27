namespace Gameplay.Personajes.InteraccionComponents
{
    public class ObtenerReduccionDeDanioAlIniciarTurno : InteraccionBehaviour
    {
        public override void InteraccionAlIniciarTurno (Personaje origen)
        {
            _interaccionComponent.AumentarArmaduraPorSegundos(origen, 80, 5);
        }
    }
}