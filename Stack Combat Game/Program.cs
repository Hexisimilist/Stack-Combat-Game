using Stack_Combat_Game;
using System.Text.Json;

GameClass game = new(100, "Ludens");

game.AddCavalry(2);
var options = new JsonSerializerOptions { WriteIndented = true };
Console.WriteLine(JsonSerializer.Serialize(game, options));

game.Serializing();
//Console.WriteLine(game.Serializing());