namespace Gameplay.Personajes.Scripts
{
    public class Llorona : Personaje
    {
        public void Muerte()
        {
            GetTargetComponent().DejarDeSerTargeteado(this);
            GetTargetComponent().HeDejadoDeTargetear();

            isTargeteable = false;
        }
    }
}