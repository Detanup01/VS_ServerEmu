using System.Security.Cryptography;
using System.Text;

namespace VS_ServerEmu;

public static class Helper
{

    public static string MakeAccountId(string email, string password)
    {
        using MD5 md = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes($"{email}|{password}");
        byte[] array = md.ComputeHash(bytes);
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("X2"));
        }
        return stringBuilder.ToString();
    }

    public static Dictionary<string, string> SplitForm(string body)
    {
        Dictionary<string, string> form = new();
        if (body.Contains("&"))
        {
            var and_splitted = body.Split("&");
            foreach (var kvp in and_splitted)
            {
                var splitted_eq = kvp.Split("=");
                form.Add(splitted_eq[0], splitted_eq[1]);
            }
        }
        else
        {
            var splitted_eq = body.Split("=");
            form.Add(splitted_eq[0], splitted_eq[1]);
        }
        return form;
    }
}
