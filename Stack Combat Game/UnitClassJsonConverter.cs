using Stack_Combat_Game_Unit;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Stack_Combat_Game
{
    internal class UnitClassJsonConverter : JsonConverter<UnitClass>
    {

        public override UnitClass? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, UnitClass value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("UnidDescriptionId", value.UnidDescriptionId);
            writer.WriteString("UnitName", value.UnitName);
            writer.WriteNumber("Attack", value.Attack);
            writer.WriteNumber("Defense", value.Defense);
            writer.WriteNumber("HitPoints", value.HitPoints);

            if (value is ISpecialAbility)
            {
                var newValue = value as ISpecialAbility;

                writer.WriteNumber("SpecialAbilityType", newValue.AbilityType);
                writer.WriteNumber("SpecialAbilityStrength", newValue.Strength);
                writer.WriteNumber("SpecialAbilityRange", newValue.Range);

            }


            writer.WriteEndObject();

        }
    }
}
