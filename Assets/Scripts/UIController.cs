using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class UIController : MonoBehaviour {
    //! Список сцен уровней
    private List<string> scenesList = new List<string> {"Level_select", "Level1", "Level2", "Level3"};
    ///
    /// @brief Метод открытия сцены выбора уровней
    ///
    public void StartScene() {
        SceneManager.LoadScene(scenesList[0]);
    }
    ///
    /// @brief Метод открытия сцены выбора уровней
    ///
    public void StartSceneBoss() {
        SceneManager.LoadScene("LevelBoss");
    }
    public void ExitLevels() {
        SceneManager.LoadScene("Menu");
    }
    ///
    /// @brief Метод закрытия настроек
    ///
    public void Options(GameObject window) {
        window.SetActive(true);
    }
    ///
    /// @brief Метод для кнопки выход из игры, выходит из игры
    ///
    public void Quit() {
        Application.Quit();
    }
    ///
    /// @brief Метод запускает выбранный уровень
    ///
    public void StartSceneLevel() {
        string btn_name = EventSystem.current.currentSelectedGameObject.name;
        int number = Int32.Parse(btn_name.Substring(btn_name.Length - 1));
        SceneManager.LoadScene(scenesList[number]);
    }
}
