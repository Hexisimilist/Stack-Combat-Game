using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Stack_Combat_Game_Unit
{
    public sealed class UnitClass : ICloneable
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
}
