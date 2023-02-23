using System.Text.Json;

namespace Stack_Combat_Game
{
    public class GameClass
    {
        public int Price { get; private set; }
        private int MaxPrice { get; set; }
        public string? TeamName { get; private set; }

        readonly static UnitClass Infantry = new("Infantry", 1, 1, 2, 1);
        readonly static UnitClass Knight = new("Knight", 2, 2, 2, 2);
        readonly static UnitClass Cavalry = new("Cavalry", 3, 3, 10, 3);

        private List<GameClass> _units;
        public IList<GameClass> Units
        {
            get
            {
                if (_units is not null)
                    return _units.AsReadOnly();
                return _units;
            }
        }

        public GameClass(int maxPrice, string teamName)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<GameClass>();
        }

        public string Serializing()
        {
            return JsonSerializer.Serialize<UnitClass>((UnitClass)_units[0]);
        }

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
                return unit.MaxHP;
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
                return unit.Name;
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
                if (Price + unit.Defense + unit.Attack + unit.MaxHP <= MaxPrice)
                {
                    Price += Price + unit.Defense + unit.Attack + unit.MaxHP;
                    _units.Add((UnitClass)unit.Clone());
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

        private sealed class UnitClass : GameClass, ICloneable
        {
            public int Id { get; }
            public string Name { get; }
            public int Attack { get; }
            public int Defense { get; }
            public int MaxHP { get; }

            public int CurrentHP { get; set; }

            public UnitClass(string name, int attack, int defense, int hp, int id)
            {
                Name = name;
                Attack = attack;
                Defense = defense;
                MaxHP = hp;
                CurrentHP = MaxHP;
                Id = id;
            }

            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }
    }
}