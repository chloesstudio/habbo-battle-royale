using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Habbo.Socket.Events {
  internal class GetOnlineCountResponse {
    public readonly string Event = "GetOnlineCountEvent";

    public readonly int count;

    public int GetCount() {
      return count;
    }

    public GetOnlineCountResponse(int count) {
      this.count = count;
    }
  }

  internal class GetOnlineCountEvent {
    public GetOnlineCountEvent(string client) => WebSocket.SendMessageAsync(client, JsonConvert.SerializeObject(new GetOnlineCountResponse(WebSocket.Socket.ListClients().Count())));
  }
}