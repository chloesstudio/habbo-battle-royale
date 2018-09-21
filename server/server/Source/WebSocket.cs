using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WatsonWebsocket;

namespace Habbo {
  internal class WebSocket {
    private readonly WatsonWsServer Socket;

    public WebSocket(string address, int port) {
      Socket = new WatsonWsServer(address, port, false, true, null, OnClientConnect, OnClientDisconnect, OnMessageReceived, false);
    }

    private bool OnMessageReceived(string ipPort, byte[] data) {
      throw new NotImplementedException();
    }

    private bool OnClientDisconnect(string ipPort) {
      throw new NotImplementedException();
    }

    private bool OnClientConnect(string ipPort, IDictionary<string, string> data) {
      throw new NotImplementedException();
    }
  }
}
