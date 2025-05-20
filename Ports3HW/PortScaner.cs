using System.Net.Sockets;

public class PortScaner{
    private static readonly object lockObject = new object();
    private static readonly List<string> results = new List<string>();
    public static async Task ScanPorts(string domain, int startPort, int endPort, int blockSize = 500){
        Console.WriteLine($"Сканування портів {startPort}-{endPort} для {domain}...\n");

        for (int blockStart = startPort; blockStart <= endPort; blockStart += blockSize){
            int blockEnd = Math.Min(blockStart + blockSize - 1, endPort);
            List<Task> tasks = new List<Task>();

            Console.WriteLine($"Перевіряємо порти {blockStart}-{blockEnd}...");

            for (int port = blockStart; port <= blockEnd; port++){
                int currentPort = port;
                tasks.Add(Task.Run(async () =>{
                    using (TcpClient tcpClient = new TcpClient()){
                        var task = tcpClient.ConnectAsync(domain, currentPort);
                        if (await Task.WhenAny(task, Task.Delay(5000)) == task && tcpClient.Connected){
                            lock (lockObject){
                                results.Add($"Порт {currentPort} відкритий");
                            }
                        }
                        tcpClient.Close();
                    }
                }));
            }
            await Task.WhenAll(tasks);
        }
        foreach (var result in results){
            Console.WriteLine(result);
        }
        Console.WriteLine("\nСканування завершено");
    }
}