$(function(){
  console.log("  _    _       _     _             ____        _   _   _        _____                   _      "             + "\n" +
              " | |  | |     | |   | |           |  _ \\      | | | | | |      |  __ \\                 | |     "           + "\n" +
              " | |__| | __ _| |__ | |__   ___   | |_) | __ _| |_| |_| | ___  | |__) |___  _   _  __ _| | ___ "             + "\n" +
              " |  __  |/ _` | '_ \\| '_ \\ / _ \\  |  _ < / _` | __| __| |/ _ \\ |  _  // _ \\| | | |/ _` | |/ _ \\"       + "\n" +
              " | |  | | (_| | |_) | |_) | (_) | | |_) | (_| | |_| |_| |  __/ | | \\ \\ (_) | |_| | (_| | |  __/"           + "\n" +
              " |_|  |_|\\__,_|_.__/|_.__/ \\___/  |____/ \\__,_|\\__|\\__|_|\\___| |_|  \\_\\___/ \\__, |\\__,_|_|\\___|"  + "\n" +
              "                                                                             __/ |             "             + "\n" +
              "                                                                            |___/              ");

  console.log("Connecting to the web socket server at `127.0.0.1:7777`...");

  var socket = new WebSocket("ws://127.0.0.1:7777");
  
  socket.onopen = function(event){
    console.log("Connected to the web socket server after " + Math.round(event.timeStamp) + "ms!");
    console.log(" ");

    $.getJSON("/resources/global/data/translations.json", function(data) {
      var _html = $(".habbo-index").html();

      $.each(data.index, function(key, value) {
        _html = _html.replace("${" + key + "}", value, -1);
      });
    
      $(".habbo-index").html(_html);

      console.log("Replaced all of (" + data.index.Count + ") strings for the index.");

      $(".habbo-overlay").fadeOut(900);
    });
  };

  socket.onmessage = function(event){
    const reader = new FileReader();

    reader.addEventListener("loadend", (e) => {
      const text = e.srcElement.result;

      var response = JSON.parse(text);

      console.log("Received socket message from the server with header `" + response.Event + "`...");

      /*switch(response.Event) {
        case "AuthenticateCredentialsEvent":
          
          switch(response.Response) {
            case 1:
              alert("OK");
              break;
          }

          break;

        case "OnlineCountEvent":
          $(".habbo-index").html($(".habbo-index").html().replace("${ONLINE_COUNT}", response.Count, -1));
          break;
      }*/
    });

    reader.readAsText(event.data);
  };

  $("[-form-data=login]").submit(function(){
    
    var username = $("[-form-data=login] [-form-data=username]").val();
    var password = $("[-form-data=login] [-form-data=password]").val();

    console.log("AuthenticateCredentialsEvent > Authenticating login credentials per `" + username + "`...");
    
    socket.send(JSON.stringify({
      Event: "AuthenticateCredentialsEvent",
      Username: username,
      Password: password
    }));

    return false;
  });
});