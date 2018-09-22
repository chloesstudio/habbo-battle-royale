$(function(){
  if(getCookie("auth_key") != "") {
    window.location.replace("/lobby");
  }

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

  var translation;
  
  socket.onopen = function(event){
    console.log("Connected to the web socket server after " + Math.round(event.timeStamp) + "ms!");
    console.log(" ");

    $.getJSON("/resources/global/data/translations.json", function(data) {
      translation = data;

      var _html = $(".habbo-index").html();

      $.each(data.index, function(key, value) {
        _html = _html.replace(key, value, -1);
      });
    
      $(".habbo-index").html(_html);

      $(".habbo-overlay").fadeOut(900, function(){
        $("[-form-data=login]").submit(function(){
          var username = $("[-form-data=login] [-form-data=username] input").val();
          var password = $("[-form-data=login] [-form-data=password] input").val();

          $("[-form-data=login] div input").each(function(){
            if($(this).val() == "") {
              $(this).removeClass("habbo-form-input-blue").addClass("habbo-form-input-red").siblings("label").text(data.index["FORM_ERROR_REQUIRED"]).show(function(){

                $(this).siblings("input").effect("shake");
              });
            }
            else
              $(this).removeClass("habbo-form-input-red").addClass("habbo-form-input-blue").siblings("label").hide();
          });
          
          
          socket.send(JSON.stringify({
            Event: "GetAuthKey", 
            Username: username,
            Password: password
          }));

          return false;
        });
      }); 
    });
  };

  socket.onmessage = function(event){
    const reader = new FileReader();

    reader.addEventListener("loadend", (e) => {
      const text = e.srcElement.result;

      var response = JSON.parse(text);

      console.log("Received socket message from the server with header `" + response.Event + "`...");

      console.log(response);

      switch(response.Event) {
        case "GetAuthKeyEvent":
          
          switch(response.Code) {
            case 1:
              alert("OK");
              break;

            case 2: // error occured
              $("[-form-data=login] [-form-data=" + response.Inputfield + "] input").removeClass("habbo-form-input-blue").addClass("habbo-form-input-red").siblings("label").text(translation.index[response.Error]).show(function(){
                $("[-form-data=login] [-form-data=" + response.Inputfield + "] input").effect("shake");
              });
              break;
          }

          break;

        case "GetOnlineCountEvent":
          $("habbo-online-count").text(response.count);
          break;
      }
    });

    reader.readAsText(event.data);
  };
});