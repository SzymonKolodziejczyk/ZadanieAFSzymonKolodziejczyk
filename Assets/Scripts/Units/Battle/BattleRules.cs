namespace AFSInterview.Battle
{
    public static class BattleRules
    {
        public static int GetResultDamage(UnitData attacker, UnitData target)
        {
            int damage = attacker.unitStats.attackDamage;
            for (int i = 0; i < attacker.unitStats.supereffectiveDamage.Count; i++)
            {
                SupereffectiveDamage SupereffectiveDamage = attacker.unitStats.supereffectiveDamage[i];
                if (target.HasAttribute(SupereffectiveDamage.attributeType))
                {
                    damage += SupereffectiveDamage.attackDamage;
                }
            }

            damage -= target.unitStats.armorPoints;
            damage = System.Math.Max(damage, 1);

            return damage;
        }
    }
}