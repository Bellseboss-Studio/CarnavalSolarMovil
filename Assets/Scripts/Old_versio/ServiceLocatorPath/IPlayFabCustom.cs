using System.Collections.Generic;

namespace ServiceLocatorPath
{
    public interface IPlayFabCustom
    {
        List<Personaje> GetPjs();
        bool IsAllCompleted();
    }
}