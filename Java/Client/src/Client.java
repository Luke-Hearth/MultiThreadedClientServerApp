import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class Client implements AutoCloseable {

    final int port = 8888;
    public Scanner reader;
    public PrintWriter writer;

    public Client() throws Exception{

        Socket socket = new Socket("localhost" , port);

        reader = new Scanner(socket.getInputStream());

        writer = new PrintWriter(socket.getOutputStream() , true);

        String line = reader.nextLine();

        if (line.trim().compareToIgnoreCase("success") != 0) {
            throw new Exception(line);


        }

    }

    void RefreshClient() throws Exception{
        Socket socket = new Socket("localhost" , port);

        reader = new Scanner(socket.getInputStream());

        writer = new PrintWriter(socket.getOutputStream() , true);

        String line = reader.nextLine();

        if (line.trim().compareToIgnoreCase("success") != 0) {
            throw new Exception(line);


        }

    }




    void UpdatePlayerWithBall(){
        String line = reader.nextLine();
        String[] words = line.split(" ");
        if(words[0].trim().compareToIgnoreCase("playerwithball") !=0){
            return;
        }
        ClientInfo.playerWithBall = words[1];

    }

    void HasBall(){
        String hasBallLine = reader.nextLine();
        if(hasBallLine.trim().compareToIgnoreCase("Ball") == 0){
            ClientInfo.hasBall = true;
        }
        else if(hasBallLine.trim().compareToIgnoreCase("NoBall") == 0){
            ClientInfo.hasBall = false;
        }
        else {
            return;
        }
    }



    void updatePlayersList(String line){

        String[] words = line.split(" ");
        //Checks to see if this is the right command else returns
        if(words[0].trim().compareToIgnoreCase("players") !=0){
            return;
        }
        ClientInfo.playerAsString.clear();
        for(int i = 1; i < words.length; i++){
            ClientInfo.playerAsString.add(words[i]);
        }
    }

    void updateNewPlayer(){
        String line = reader.nextLine();
        String[] words = line.split(" ");
        if(words[0].trim().compareToIgnoreCase("newplayer") !=0){
            return;
        }
        ClientInfo.newPlayerID = words[1];
    }


    void PassBall(){
        writer.println("pass " + ClientInfo.currentlySelectedPlayer);

    }


    @Override
    public void close() throws Exception {
        reader.close();
        writer.close();
        System.out.println("Server Closed");

    }
}
