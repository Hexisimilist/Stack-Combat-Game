using System.Text.Json.Serialization;

namespace Stack_Combat_Game
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
        public void UseAbility(ArmyClass friendly, ArmyClass enemies)
        {
            Random rand = new();
            int range = Range;
            if (Range > enemies.UnitDescriptions.Count)
                range = enemies.UnitDescriptions.Count;
            for (int i = 1; i < range; i++)
            {
                if (rand.Next(2) == 1)
                    enemies.EditUnit(i).CurrentHP -= Strength;
            }
        }
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

    public class Warlock : UnitClass, ISpecialAbility
    {
        public Warlock(UnitClass unit, int range, int strength) : base(
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

        public void UseAbility(ArmyClass friendly, ArmyClass enemies)
        {
            Random rand = new();
            int range = Range;
            if (Range > friendly.UnitDescriptions.Count)
                range = friendly.UnitDescriptions.Count;
            for (int i = 1; i < range; i++)
            {
                if (rand.Next(2) == 1)
                    if (friendly.UnitDescriptions[i].CurrentHP > 0)
                    {
                        friendly.MaxPrice += friendly.UnitDescriptions[i].Price;
                        friendly.AddUnit((UnitClass)friendly.UnitDescriptions[i].Clone());
                    }
            }
        }
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

        public void UseAbility(ArmyClass friendly, ArmyClass enemies)
        {
            Random rand = new();
            int range = Range;
            if (Range > friendly.UnitDescriptions.Count)
                range = friendly.UnitDescriptions.Count;
            for (int i = 1; i < range; i++)
            {
                if (rand.Next(2) == 1)
                    if (friendly.UnitDescriptions[i].CurrentHP < friendly.UnitDescriptions[i].HitPoints)
                        friendly.EditUnit(i).CurrentHP += Strength;
            }
        }
    }
}
