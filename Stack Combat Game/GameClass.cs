using Stack_Combat_Game_Unit;
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

        readonly UnitClass Infantry;
        readonly UnitClass HeavyInfantry;
        readonly UnitClass Knight;

        public UnitClass[] UnitDescriptions { get; set; }
        public int[] Units { get; private set; }

        [JsonIgnore]
        private List<UnitClass> _units;
        [JsonIgnore]
        public IList<UnitClass> UnitsListReadOnly
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
            UnitDescriptions = new UnitClass[] { Infantry = new(1, "Infantry", 1, 1, 2),
                HeavyInfantry = new(2, "HeavyInfantry", 2, 2, 2),
                Knight = new(3, "Knight", 3, 3, 10) };
            Units = Array.Empty<int>();
        }

        public GameClass(int maxPrice, string teamName, int infantryAttack, int infantryDefense, int infantryHP,
            int heavyInfatryAttack, int heavyInfantryDefense, int heavyInfantryHP,
            int knightAttack, int knightDefense, int knightHP)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<UnitClass>();
            UnitDescriptions = new UnitClass[] { Infantry = new(1, "Infantry", infantryAttack, infantryDefense, infantryHP),
                HeavyInfantry = new(2, "HeavyInfantry", heavyInfatryAttack, heavyInfantryDefense, heavyInfantryHP),
                Knight = new(3, "Knight", knightAttack, knightDefense, knightHP)};
            Units = Array.Empty<int>();
        }

        public void ClearDeadUnits()
        {
            int deadUnits = 0;
            for (int i = 0; i < _units.Count; i++)
            {
                if (GetUnitCurrentHealth(i) <= 0)
                {
                    _units.RemoveAt(i);
                    Units[i] = 0;
                    deadUnits++;
                }
            }
            int[] temp = new int[Units.Length - deadUnits];
            int n = 0;
            for (int j = 0; j < Units.Length; j++)
            {
                while (Units[n] == 0)
                {
                    n++;
                }
                temp[j] = Units[n];
            }
        }

        public int GetUnitCurrentHealth(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                return _units[unitNum].CurrentHP;
            return -1;
        }

        public int GetUnitMaxHealth(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                return _units[unitNum].HitPoints;
            return -1;
        }

        public int GetUnitDefense(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                return _units[unitNum].Defense;
            return -1;
        }

        public int GetUnitAttack(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                return _units[unitNum].Attack;
            return -1;
        }

        public string GetUnitName(int unitNum)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                return _units[unitNum].UnitName;
            return "Unknown";
        }

        public void AddInfantry(int count)
        {
            AddUnit(Infantry, count);
        }
        public void AddHeavyInfantry(int count)
        {
            AddUnit(HeavyInfantry, count);
        }
        public void AddKnight(int count)
        {
            AddUnit(Knight, count);
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
            if (unitNum >= 0 && unitNum < _units.Count)
                _units[unitNum].CurrentHP -= damage + _units[unitNum].Defense;
        }
    }
}