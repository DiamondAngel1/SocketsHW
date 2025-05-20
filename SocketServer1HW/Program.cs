using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var endPoint = new IPEndPoint(IPAddress.Any, 2009);
server.Bind(endPoint);

server.Listen(10);

Console.WriteLine($"Запуск сервера {endPoint}");


while (true)
{
    Socket client = server.Accept();

    var buffer = new byte[1024];
    int sizeBytes = client.Receive(buffer);

    var text = Encoding.UTF8.GetString(buffer, 0, sizeBytes);
    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
    string clientIp = ip.Address.ToString();
    Console.WriteLine($"О {DateTime.UtcNow.ToLocalTime().ToShortTimeString()} від [{clientIp}] отримано рядок: : " + text);
    string resultText = "Привіт, клієнт!";
    client.Send(Encoding.UTF8.GetBytes(resultText));

    client.Shutdown(SocketShutdown.Both);

    client.Close();
}

server.Close();