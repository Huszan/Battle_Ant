using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    void Update()
    {
        //Give slider value to text GameObject
        GetComponentInChildren<TMP_Text>().text = 
            GetComponent<Slider>().value.ToString();
    }
}
