using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2009);
await client.ConnectAsync(endPoint);

string message = "Привіт, сервер!";

await client.SendAsync(Encoding.UTF8.GetBytes(message));

byte[] buffer = new byte[1024];
int bytes = await client.ReceiveAsync(buffer);

IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
string serverIp = ip.Address.ToString();
Console.WriteLine($"О {DateTime.UtcNow.ToLocalTime().ToShortTimeString()} від [{serverIp}] отримано рядок: " + Encoding.UTF8.GetString(buffer, 0, bytes));

client.Shutdown(SocketShutdown.Both);
client.Close();
