using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using SocketIO;

public class NetworkManager : MonoBehaviour
{

    public static NetworkManager instance;
    public GameObject canvas;
    public SocketIOComponent socket;
    public InputField playerNameInput;
    public GameObject player;

    public GameObject panelbungbtnstart;


    [SerializeField]
    Transform PlayerPanel;

    [SerializeField]
    GameObject Lobby;

    void Awake()
    {
        panelbungbtnstart.SetActive(false);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        // subscribe to all the various websocket events

        panelbungbtnstart.SetActive(false);
        socket.On("other player connected", OnOtherPlayerConnected);
        socket.On("play", OnPlay);
        socket.On("status", UpdateStatus);
        socket.On("startgame", Startgame);
        socket.On("other player disconnected", OnOtherPlayerDisconnect);
    }

    public void JoinGame()
    {
        StartCoroutine(ConnectToServer());
        panelbungbtnstart.SetActive(true);
    }

    void Startgame(SocketIOEvent socketIOEvent)
    {

        print("startgame");
        Lobby.SetActive(false);
    }
    public void ready()
    {
        PlayerJSON statusonly = new PlayerJSON("", "Ready");
        string data = JsonUtility.ToJson(statusonly);
        socket.Emit("ready", new JSONObject(data));
         
    }
    #region Commands

    IEnumerator ConnectToServer()
    {
        yield return new WaitForSeconds(0.5f);

        socket.Emit("player connect");

        yield return new WaitForSeconds(1f);

        string playerName = playerNameInput.text;
       
        PlayerJSON playerJSON = new PlayerJSON(playerName, "Standby");
        string data = JsonUtility.ToJson(playerJSON);
        socket.Emit("play", new JSONObject(data));
        canvas.gameObject.SetActive(false);
    }

    #endregion

    #region Listening

    void OnPlay(SocketIOEvent socketIOEvent)
    {
        print("you joined");
        string data = socketIOEvent.data.ToString();
        UserJSON currentUserJSON = UserJSON.CreateFromJSON(data);
   

        GameObject p = Instantiate(player) as GameObject;
        Player pc = p.GetComponent<Player>();
        pc.transform.SetParent(PlayerPanel);
        pc.transform.localScale = new Vector3(1, 1, 1);
        pc.setname(currentUserJSON.name, currentUserJSON.status);
       
        pc.isLocalPlayer = true;
        p.name = currentUserJSON.name;

    }

    void UpdateStatus(SocketIOEvent socketIOEvent)
    {

        string data = socketIOEvent.data.ToString();
        UserJSON userJSON = UserJSON.CreateFromJSON(data);
         // if it is the current player exit
    /*    if (userJSON.name == playerNameInput.text)
        {

            return;
        }*/
        GameObject p = GameObject.Find(userJSON.name) as GameObject;
        if (p != null)
        {
             
            Player pc = p.GetComponent<Player>();
            pc.setstatus(userJSON.status);
        }

        

    }

    void OnOtherPlayerConnected(SocketIOEvent socketIOEvent)
    {
        print("Someone else joined");
        string data = socketIOEvent.data.ToString();
        UserJSON userJSON = UserJSON.CreateFromJSON(data);
        
        GameObject o = GameObject.Find(userJSON.name) as GameObject;
        if (o != null)
        {
            return;
        }
        GameObject p = Instantiate(player) as GameObject;
        p.transform.SetParent(PlayerPanel);
        p.transform.localScale = new Vector3(1, 1, 1);
        // here we are setting up their other fields name and if they are local
        Player pc = p.GetComponent<Player>();
        pc.setname(userJSON.name, userJSON.status);
     
        pc.isLocalPlayer = false;
        p.name = userJSON.name;
   
    }
    
  
    void OnOtherPlayerDisconnect(SocketIOEvent socketIOEvent)
    {
        print("user disconnected");
        string data = socketIOEvent.data.ToString();
        UserJSON userJSON = UserJSON.CreateFromJSON(data);
        Destroy(GameObject.Find(userJSON.name));
    }

    #endregion

    #region JSONMessageClasses

    [Serializable]
    public class PlayerJSON
    {
        public string name;
        public string status;
        public PlayerJSON(string _name, string _status)
        {
            
            name = _name;
            status = _status;
        }
    }
 
 
    [Serializable]
    public class UserJSON
    {
        public string name;
        public string status;

        public static UserJSON CreateFromJSON(string data)
        {
            return JsonUtility.FromJson<UserJSON>(data);
        }
    }

 
   
  
    #endregion
}
