using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public void StartScene() {
        SceneManager.LoadScene("Level1");
    }
    public void Options(GameObject window) {
        window.SetActive(true);
    }
    public void Quit() {
        Application.Quit();
    }
}
