import javax.swing.*;
import java.awt.event.WindowEvent;
import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;

public class ClientProgram {



    public static void main(String[] args) {
        System.out.println("Started");

        try(Client client = new Client()){
            ClientGUI gui = new ClientGUI(client);
            System.out.println("Logged in succsessfull");
            while(true) {
                try {
                    String line = client.reader.nextLine();
                    String[] lineParts = line.split(" ");

                    switch (lineParts[0]) {
                        case "players":
                            client.updatePlayersList(line);
                            break;
                        default:
                            break;
                    }



                    client.updateNewPlayer();
                    client.UpdatePlayerWithBall();
                    client.HasBall();
                    UpdateGUI();
                }
                catch (Exception e){
                    e.printStackTrace();
                    try {
                        if (ClientInfo.hasBall) {
                            File serverPath = new File("..\\Server\\out\\production\\Server");
                            if(serverPath.exists()) {
                                System.out.println("AHHH");
                                ProcessBuilder pc = new ProcessBuilder("java" ,
                                        "-cp",
                                        serverPath.getAbsolutePath(),
                                        "Server" );


                                try {

                                    pc.start();
                                    Thread.sleep(1000);
                                    client.RefreshClient();
                                } catch (IOException x) {
                                    x.printStackTrace();
                                }
                            }


                        } else {

                            Thread.sleep(1200);
                            client.RefreshClient();
                        }
                    }
                    catch (Exception v){
                        v.printStackTrace();
                        System.out.println("Failed to restart server");
                        gui.dispatchEvent(new WindowEvent( gui , WindowEvent.WINDOW_CLOSING));
                    }
                }


            }
        }
        catch(Exception e){
            System.out.println(e.getMessage());
        }
    }

    static void UpdateGUI(){
        if (!ClientGUI.newPlayer.getText().equals("New player: " + ClientInfo.newPlayerID))
        {
            ClientGUI.newPlayer.setText("New player: " + ClientInfo.newPlayerID );
        }

        if (!ClientGUI.ballPlayer.getText().equals("Ball player: " + ClientInfo.playerWithBall))
        {
            ClientGUI.ballPlayer.setText("Ball player: " + ClientInfo.playerWithBall);
            ClientGUI.send.setEnabled(ClientInfo.hasBall);
        }

        if(ClientInfo.playerAsString.size() != ClientGUI.playerList.getModel().getSize()) {
            ClientGUI.playerList.removeAllItems();
            for (String player : ClientInfo.playerAsString) {
                ClientGUI.playerList.addItem(player);
            }
        }
    }



}
