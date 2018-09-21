using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WatsonWebsocket;

namespace Habbo {
  internal class WebSocket {
    public static WatsonWsServer Socket;

    public WebSocket(string address, int port) {
      Socket = new WatsonWsServer(address, port, false, true, null, OnClientConnect, OnClientDisconnect, OnMessageReceived, false);

      Console.WriteLine("Listening on IP/TCP connections at port " + port + "..." + Environment.NewLine);
    }

    private bool OnClientConnect(string ipPort, IDictionary<string, string> data) {
      Console.WriteLine(" A new client connected from " + ipPort + "...");

      if(Users.SocketUsers.Users.ContainsKey(ipPort)) {
        Console.WriteLine(" Detected faulty user data! Resetting socket user for " + ipPort + "...");
        Users.SocketUsers.Users.Remove(ipPort);
      }

      Users.SocketUsers.Users.Add(ipPort, new Users.SocketUser(ipPort));

      foreach (string client in (new List<string>(Socket.ListClients()))) {
        new Socket.Events.GetOnlineCountEvent(client);
      }

      return true;
    }

    private bool OnClientDisconnect(string ipPort) {
      Console.WriteLine(" Lost connection with " + ipPort + "...");

      if (Users.SocketUsers.Users.ContainsKey(ipPort))
        Users.SocketUsers.Users.Remove(ipPort);

      foreach (string client in (new List<string>(Socket.ListClients()))) {
        new Socket.Events.GetOnlineCountEvent(client);
      }

      return true;
    }

    private bool OnMessageReceived(string ipPort, byte[] data) {
      Console.WriteLine(" Received a new message by " + ipPort + "...");

      if (data == null || data.Length == 0)
        return false;

      new Socket.SocketEvents(ipPort, Encoding.UTF8.GetString(data));

      return true;
    }

    public static bool SendMessageAsync(string ipPort, string message) {
      Socket.SendAsync(ipPort, Encoding.UTF8.GetBytes(message));

      return true;
    }
  }
}
