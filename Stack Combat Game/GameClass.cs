using Stack_Combat_Game_Unit;
using System.Text.Json.Serialization;

namespace Stack_Combat_Game
{
    public class GameClass
    {
        static readonly object Instancelock = new();
        [JsonIgnore]
        public int Price { get; private set; }
        [JsonIgnore]
        private int MaxPrice { get; set; }
        public string? TeamName { get; private set; }

        readonly UnitClass Infantry;
        readonly UnitClass HeavyInfantry;
        readonly UnitClass Knight;
        readonly UnitClass Archer;


        public UnitClass[] UnitDescriptions { get; set; }

        [JsonPropertyName("Units")]
        public int[] UnitsOrder { get; private set; }

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

        private static GameClass instance;

        public static GameClass GetInstance()
        {
            if (instance == null)
                throw new Exception("GameClass object has not been initialized!");
            return instance;
        }

        public static GameClass GetInstance(int maxPrice, string teamNam, params UnitClass[] units)
        {
            if (instance == null)
            {
                lock (Instancelock)
                {
                    instance ??= new GameClass(maxPrice, teamNam, units);
                }
            }
            return instance;
        }

        public static GameClass GetInstance(int maxPrice, string teamName, int infantryAttack, int infantryDefense, int infantryHP,
            int heavyInfatryAttack, int heavyInfantryDefense, int heavyInfantryHP,
            int knightAttack, int knightDefense, int knightHP)
        {
            if (instance == null)
            {
                lock (Instancelock)
                {
                    instance ??= new GameClass(maxPrice, teamName, infantryAttack, infantryDefense, infantryHP,
                        heavyInfatryAttack, heavyInfantryDefense, heavyInfantryHP,
                        knightAttack, knightDefense, knightHP);
                }
            }
            return instance;
        }

        private GameClass(int maxPrice, string teamName, params UnitClass[] units)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<UnitClass>();
            UnitDescriptions = new UnitClass[units.Length];
            Array.Copy(units, UnitDescriptions, units.Length);
            UnitsOrder = Array.Empty<int>();

            //TODO Probably to delete the second argument in AddUnit() or to add this variable in parameter of GameClass ctor
            foreach (var item in units)
            {
                AddUnit(item, 1);
            }

        }

        private GameClass(int maxPrice, string teamName, int infantryAttack, int infantryDefense, int infantryHP,
            int heavyInfatryAttack, int heavyInfantryDefense, int heavyInfantryHP,
            int knightAttack, int knightDefense, int knightHP)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<UnitClass>();
            UnitDescriptions = new UnitClass[] { Infantry = new(1, "Infantry", infantryAttack, infantryDefense, infantryHP),
                HeavyInfantry = new(2, "HeavyInfantry", heavyInfatryAttack, heavyInfantryDefense, heavyInfantryHP),
                Knight = new(3, "Knight", knightAttack, knightDefense, knightHP)};
            UnitsOrder = Array.Empty<int>();
        }

        public void ClearDeadUnits()
        {
            int deadUnits = 0;
            for (int i = 0; i < _units.Count; i++)
            {
                if (GetUnitCurrentHealth(i) <= 0)
                {
                    _units.RemoveAt(i);
                    UnitsOrder[i] = 0;
                    deadUnits++;
                }
            }
            int[] temp = new int[UnitsOrder.Length - deadUnits];
            int n = 0;
            for (int j = 0; j < UnitsOrder.Length; j++)
            {
                while (UnitsOrder[n] == 0)
                {
                    n++;
                }
                temp[j] = UnitsOrder[n];
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
        public void AddArcher(int count)
        {
            AddUnit(Archer, count);
        }
        private void AddUnit(UnitClass unit, int count)
        {
            
                for (int i = 0; i < count; i++)
                {

                    if (unit is ISpecialAbility)
                    {
                        var specialUnit = unit as ISpecialAbility;
                        Price += Price + unit.Attack + unit.Defense + unit.HitPoints
                            + (specialUnit.Range + specialUnit.Strength) * 2;
                    }

                    else
                    {
                        Price += Price + unit.Defense + unit.Attack + unit.HitPoints;
                    }

                    if (Price <= MaxPrice)
                    {
                        _units.Add((UnitClass)unit.Clone());
                        int[] units = UnitsOrder;
                        Array.Resize(ref units, UnitsOrder.Length + 1);
                        UnitsOrder = units;
                        UnitsOrder[^1] = unit.UnidDescriptionId;
                        continue;
                    }
                    throw new Exception("Army Cost doesn't relate to the requirements");
                }
        }

        public void GetAttacked(int unitNum, int damage)
        {
            if (unitNum >= 0 && unitNum < _units.Count)
                _units[unitNum].CurrentHP -= damage + _units[unitNum].Defense;
        }
    }
}