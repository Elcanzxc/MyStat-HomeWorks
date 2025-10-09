
using Microsoft.EntityFrameworkCore;
using ServerApp.Server;
using ServerApp.Sql;


var options = new DbContextOptionsBuilder<ChatDbContext>()
    .UseSqlServer("Server=localhost;Database=ChatServerDB;Integrated Security=True;TrustServerCertificate=True;")
    .Options;
var db = new ChatDbContext(options);
var server = new Server("127.0.0.1", 7777, db);
await server.StartAsync();