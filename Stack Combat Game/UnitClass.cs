using System.Text.Json;
using System.Text.Json.Serialization;

namespace Stack_Combat_Game
{
    public class GameClass
    {
        [JsonIgnore]
        public int Price { get; private set; }
        [JsonIgnore]
        private int MaxPrice { get; set; }
        public string? TeamName { get; private set; }

        readonly static UnitClass Infantry = new("Infantry", 1, 1, 2, 1);
        readonly static UnitClass Knight = new("Heavy Infantry", 2, 2, 2, 2);
        readonly static UnitClass Cavalry = new("Knight", 3, 3, 10, 3);

        public UnitClass[] UnitDescriptions { get; set; }
        public int[] Units { get; private set; }

        [JsonIgnore]
        private List<UnitClass> _units;
        [JsonIgnore]
        public IList<UnitClass> UnitsList
        {
            get
            {
                return _units.AsReadOnly();
            }
        }

        public GameClass(int maxPrice, string teamName)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<UnitClass>();
            UnitDescriptions = new UnitClass[] { Infantry, Knight, Cavalry };
            Units = new int[0];
        }

        /*public void Serializing()
        {
            string fileName = "Units.json";
            List<string> jsonstring = new();
            var options = new JsonSerializerOptions { WriteIndented = true };
            jsonstring.Add(JsonSerializer.Serialize(UnitDescriptions, options));
            File.WriteAllLines(fileName, jsonstring);
        }*/

        GameClass()
        {
        }

        public void ClearDeadUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (GetUnitCurrentHealth(i) <= 0)
                    _units.RemoveAt(i);
            }
        }

        public int GetUnitCurrentHealth(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                return unit.CurrentHP;
            }
            else
                return -1;
        }

        public int GetUnitMaxHealth(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                return unit.HitPoints;
            }
            else
                return -1;
        }

        public int GetUnitDefense(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                return unit.Defense;
            }
            else
                return -1;
        }

        public int GetUnitAttack(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                return unit.Attack;
            }
            else
                return -1;
        }

        public string GetUnitName(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                return unit.UnitName;
            }
            else
                return "Unknown";
        }

        public void AddInfantry(int count)
        {
            AddUnit(Infantry, count);
        }
        public void AddKnight(int count)
        {
            AddUnit(Knight, count);
        }
        public void AddCavalry(int count)
        {
            AddUnit(Cavalry, count);
        }
        private void AddUnit(UnitClass unit, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (Price + unit.Defense + unit.Attack + unit.HitPoints <= MaxPrice)
                {
                    Price += Price + unit.Defense + unit.Attack + unit.HitPoints;
                    _units.Add((UnitClass)unit.Clone());
                    int[] units = Units;
                    Array.Resize(ref units, Units.Length + 1);
                    Units = units;
                    Units[^1] = unit.UnidDescriptionId;
                    continue;
                }
                break;
            }
        }

        public void GetAttacked(int unitNum, int damage)
        {
            if (unitNum < _units.Count)
            {
                UnitClass unit = (UnitClass)_units[unitNum];
                unit.CurrentHP -= damage + unit.Defense;
                _units[unitNum] = unit;
            }
        }

        public sealed class UnitClass : ICloneable
        {
            public int UnidDescriptionId { get; }
            public string UnitName { get; }
            public int Attack { get; }
            public int Defense { get; }
            public int HitPoints { get; }

            [JsonIgnore]
            public int CurrentHP { get; set; }

            public UnitClass(string name, int attack, int defense, int hp, int id)
            {
                UnitName = name;
                Attack = attack;
                Defense = defense;
                HitPoints = hp;
                CurrentHP = HitPoints;
                UnidDescriptionId = id;
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
    }
}