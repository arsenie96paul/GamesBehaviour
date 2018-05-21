using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


    public void LoadScene(int level)
    {
        //Application.LoadLevel(level); <--- OUT OF DATE
        SceneManager.LoadScene(level);
    }
}
