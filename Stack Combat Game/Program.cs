using Stack_Combat_Game;
using Stack_Combat_Game_Unit;
using System.Text.Json;

UnitClass[] units = new[]
{
    new UnitClass(1, "Knight", 10,5,5),
    new Healer(new(2,"Healer",20,0,3), 2, 3),
    new Archer(new(3,"Archer",20,0,3), 2, 2)
};

GameClass game = GameClass.GetInstance(300, "Lohi", units);


var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(game, options));

