namespace Gameplay.Personajes.Scripts
{
    public class Llorona : Personaje
    {
        public void AAMuerte()
        {
            GetTargetComponent().DejarDeSerTargeteado(this);
            GetTargetComponent().HeDejadoDeTargetear();

            isTargeteable = false;
        }
    }
}