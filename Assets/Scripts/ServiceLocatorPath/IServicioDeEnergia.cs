namespace ServiceLocatorPath
{
    public interface IServicioDeEnergia
    {
        void Init();
        bool TieneEnergiaSuficiente(int costoDeEnergia);
        void AddEnergy();
    }
}