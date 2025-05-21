using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var endPoint = new IPEndPoint(IPAddress.Any, 2008);
server.Bind(endPoint);

server.Listen(10);

Console.WriteLine($"Запуск сервера {endPoint}");

while (true)
{
    Socket client = await server.AcceptAsync();
    _ = Task.Run(() => HandleClientAsync(client));
}

async Task HandleClientAsync(Socket client){
    var buffer = new byte[255];
    int sizeBytes = await client.ReceiveAsync(buffer);

    string text = Encoding.UTF8.GetString(buffer, 0, sizeBytes).Trim();

    string resultText;
    if (text == "date")
        resultText = DateTime.Now.ToString("yyyy-MM-dd");
    else if (text == "time")
        resultText = DateTime.Now.ToString("HH:mm:ss");
    else if (text == "datetime")
        resultText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    else
        resultText = "Невірний запит";

    await client.SendAsync(Encoding.UTF8.GetBytes(resultText));
    Console.WriteLine($"Запит: {text}, Відповідь: {resultText}");

    client.Shutdown(SocketShutdown.Both);
    client.Close();
}
