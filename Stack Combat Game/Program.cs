using Stack_Combat_Game;
using System.Text.Json;

GameClass game = new(100, "Ludens");

game.AddKnight(2);
var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(game, options));

//Console.WriteLine(game.Serializing());