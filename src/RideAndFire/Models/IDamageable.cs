namespace RideAndFire.Models;

public interface IDamageable
{
    float Health { get; }
    float MaxHealth { get; }
    bool IsDead { get; }
    void OnDamage(float damage);
}