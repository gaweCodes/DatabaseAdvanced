using System;
using System.Linq;

namespace VidAppCodeFirst
{
    internal static class Program
    {
        private static void Main()
        {
            using (var ctx = new VidAppContext())
            {
                Console.WriteLine(ctx.Genres.Count());
            }
            Console.ReadLine();
        }
    }
}
