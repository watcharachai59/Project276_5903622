var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);

server.listen(3000);

var clients = [];

app.get('/', function(req, res) {
	res.send('hey you got back get "/"');
});

io.on('connection', function(socket) {
	
	var currentPlayer = {};
	currentPlayer.name = 'unknown';

	socket.on('player connect', function() {
		console.log(currentPlayer.name+' recv: player connect');
		for(var i =0; i<clients.length;i++) {
			var playerConnected = {
				name:clients[i].name,
				status:clients[i].status
			};
			// in your current game, we need to tell you about the other players.
			socket.emit('other player connected', playerConnected);
			console.log(currentPlayer.name+' emit: other player connected: '+JSON.stringify(playerConnected));
		}
    });









    socket.on('s1', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('Next2');
               socket.broadcast.emit('Next2');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 


    });

    
    socket.on('s2', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('Next3');
               socket.broadcast.emit('Next3');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 


    });


    socket.on('s3', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('NextEnd');
               socket.broadcast.emit('NextEnd');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 


    });








    socket.on('SendScore', function(num)
    {
       
        data = {
            names:currentPlayer.name,
            count:num.Textend.toString()
        };
    
        socket.emit('SendScore',data);
        socket.broadcast.emit('SendScore',data);

        console.log("002"+JSON.stringify(data));
    });







    socket.on('send1', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('Next2');
               socket.broadcast.emit('Next2');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 

        
        if(num.textin === 1)
        {
            socket.emit("score");
            socket.emit("True");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);

            
        }
        
        else
        {
            socket.emit("False");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);


        }

    });




    socket.on('send2', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('Next3');
               socket.broadcast.emit('Next3');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 

        
        if(num.textin === 3)
        {
            socket.emit("score");
            socket.emit("True");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);

            
        }
        
        else
        {
            socket.emit("False");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);


        }

    });








    socket.on('send3', function(num)
    {
        var c =0;
   
        console.log(clients[0]);
        for(var i =0; i<clients.length;i++) {
   
           if(clients[i].status !== "ตอบแล้ว"){
               c++;
           }
           console.log(clients[i].status);
           };
           if(c ===0){
               socket.emit('NextEnd');
               socket.broadcast.emit('NextEnd');
   
               for(var i =0; i<clients.length;i++) {
                   clients[i].status ="OnPlay";
   
                   socket.emit('status', clients[i]);
                   socket.broadcast.emit('status', clients[i]);
               }
              
              
           } 

        
        if(num.textin === 3)
        {
            socket.emit("score");
            socket.emit("True");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);

            
        }
        
        else
        {
            socket.emit("False");
            currentPlayer.status ="ตอบแล้ว";
            socket.emit('status', currentPlayer);
            socket.broadcast.emit('status',  currentPlayer);


        }

    });




























	socket.on('play', function(data) {
		console.log(currentPlayer.name+' recv: play: '+JSON.stringify(data));
		// if this is the first person to join the game init the enemies
		if(clients.length === 0) {
			 
		}
 
		// we always will send the enemies when the player joins
		 
 		currentPlayer = {
			name:data.name,
		status:data.status
		};
		clients.push(currentPlayer);
		// in your current game, tell you that you have joined
		console.log(currentPlayer.name+' emit: play: '+JSON.stringify(currentPlayer));
		socket.emit('play', currentPlayer);
		// in your current game, we need to tell the other players about you.
		socket.broadcast.emit('other player connected', currentPlayer);
	});
    socket.on('ready', function(data) {
        console.log(clients[0]);
	 currentPlayer.status = data.status;
     socket.emit('status', currentPlayer);
     socket.broadcast.emit('status', currentPlayer);
     var c =0;
   
     console.log(clients[0]);
     for(var i =0; i<clients.length;i++) {

        if(clients[i].status !== "Ready"){
            c++;
        }
        console.log(clients[i].status);
        };
        if(c ===0){
            socket.emit('startgame');
            socket.broadcast.emit('startgame');

            for(var i =0; i<clients.length;i++) {
                clients[i].status ="OnPlay";

                socket.emit('status', clients[i]);
                socket.broadcast.emit('status', clients[i]);
            }
           
           
        } 
        console.log(c);
	});
     
  
   

	 

	socket.on('disconnect', function() {
		console.log(currentPlayer.name+' recv: disconnect '+currentPlayer.name);
		socket.broadcast.emit('other player disconnected', currentPlayer);
		console.log(currentPlayer.name+' bcst: other player disconnected '+JSON.stringify(currentPlayer));
		for(var i=0; i<clients.length; i++) {
			if(clients[i].name === currentPlayer.name) {
				clients.splice(i,1);
			}
		}
	});

});

console.log('--- server is running ...');

function guid() {
	function s4() {
		return Math.floor((1+Math.random()) * 0x10000).toString(16).substring(1);
	}
	return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}