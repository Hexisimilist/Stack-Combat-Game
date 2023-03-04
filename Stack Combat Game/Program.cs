using Stack_Combat_Game;
using Stack_Combat_Game_Unit;
using System.Text.Json;

UnitClass[] units = new[]
{
    new UnitClass(1, "Knight", 7,0,30),
    new Healer(new(2,"Healer",2,0,15), 1, 10),
    new Healer(new(3,"Healer",1,0,2), 1, 5),
    new Healer(new(4,"Healer",0,0,1), 1, 3)
};

GameClass game = GameClass.GetInstance(100, "InteCore2Duo", units);


var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(game, options));

//string fileName = "InteCore2Duo.json";
//string jsonString = JsonSerializer.Serialize(game, options);
//File.WriteAllText(fileName, jsonString);

