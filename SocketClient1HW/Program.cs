using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

client.Connect("127.0.0.1", 2009);

string message = $"Привіт, сервер!";

client.Send(Encoding.UTF8.GetBytes(message));

byte[] buffer = new byte[1024];
int bytes = client.Receive(buffer);

IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
string serverIp = ip.Address.ToString();
Console.WriteLine($"О {DateTime.UtcNow.ToLocalTime().ToShortTimeString()} від [{serverIp}] отримано рядок: " + Encoding.UTF8.GetString(buffer));

client.Shutdown(SocketShutdown.Both);
client.Close();
