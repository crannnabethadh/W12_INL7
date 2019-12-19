using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitPlayer : MonoBehaviour
{
    [SerializeField] Image characterImage;
    [SerializeField] Text characterText;

    // Start is called before the first frame update
    void Start()
    {
        Sprite sprite = Resources.Load<Sprite>("Images/" + PlayerPrefs.GetInt("imageIndex"));
        characterImage.GetComponent<Image>().sprite = sprite;
        characterText.text = PlayerPrefs.GetString("playerName");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
