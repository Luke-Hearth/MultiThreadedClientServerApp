import java.util.*;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentLinkedQueue;

public class GameInfo {

    //Handles Global info about the game

    public static  int  playerNumber = 1;

    public static ConcurrentHashMap<Integer,Boolean> gameStatus = new ConcurrentHashMap<>();

    public static String newPLayerID;

    public static ConcurrentLinkedQueue<String> players = new ConcurrentLinkedQueue<>();
}
