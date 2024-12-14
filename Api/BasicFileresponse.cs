using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using ModdableWebServer.Helper;

namespace VS_ServerEmu.Api;

internal class BasicFileresponse
{
    [HTTP("GET", "/latestunstable.txt")]
    public static bool latestunstable(HttpRequest request, ServerStruct serverStruct)
    {
        serverStruct.Response.MakeGetResponse(File.ReadAllText("stable-unstable.json"));
        serverStruct.SendResponse();
        return true;
    }

    [HTTP("GET", "/stable-unstable.json")]
    public static bool stableunstable(HttpRequest request, ServerStruct serverStruct)
    {
        serverStruct.Response.MakeGetResponse(File.ReadAllText("stable-unstable.json"));
        serverStruct.SendResponse();
        return true;
    }

    [HTTP("GET", "/vshostingunsupported.txt")]
    public static bool vshostingunsupported(HttpRequest request, ServerStruct serverStruct)
    {
        serverStruct.Response.MakeGetResponse(File.ReadAllText("vshostingunsupported.txt"));
        serverStruct.SendResponse();
        return true;
    }

    [HTTP("GET", "/lateststable.txt")]
    public static bool lateststable(HttpRequest request, ServerStruct serverStruct)
    {
        serverStruct.Response.MakeGetResponse(File.ReadAllText("lateststable.txt"));
        serverStruct.SendResponse();
        return true;
    }
}
