using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
await client.ConnectAsync("127.0.0.1", 2008);

Console.Write("Введіть 'date', 'time' або 'datetime' для отримання часу: ");
string message = Console.ReadLine()?.Trim().ToLower();

byte[] buffer = Encoding.UTF8.GetBytes(message);
await client.SendAsync(buffer, SocketFlags.None);

buffer = new byte[255];
int bytes = await client.ReceiveAsync(buffer, SocketFlags.None);

Console.WriteLine($"Відповідь від сервера: {Encoding.UTF8.GetString(buffer, 0, bytes)}");

client.Shutdown(SocketShutdown.Both);
client.Close();
