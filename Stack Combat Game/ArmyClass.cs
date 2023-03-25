
using System.Text.Json.Serialization;

namespace Stack_Combat_Game
{
    public sealed class ArmyClass
    {

        public ArmyClass()
        {

        }

        static readonly object InstanceLock = new();
        [JsonIgnore]
        public int Price { get; private set; }
        [JsonIgnore]
        public int MaxPrice { get; set; }
        public string? TeamName { get; set; }


        private List<UnitClass> _units;
        private int[] _unitsOrder;

        public IList<UnitClass> UnitDescriptions
        {
            get
            {
                if (_units == null)
                    lock (InstanceLock)
                    {
                        _units ??= new List<UnitClass>();
                    }
                return _units.AsReadOnly();
            }
        }

        [JsonPropertyName("Units")]
        public int[] UnitsOrder
        {
            get
            {
                _unitsOrder = new int[UnitDescriptions.Count];
                for (int i = 0; i < _unitsOrder.Length; i++)
                {
                    _unitsOrder[i] = UnitDescriptions[i].UnitDescriptionId;
                }
                return _unitsOrder;
            }
            private set
            {
            }
        }




        public void UpdateUnits(List<UnitClass> units)
        {
            _units = units;
        }


        /*public ArmyClass(int maxPrice, string teamName, params UnitClass[] units)
        {
            TeamName = teamName;
            MaxPrice = maxPrice;
            _units = new List<UnitClass>();
            UnitsOrder = new int[_units.Count];

            foreach (var item in units)
            {
                AddUnit(item);
            }
            Array.Sort(UnitsOrder);
        }*/


        public void ClearDeadUnits()
        {
            if (_units != null)
                if (_units.Count > 0)
                    for (int i = 0; i < _units.Count; i++)
                    {
                        if (_units[i].CurrentHP <= 0)
                        {
                            _units.RemoveAt(i);
                            UnitsOrder = UnitsOrder.RemoveAt(i);
                        }
                    }
        }

        public UnitClass EditUnit(int number)
        {
            if (number >= UnitDescriptions.Count || number < 0)
                throw new ArgumentOutOfRangeException("Index", "Index is out of range!");
            return _units[number];
        }

        public void AddUnit(UnitClass unit)
        {
            if (_units == null)
                lock (InstanceLock)
                {
                    _units ??= new List<UnitClass>();
                }

            Price += unit.Price;
            if (Price <= MaxPrice)
            {
                _units.Add(unit);
            }
            else
                throw new Exception("Army Cost doesn't relate to the requirements");
        }
    }
}