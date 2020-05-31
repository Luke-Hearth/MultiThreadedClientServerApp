import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;


public class ClientHandler implements  Runnable {

    private final Socket socket;
    int thisPlayerNum;

    public ClientHandler(Socket socket){
        this.socket = socket;

    }

    @Override
    public void run() {
        try {
            //Creates scanner and writed so that the server can send and read messages from the client
            Scanner scanner = new Scanner(socket.getInputStream());
            PrintWriter writer = new PrintWriter(socket.getOutputStream(), true);
            try {

                thisPlayerNum = AddInfo(thisPlayerNum);

                writer.println("Success");
                new Thread(new MessageHandler(scanner , writer , thisPlayerNum)).start();

                while (true) {
                    // This is where i test to see if there is still a connection
                    if (scanner.ioException() != null) {
                        // Client is no longer available
                        return;
                    }

                    SendPlayers(writer);
                    SendNewPlayer(writer);
                    SendPlayerWithBall(writer);

                    //If this player has the ball allow it to send mesages about passing ball

                    hasBall(writer , thisPlayerNum);

                }

            } catch (Exception e) {


            }
        } catch (Exception e) {

            e.printStackTrace();
        }
    }
    void SendPlayers(PrintWriter writer){

        String playersList = "players ";
        for(String player: GameInfo.players){
            playersList += player;
            playersList += " ";

        }
         writer.println(playersList);

    }

    void SendPlayerWithBall(PrintWriter writer){

        String playersWithBaLL = "playerwithball ";
        for(int key : GameInfo.gameStatus.keySet()){
            if(GameInfo.gameStatus.get(key)){
                playersWithBaLL += Integer.toString(key);
                writer.println(playersWithBaLL);
                return;
            }

        }


    }

    void SendNewPlayer(PrintWriter writer){

        String newPlayerInfo = "newplayer " + GameInfo.newPLayerID;
        writer.println(newPlayerInfo);
    }

    int AddInfo(int thisPlayerNum){
        //Assings a new unique ID for each player incrementally
        thisPlayerNum = GameInfo.playerNumber;
        GameInfo.newPLayerID = Integer.toString(thisPlayerNum);
        GameInfo.players.add(Integer.toString(thisPlayerNum));
        GameInfo.playerNumber += 1;
        System.out.println("New connection with player: " + thisPlayerNum);
        System.out.println("All players: " + GameInfo.players);

        //if there are no player teh first player to connect gets the ball

        if (GameInfo.gameStatus.isEmpty()) {
            GameInfo.gameStatus.put(thisPlayerNum, true);
        } else {
            GameInfo.gameStatus.put(thisPlayerNum, false);
        }
        return thisPlayerNum;
    }



     void hasBall(PrintWriter writer , int thisPlayerNum){
        if (GameInfo.gameStatus.get(thisPlayerNum)) {
            writer.println("Ball");

        } else {
            writer.println("NoBall");
        }
    }
}


