using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    [SerializeField] Image profileImage;
    [SerializeField] InputField playerNameInputField;
    int imageIndex = 1;
    const int totalNumberOfImages = 4;
    List<int> generatedImages = new List<int>();

    private void Start()
    {
        if (PlayerPrefs.HasKey("imageIndex"))
        {
            PlayerPrefs.DeleteKey("imageIndex");
        }
        PlayerPrefs.SetInt("imageIndex", imageIndex);
    }

    // Binded to Load Random Image Button On Click Event
    public void LoadRandomImage()
    {
        if (generatedImages.Count < totalNumberOfImages)
        {
            do
            {
                imageIndex = Random.Range(1, 5);
            }
            while (generatedImages.Contains(imageIndex));

            generatedImages.Add(imageIndex);
            Sprite sprite = Resources.Load<Sprite>("Images/" + imageIndex);
            profileImage.GetComponent<Image>().sprite = sprite; 
        }
    }

    // Binded to Load Next Scene Button On Click Event
    public void LoadNextScene()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            playerNameInputField.placeholder.GetComponent<Text>().text = "Player name can NOT be empty";
            playerNameInputField.placeholder.GetComponent<Text>().color = Color.red;
        }
        else
        {
            if (PlayerPrefs.HasKey("imageIndex"))
            {
                PlayerPrefs.DeleteKey("imageIndex");
            }
            PlayerPrefs.SetInt("imageIndex", imageIndex);
            PlayerPrefs.SetString("playerName", playerNameInputField.text);
            SceneManager.LoadScene(1);
        }
    }

}
