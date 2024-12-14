using ModdableWebServer.Attributes;
using ModdableWebServer;
using NetCoreServer;
using ModdableWebServer.Helper;
using VS_ServerEmu.VS;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace VS_ServerEmu.Auth
{
    internal class Auth2
    {
        [HTTP("POST", "/clientvalidate")]
        public static bool clientvalidate(HttpRequest request, ServerStruct serverStruct)
        {
            serverStruct.Response.MakeGetResponse("");
            serverStruct.SendResponse();
            return true;
        }

        [HTTP("POST", "/v2.1/clientrequestmptoken")]
        public static bool clientrequestmptoken(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            Console.WriteLine($"{splitted["uid"]} has sent a client request MP Token, with serverlogin: {splitted["serverlogintoken"]}");

            MpTokenResponse mpTokenResponse = new()
            { 
                valid = 1,
                mptokenv2 = RandomNumberGenerator.GetHexString(10),
                reason = ""
            };

            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(mpTokenResponse));
            serverStruct.SendResponse();
            return true;
        }


        [HTTP("POST", "/v2/gamelogin")]
        public static bool V2GameLogin(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            if (!AuthManager.IsUserExist_Email(splitted["email"]))
            {
                AuthManager.CreateNewUser(splitted["email"], Helper.MakeAccountId(splitted["email"], splitted["password"]));
                Console.WriteLine("User created with mail: " + splitted["email"]);
            }
            var user = AuthManager.GetUser_Email(splitted["email"])!;
            
            LoginResponse loginResponse = new()
            {
                valid = 1,
                hasgameserver = false,
                playername = user.Name,
                sessionkey = RandomNumberGenerator.GetHexString(3),
                sessionsignature = RandomNumberGenerator.GetHexString(3),
                uid = user.Id,
                entitlements = null,
                prelogintoken = "nonde",
                reason = "has an account",
                reasondata = "data"
            };

            Console.WriteLine($"{splitted["email"]} has logged in");
            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(loginResponse));
            serverStruct.SendResponse();
            return true;
        }

        [HTTP("POST", "/gamelogout")]
        public static bool gamelogout(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            Console.WriteLine($"{splitted["email"]} has logged out");

            serverStruct.Response.MakeOkResponse();
            serverStruct.SendResponse();
            return true;
        }

        [HTTP("POST", "/v2/servervalidate")]
        public static bool servervalidate(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            Console.WriteLine($"{splitted["uid"]} has sent a server validate: {splitted["serverlogintoken"]}");
            var user = AuthManager.GetUser_Id(splitted["uid"])!;
            ValidateServerResponse validateResponse = new()
            { 
                entitlements = null,
                playername = user.Name,
                valid = 1,
                reason = "Because I said it!"
            
            };

            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(validateResponse));
            serverStruct.SendResponse();
            return true;
        }


        [HTTP("POST", "/resolveplayername")]
        public static bool resolveplayername(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            Console.WriteLine("resolveplayername");
            var user = AuthManager.GetUser_Name(splitted["playername"])!;
            ResolveResponse resolveResponse = new()
            { 
                playeruid = user.Id
            };
            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(resolveResponse));
            serverStruct.SendResponse();
            return true;
        }

        [HTTP("POST", "/resolveplayeruid")]
        public static bool resolveplayeruid(HttpRequest request, ServerStruct serverStruct)
        {
            var splitted = Helper.SplitForm(request.Body);
            Console.WriteLine("resolveplayeruid");
            var user = AuthManager.GetUser_Name(splitted["uid"])!;
            ResolveResponseUid resolveResponse = new()
            {
                playername = user.Name,
            };
            serverStruct.Response.MakeGetResponse(JsonConvert.SerializeObject(resolveResponse));
            serverStruct.SendResponse();
            return true;
        }
    }
}
