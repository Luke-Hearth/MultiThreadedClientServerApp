import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class Server {

    private final static int portNum = 8888;

    public static void main(String[] args) {
        RunServer();
    }

    static void RunServer(){
        ServerSocket serverSocket = null;
        try{
            serverSocket = new ServerSocket(portNum);
            new ServerCloseable();
            System.out.println("Awaiting Connection..");
            while (true){
                Socket socket = serverSocket.accept();
                new Thread(new ClientHandler(socket)).start();

            }

        }
        catch (IOException e){
            e.printStackTrace();
        }


    }

}
