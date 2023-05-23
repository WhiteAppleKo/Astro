using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ASyncLoader : MonoBehaviour

{      public Slider slider;
public TextMeshProUGUI progressText;
        public GameObject GameLoading;
              
    public void LoadLevel(int sceneIndex)
    {
    
        
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    // Start is called before the first frame update
   IEnumerator LoadAsynchronously(int sceneIndex)
    
   {
    
    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    GameLoading.SetActive(true);
    
    while(!operation.isDone)
    {
        float progress = Mathf.Clamp01(operation.progress / 0.9f);
        slider.value = progress;
        progressText.text = progress * 100f + "%";
        yield return null;
    }
     

   }
}
