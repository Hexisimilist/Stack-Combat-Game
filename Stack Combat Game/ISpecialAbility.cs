using System.Text.Json.Serialization;

namespace Stack_Combat_Game
{
    internal interface ISpecialAbility
    {
        [JsonPropertyName("SpecialAbilityRange")]
        [JsonPropertyOrder(2)]
        public int Range { get; }

        [JsonPropertyName("SpecialAbilityStrength")]
        [JsonPropertyOrder(1)]

        public int Strength { get; }

        [JsonPropertyName("SpecialAbilityType")]
        [JsonPropertyOrder(0)]
        public int AbilityType { get; }

        public void UseAbility(ArmyClass friendly, ArmyClass enemies);
    }
}
