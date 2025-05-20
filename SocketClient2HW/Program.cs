using System.Net;
using System.Net.Sockets;
using System.Text;


Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
client.Connect("127.0.0.1", 2008);

Console.Write("Введіть 'date', 'time' або 'datetime' для отримання часу: ");
string message = Console.ReadLine()?.Trim().ToLower();

byte[] buffer = Encoding.UTF8.GetBytes(message);
client.Send(buffer);

buffer = new byte[255];
int bytes = client.Receive(buffer);

Console.WriteLine($"Відповідь від сервера: {Encoding.UTF8.GetString(buffer, 0, bytes)}");

