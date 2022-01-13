using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderText : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    void Update()
    {
        transform.GetComponent<TMP_Text>().text = slider.value.ToString();
    }
}
