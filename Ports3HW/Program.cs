using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.Write("Введіть домен: ");
string domain = Console.ReadLine();

Console.Write("Введіть початковий порт: ");
int startPort = int.Parse(Console.ReadLine());

Console.Write("Введіть кінцевий порт: ");
int endPort = int.Parse(Console.ReadLine());

await PortScaner.ScanPorts(domain, startPort, endPort);
