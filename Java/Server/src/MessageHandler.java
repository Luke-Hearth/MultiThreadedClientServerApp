import java.io.PrintWriter;
import java.util.Scanner;

public class MessageHandler implements Runnable {

    private final Scanner scanner;
    private final PrintWriter writer;
    private  int thisPlayerNum;



    public MessageHandler(Scanner scanner , PrintWriter writer , int thisPlayerNum){

        this.scanner = scanner;
        this.thisPlayerNum = thisPlayerNum;
        this.writer = writer;
    }

    @Override
    public void run() {
        try {
            while (true) {
                //Checks if it even needs to get the new line
                String line = scanner.nextLine();
                String[] words = line.split(" ");
                //Checks if the input is actually what we want i.e the "pass" keyword
                if (words[0].trim().compareToIgnoreCase("pass") == 0) {
                    int passPlayerNum = Integer.parseInt(words[1]);
                    //Check if the playernumber is a different one than the client
                    if (thisPlayerNum != passPlayerNum) {
                        if (GameInfo.gameStatus.containsKey(passPlayerNum)) {
                            GameInfo.gameStatus.replace(thisPlayerNum, false);
                            GameInfo.gameStatus.replace(passPlayerNum, true);
                            System.out.println("The ball has been passed to player " + passPlayerNum);
                        } else {

                            System.out.println("The ball doesnt pass the player has disconnected " + thisPlayerNum + " still has ball");
                        }
                    } else {

                        System.out.println("The ball is passed to: " + thisPlayerNum);

                    }

                }


            }
        }
        catch (Exception e){
            System.out.println("Player " + thisPlayerNum + " has disconnected");
            RemoveInfo(thisPlayerNum);

        }
    }

    void RemoveInfo(int thisPlayerNum){
        GameInfo.players.remove(Integer.toString(thisPlayerNum));
        //If the dissconnected guy has the ball
        if(GameInfo.gameStatus.get(thisPlayerNum)){
            GameInfo.gameStatus.remove(thisPlayerNum);
            //If the game is not empty
            if(!GameInfo.gameStatus.isEmpty()){
                //Set an element in the map to have the ball
                for(int key : GameInfo.gameStatus.keySet()){
                    GameInfo.gameStatus.replace(key,true);
                    System.out.println("The ball has been passed to " +  key + " due to disconnect");
                    break;
                }

            }
        }
        else{
            GameInfo.gameStatus.remove(thisPlayerNum);
        }

        System.out.println("All players: " + GameInfo.players);
        writer.close();
        scanner.close();
    }
}
