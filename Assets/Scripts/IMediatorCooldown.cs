using System.Collections.Generic;
using ServiceLocatorPath;

public interface IMediatorCooldown
{
    void CannotAttackAnymore();
    void ConfiguraCooldownsPorPersonaje(List<Personaje> personajes);
}