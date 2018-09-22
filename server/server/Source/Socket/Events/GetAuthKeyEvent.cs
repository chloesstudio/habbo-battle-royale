using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Security.Cryptography;

using Newtonsoft.Json;

namespace Habbo.Socket.Events {
  internal class GetAuthKeyRequest {
    public string Username { get; set; }
    public string Password { get; set; }
  }

  internal class GetAuthKeyResponse {
    public readonly string Event = "GetAuthKeyEvent";

    public int Code { get; set; }

    public string Inputfield { get; set; }

    public string Error { get; set; }

    public string AuthKey { get; set;  }

    public GetAuthKeyResponse(string inputfield, string error) {
      this.Code = 2;
      this.Inputfield = inputfield;
      this.Error = error;
    }

    public GetAuthKeyResponse(string authKey) {
      this.Code = 1;
      this.AuthKey = authKey;
    }
  }

  internal class GetAuthKeyEvent {
    public GetAuthKeyEvent(string client, string message) {
      GetAuthKeyRequest data = JsonConvert.DeserializeObject<GetAuthKeyRequest>(message);

      if (data.Username.Length == 0 || data.Password.Length == 0)
        return;

      SQLiteConnection connection = new SQLiteConnection(Program.ConnectionString);
      connection.Open();
      
      SQLiteCommand command = new SQLiteCommand("SELECT * FROM `users` WHERE `username` = @username LIMIT 1", connection);
      command.Parameters.AddWithValue("@username", data.Username);

      SQLiteDataReader reader = command.ExecuteReader();

      if(!reader.Read()) {
        WebSocket.SendMessageAsync(client, JsonConvert.SerializeObject(new GetAuthKeyResponse("username", "FORM_ERROR_USERNAME_INVALID")));

        connection.Close();

        return;
      }

      using (SHA256 sha256Hash = SHA256.Create()) {
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data.Password));

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++) {
          builder.Append(bytes[i].ToString("x2"));
        }

        string hash = builder.ToString();

        if (Convert.ToString(reader["password"]) != hash) {
          WebSocket.SendMessageAsync(client, JsonConvert.SerializeObject(new GetAuthKeyResponse("password", "FORM_ERROR_PASSWORD_INVALID")));

          connection.Close();

          return;
        }
      }
    }
  }
}
