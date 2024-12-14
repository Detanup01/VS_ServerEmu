using ModdableWebServer.Servers;
using NetCoreServer;
using System.Reflection;

namespace VS_ServerEmu;

internal class ServerMain
{
    static HTTP_Server? Server;

    public static void Start(int port = 80)
    {
        Server = new("0.0.0.0", port);
        Server.OverrideAttributes(Assembly.GetCallingAssembly());
        Server.ReceivedFailed += ReceivedFailed;
        Server.Start();
    }

    private static void ReceivedFailed(object? sender, HttpRequest request)
    {
        try
        {
            File.AppendAllText("REQUESTED.txt", request.Url + "\n" + request.Method + "\n" + request.Body + "\n");
        }
        catch { }
        Console.Write("something isnt good: ");
        Console.Write(request.Method + "  ");
        Console.WriteLine(request.Url);
    }

    public static void Stop()
    {
        Server?.Stop();
    }
}
