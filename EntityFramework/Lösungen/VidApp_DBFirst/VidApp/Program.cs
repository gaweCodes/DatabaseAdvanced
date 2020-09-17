using System;

namespace VidApp {
    class Program {
        static void Main(string[] args) {
            using (var context = new VidAppContext()) {
                context.Database.Log = Console.WriteLine;

                context.AddVideo("Video 4", DateTime.Today, "Horror", (byte) Classification.Gold);
            }
        }
    }
}