using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbo {
  internal class Program {
    private static void Main(string[] args) {
      Console.Title = "Habbo Battle Royale Server";

      Console.WriteLine("  _    _       _     _             ____        _   _   _        _____                   _      " + Environment.NewLine +
              " | |  | |     | |   | |           |  _ \\      | | | | | |      |  __ \\                 | |     " + Environment.NewLine +
              " | |__| | __ _| |__ | |__   ___   | |_) | __ _| |_| |_| | ___  | |__) |___  _   _  __ _| | ___ " + Environment.NewLine +
              " |  __  |/ _` | '_ \\| '_ \\ / _ \\  |  _ < / _` | __| __| |/ _ \\ |  _  // _ \\| | | |/ _` | |/ _ \\" + Environment.NewLine +
              " | |  | | (_| | |_) | |_) | (_) | | |_) | (_| | |_| |_| |  __/ | | \\ \\ (_) | |_| | (_| | |  __/" + Environment.NewLine +
              " |_|  |_|\\__,_|_.__/|_.__/ \\___/  |____/ \\__,_|\\__|\\__|_|\\___| |_|  \\_\\___/ \\__, |\\__,_|_|\\___|" + Environment.NewLine +
              "                                                                             __/ |             " + Environment.NewLine +
              "                                                                            |___/              " + Environment.NewLine);
      try {
        new WebSocket("127.0.0.1", 7777);
      }
      catch(Exception exception) {
        Console.ForegroundColor = ConsoleColor.DarkRed;

        Console.WriteLine(exception.Message);

        Console.ResetColor();
      }
      

      while(true)
        Console.ReadLine();
    }
  }
}
