using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    void Update()
    {
        //Give slider value to text GameObject
        GetComponentInChildren<TMPro.TMP_Text>().text = 
            GetComponent<Slider>().value.ToString();
    }
}
