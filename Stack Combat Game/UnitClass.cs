using Stack_Combat_Game;
using System.Text.Json.Serialization;

namespace Stack_Combat_Game_Unit
{
    [JsonConverter(typeof(UnitClassJsonConverter))]
    public class UnitClass : ICloneable
    {
        public int UnitDescriptionId { get; protected set; }
        public string UnitName { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int HitPoints { get; protected set; }
        public int Price { get; protected set; }

        [JsonIgnore]
        public int CurrentHP { get; set; }

        public UnitClass(int id, string name, int attack, int defense, int hp)
        {
            UnitDescriptionId = id;
            UnitName = name;
            Attack = attack;
            Defense = defense;
            HitPoints = hp;
            CurrentHP = HitPoints;
            Price = defense + attack + hp;
        }

        public void ReceiveDamage(int damage)
        {
            this.CurrentHP -= damage + this.Defense;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }

    public class Archer : UnitClass, ISpecialAbility
    {
        public Archer(UnitClass unit, int range, int strength) : base(
            unit.UnitDescriptionId,
            unit.UnitName,
            unit.Attack,
            unit.Defense,
            unit.HitPoints
            )
        {
            Range = range;
            Strength = strength;
            AbilityType = 1;
            Price = Attack + Defense + HitPoints + (Range + Strength) * 2;
        }

        public int Range { get; private set; }
        public int Strength { get; private set; }
        public int AbilityType { get; private set; }
    }

    public class Knight : UnitClass
    {
        public Knight(int id, string name, int attack, int defense, int hp) : base(id, name, attack, defense, hp)
        {

        }
    }

    public class Infantry : UnitClass
    {
        public Infantry(int id, string name, int attack, int defense, int hp) : base(id, name, attack, defense, hp)
        {

        }
    }

    public class HeavyInfantry : UnitClass
    {
        #region Bad DRY Example
        /*public HeavyInfantry(int id, string name, int attack, int defense, int hp)
        {
            UnitDescriptionId = id;
            UnitName = name;
            Attack = attack;
            Defense = defense;
            HitPoints = hp;
            CurrentHP = HitPoints;
        }*/
        #endregion

        #region Correct DRY Example
        public HeavyInfantry(int id, string name, int attack, int defense, int hp) : base(id, name, attack, defense, hp)
        {

        }

        #endregion
    }

    public class Healer : UnitClass, ISpecialAbility
    {
        public Healer(UnitClass unit, int range, int strength) : base(
            unit.UnitDescriptionId,
            unit.UnitName,
            unit.Attack,
            unit.Defense,
            unit.HitPoints
            )
        {

            Range = range;
            Strength = strength;
            AbilityType = 2;
            Price = Attack + Defense + HitPoints + (Range + Strength) * 2;
        }

        public int Range { get; private set; }
        public int Strength { get; private set; }
        public int AbilityType { get; private set; }
    }
}
