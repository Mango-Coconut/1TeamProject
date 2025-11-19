using System;
public interface IDamageable
{
    float CurHP { get; }
    float MaxHP { get; }
    bool IsDead { get; }

    event Action<float, float> OnHPChanged;
    event Action OnDied;

    void TakeDamage(float amount);
    void Heal(float amount);  // 필요 없다 싶으면 나중에 빼도 됨
}
