using System;

namespace AI_Fighter_2d
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CoreGame())
                game.Run();
        }
    }
}
