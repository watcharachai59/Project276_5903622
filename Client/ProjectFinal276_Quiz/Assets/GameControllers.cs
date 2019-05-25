using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllers : MonoBehaviour
{
    static SocketIOComponent socket;
    public GameObject panelbung1;
    public GameObject panelbung2;
    public GameObject panelbung3;


    public  Text Subject;
    public  int selectchoi;
    public Text textout;
    public Text textin;
    public Text textScore;
    public Text Textend;

    public GameObject StartCanvas;
    public InputField getname;

    public Text[] name;
    public Text[] status;


    public GameObject panelQ1;
    public GameObject panelQ2;
    public GameObject panelQ3;

    public GameObject alltext;
    public GameObject scoreprefab;
    int scoregame;

    void Start()
    {
        panelbung1.SetActive(false);
        panelbung2.SetActive(false);
        panelbung3.SetActive(false);
        panelQ1.SetActive(true);
        alltext.SetActive(true);
        panelQ2.SetActive(false);
        panelQ3.SetActive(false);

        scoregame = 0;


        StartCanvas.SetActive(true);
        selectchoi = 0;
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("True", True);
        socket.On("False", False);
        socket.On("login", Login);
        socket.On("Next2", Next2);
        socket.On("Next3", Next3);
        socket.On("score", score);
        socket.On("NextEnd", NextEnd);
        socket.On("SendScore", SendScores);
    }


    void Update()
    {
        textScore.text = scoregame.ToString();
    }


    public void load()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Login(SocketIOEvent e)
    {
        PlayerLogin a = PlayerLogin.CreateFromJSON(e.data.str);
        if (a.count == 1)
        {
            name[0].text = getname.text;
            
        }
  
    }


    void score(SocketIOEvent e)
    {
        scoregame++;
    }
    void SendScores(SocketIOEvent e)
    {
        string data = e.data.ToString();
        print(data);
        PlayerLogin userHealthJSON = PlayerLogin.CreateFromJSON(data);
        GameObject ShowScorePre = Instantiate(scoreprefab);
        ShowScorePre.transform.SetParent(GameObject.Find("ScorePanels").transform);
        ShowScorePre.transform.localScale = new Vector3(1, 1, 1);
        ShowScorePre.GetComponent<Player>().setname(userHealthJSON.names, userHealthJSON.count.ToString());

       
    
    }
    void invokNextEnd()
    {
        panelQ3.SetActive(false);
        alltext.SetActive(false);

    
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("Textend", scoregame);
        socket.Emit("SendScore", jsonObject);

    }
    void NextEnd(SocketIOEvent e)
    {
        Invoke("invokNextEnd", 1f);
    }



    void invokeNext2()
    {
        panelQ2.SetActive(true);
        panelQ1.SetActive(false);
        selectchoi = int.Parse(textin.text);
        textin.text = "";
        textout.text = "";
    }

    void Next2(SocketIOEvent e)
    {
        Invoke("invokeNext2", 1f);
    }



    void invokeNext3()
    {
        panelQ2.SetActive(false);
        panelQ3.SetActive(true);
        selectchoi = int.Parse(textin.text);
        textin.text = "";
        textout.text = "";
    }


    void Next3(SocketIOEvent e)
    {
        Invoke("invokeNext3", 1f);

        
    }

    void True(SocketIOEvent e)
    {
        textout.text = ("ตอบถูก");

    }
 
    void False(SocketIOEvent e)
    {
        textout.text = ("ตอบผิด");
    }



    public void choi1()
    {
       // panelChoi.SetActive(true);
        textin.text = "1";
       /* selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send", jsonObject);*/
        

    }
    public void choi2()
    {
       // panelChoi.SetActive(true);
        textin.text = "2";
       /* selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send", jsonObject);*/
        

    }
    public void choi3()
    {
       // panelChoi.SetActive(true);
        textin.text = "3";
      /*  selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send", jsonObject);*/
        
    }
    public void choi4()
    {
     //   panelChoi.SetActive(true);
        textin.text = "4";
      /*  selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send", jsonObject);*/
        
    }






    void OnConnected(SocketIOEvent e)
    {
        print("HasConnect");
          PlayerInfo add = new PlayerInfo();
          add.name = "Name1";
          add.lives = 999;

          string json = JsonUtility.ToJson(add);

          PlayerInfo a = PlayerInfo.CreateFromJSON(json);
          print(a.name );
          print(a.lives);

    }

    public void SentChoi1()
    {
        panelbung1.SetActive(true);
        selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send1", jsonObject);
        socket.Emit("s1", jsonObject);
    }
    public void SentChoi2()
    {
        panelbung2.SetActive(true);
        selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send2", jsonObject);
        socket.Emit("s2", jsonObject);
    }
    public void SentChoi3()
    {
        panelbung3.SetActive(true);
        selectchoi = int.Parse(textin.text);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.NUMBER);
        jsonObject.AddField("textin", selectchoi);
        socket.Emit("send3", jsonObject);
        socket.Emit("s3", jsonObject);
    }

}


[System.Serializable]
public class PlayerInfo
{
    public string name;
    public int lives;


    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }


}

[System.Serializable]
public class PlayerLogin
{
    public string names;
    public int count;


    public static PlayerLogin CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerLogin>(jsonString);
    }


}