using Stack_Combat_Game;
using System.Text.Json.Serialization;

namespace Stack_Combat_Game_Unit
{
    public class UnitClass : ICloneable
    {
        public int UnidDescriptionId { get; }
        public string UnitName { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int HitPoints { get; }

        [JsonIgnore]
        public int CurrentHP { get; set; }

        public UnitClass(int id, string name, int attack, int defense, int hp)
        {
            UnidDescriptionId = id;
            UnitName = name;
            Attack = attack;
            Defense = defense;
            HitPoints = hp;
            CurrentHP = HitPoints;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Archer : UnitClass, ISpecialAbility
    {
        public Archer(UnitClass unit, int range, int strength) : base(
            unit.UnidDescriptionId,
            unit.UnitName,
            unit.Attack,
            unit.Defense,
            unit.HitPoints
            )
        {

            Range = range;
            Strength = strength;
            AbilityType = 1;
        }

        public int Range { get ; private set; }
        public int Strength { get; private set; }
        public int AbilityType { get; private set; }
    }




}
