namespace ServiceLocatorPath
{
    public interface IServicioDeEnergia
    {
        void Init();
        bool TieneEnergiaSuficiente(int costoDeEnergia);
        void AddEnergy();
        void AddQuantityOfEnergyInTheNextTurn(int i);
        bool TieneEnergiaSuficienteP2(int costoDeEnergia);
        void AddEnergyP2();
    }
}