using Stack_Combat_Game;
using System.Text.Json;

UnitClass[] units = new[]
{
    new UnitClass(1, "Knight", 7,0,30),
    new Healer(new(2,"Healer",2,0,15), 1, 10),
    new Healer(new(3,"Healer",1,0,2), 1, 5),
    new Healer(new(4,"Healer",0,0,1), 1, 3)
};


ArmyClass game = new();
game.MaxPrice = 10000000;
game.TeamName = "ass";

for (int i = 0; i < units.Length; i++)
{
    game.AddUnit(units[i]);
}

//game.AddUnit(units);
//Console.WriteLine(game.TeamName);
//game.TeamName = "InteCore2Duo";
//ArmyClass gameNew = ArmyClass.GetInstance();

//ame.MaxPrice = 1000;

//Console.WriteLine($"{game.TeamName}, {gameNew.TeamName}");



//game.AddUnit(new Knight(1, "Knight", 7, 0, 30));
var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(game, options));

//string fileName = "InteCore2Duo.json";
//string jsonString = JsonSerializer.Serialize(game, options);
//File.WriteAllText(fileName, jsonString);

