import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ClientGUI extends JFrame {

    public static JButton send = new JButton("Send to player");
    public static JLabel newPlayer = new JLabel("New player:");
    public static JLabel ballPlayer = new JLabel("Ball player:");
    public static JComboBox playerList = new JComboBox();
    public static JPanel centre = new JPanel();


    public ClientGUI(Client client){


        send.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                ClientInfo.currentlySelectedPlayer = String.valueOf(playerList.getSelectedItem());
                client.PassBall();

            }
        });


        send.setEnabled(false);
        centre.add(newPlayer , BorderLayout.CENTER);
        centre.add(ballPlayer , BorderLayout.CENTER);
        add(centre , BorderLayout.CENTER);
        setTitle("Client GUI");


        add(playerList , BorderLayout.NORTH);
        add(send , BorderLayout.SOUTH);



        setSize(400,400);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setVisible(true);
        setResizable(true);
        setLocationRelativeTo(null);
    }


}
