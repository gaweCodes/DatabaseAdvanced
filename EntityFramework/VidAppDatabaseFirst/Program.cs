using System;
using System.Linq;

namespace VidApp
{
    internal static class Program
    {
        private static void Main()
        {
            using (var context = new VidAppEntities())
            {
                context.AddVideo("Basco 1", DateTime.Now, context.Genres.First().Name, (byte) Classification.Platin);
                context.AddVideo("Basco 2", DateTime.Now, context.Genres.First().Name, (byte) Classification.Platin);
                context.AddVideo("Basco 3", DateTime.Now, context.Genres.First().Name, (byte) Classification.Platin);
            }
            Console.ReadLine();

        }
    }
}
