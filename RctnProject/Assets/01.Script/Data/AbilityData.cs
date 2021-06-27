
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Data", menuName = "Scriptable Object/Ability Data", order = int.MaxValue)]
public class AbilityData : ScriptableObject
{
    [SerializeField]
    private float health;
    public float Health { get { return health; } }

    [SerializeField]
    private float attackRange;
    public float AttackRange { get { return attackRange; } }

    [SerializeField]
    private float attackDamage;
    public float AttackDamage { get { return attackDamage; } }

}
