using UnityEngine;

namespace SpellCaller
{
    /// <summary>
    /// Lida com comportamentos relacionados a aplicar dano (sendo acionado geralmente por colisões de ataques) 
    /// </summary> <summary>
    public interface IDamageable
    {
        void ApplyDamage(int damageValue, Vector3 positionValue);
        void ApplyContinuosDamage(int damageValue, float intervalValue, Vector3 positionValue);
        void ForceDeath();
        void CheckDeath();
    }
}
