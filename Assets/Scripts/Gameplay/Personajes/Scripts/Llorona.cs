namespace Gameplay.Personajes.Scripts
{
    public class Llorona : Personaje
    {
        public override void Muerte()
        {
            GetTargetComponent().DejarDeSerTargeteado(this);
            GetTargetComponent().HeDejadoDeTargetear(this);

            isTargeteable = false;
        }
    }
}