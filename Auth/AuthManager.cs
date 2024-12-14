using System.Text.Json;
using VS_ServerEmu.UserRelated;

namespace VS_ServerEmu.Auth;

public static class AuthManager
{
    static List<UserJson> Users = [];

    public static bool IsUserExist_Id(string uid)
    {
        Reload();
        return Users.Any(x => x.Id == uid);
    }

    public static bool IsUserExist_Email(string email)
    {
        Reload();
        return Users.Any(x => x.Email == email);
    }
    public static UserJson? GetUser_Id(string uid)
    {
        Reload();
        return Users.FirstOrDefault(x => x.Id == uid);
    }

    public static UserJson? GetUser_Email(string email)
    {
        Reload();
        return Users.FirstOrDefault(x => x.Email == email);
    }

    public static UserJson? GetUser_Name(string name)
    {
        Reload();
        return Users.FirstOrDefault(x => x.Name == name);
    }

    public static void CreateNewUser(string email, string id)
    {
        Users.Add(new()
        { 
            Email = email,
            Id = id,
            Name = email
        });
        Save();
        Reload();
    }


    public static void Reload()
    {
        Users = JsonSerializer.Deserialize<List<UserJson>>(File.ReadAllText("Users.json"));
        if (Users == null)
            Users = [];
    }

    public static void Save()
    {
        File.WriteAllText("Users.json", JsonSerializer.Serialize(Users));
    }
}
