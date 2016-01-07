using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    public static RespawnScript respawnScript;

    public static void respawn(GameObject oldPlayer)
    {
        respawnScript.respawn(oldPlayer);   
    }

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerID = "Player " + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;
    }
    
    public static void UnRegisterPlayer(string name)
    {
        players.Remove(name);
    }

    public static Player GetPlayer (string ID)
    {
        return players[ID];
    }

    void OnGUI ()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            GUILayout.BeginArea(new Rect(20, 20, 200, 500));
            GUILayout.BeginVertical();

            foreach (string s in players.Keys)
            {
                GUILayout.Label(s + "  -  " + players[s].transform.name);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
