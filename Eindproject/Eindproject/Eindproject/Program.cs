using System;

namespace Eindproject
{
#if WINDOWS || XBOX
    static class Program
    {

        static void Main(string[] args)
        {
            using (GameEngine game = new GameEngine())
            {
                game.Run();
            }
        }

    }
#endif
}

