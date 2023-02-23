using Stack_Combat_Game;
using System.Text.Json;

GameClass game = new(100, "Ludens");

game.AddCavalry(2);


Console.WriteLine(game.Serializing());