namespace VS_ServerEmu.VS;

public class LoginResponse
{
    public int valid; // set 1 to accept, others to be bad

    public string reason;

    public string reasondata;

    public string sessionkey; //random

    public string sessionsignature; // send somethings. can be anything

    public string uid;

    public string playername;

    public string entitlements; // return nothing

    public string prelogintoken;

    public bool hasgameserver;
}
