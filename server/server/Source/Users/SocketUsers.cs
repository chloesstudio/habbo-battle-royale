using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habbo.Users {
  internal class SocketUser {
    private string networkAddress;

    private string GetNetworkAddress() {
      return networkAddress;
    }

    private void SetNetworkAddress(string value) {
      networkAddress = value;
    }

    private int networkPort;

    private int GetNetworkPort() {
      return networkPort;
    }

    private void SetNetworkPort(int value) {
      networkPort = value;
    }

    private string authKey = "";

    private string GetAuthKey() {
      return authKey;
    }

    private void SetAuthKey(string value) {
      authKey = value;
    }

    public SocketUser(string ipPort) {
      string[] networkData = ipPort.Split(':');

      this.networkAddress = networkData[0];
      this.networkPort = Convert.ToInt32(networkData[1]);
    }
  }

  internal class SocketUsers {
    public static Dictionary<string, SocketUser> Users = new Dictionary<string, SocketUser>();
  }
}
