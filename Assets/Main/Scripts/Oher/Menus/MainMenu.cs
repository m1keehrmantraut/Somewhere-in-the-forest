using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;
    
    
    public void PlayGame()
    {
        StartCoroutine(Asynchronously());
    }

    IEnumerator Asynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game");
        
        progressBar.gameObject.SetActive(true);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressBar.value = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
