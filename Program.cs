namespace VS_ServerEmu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerMain.Start();
            Console.ReadLine();
            ServerMain.Stop();
        }
    }
}
