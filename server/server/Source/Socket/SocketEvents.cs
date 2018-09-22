using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Habbo.Socket {
  internal class SocketEvent {
    public string Event { get; set; }
  }

  internal class SocketEvents {
    public SocketEvents(string client, string message) {
      SocketEvent data = JsonConvert.DeserializeObject<SocketEvent>(message);

      switch(data.Event) {
        case "GetOnlineCount":
          new Events.GetOnlineCountEvent(client);
          break;

        case "GetAuthKey":
          new Events.GetAuthKeyEvent(client, message);
          break;

        default:
          Console.ForegroundColor = ConsoleColor.DarkRed;

          Console.WriteLine(" Received a faulty socket event header from " + client + "!");

          Console.ResetColor();
          break;
      }
    }
  }
}
