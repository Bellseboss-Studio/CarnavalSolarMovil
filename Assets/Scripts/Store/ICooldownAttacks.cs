namespace Store
{
    public interface ICooldownAttacks
    {
        void ConfigureSliderValues(float cooldown, IMediatorCooldown mediatorCooldown);
        void CooldownForNormalAttack();
        void CooldownForSpecialAttack();
    }
}