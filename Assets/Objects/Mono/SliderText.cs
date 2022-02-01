using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    void Update()
    {
        transform.GetComponent<TMP_Text>().text = slider.value.ToString();
    }
}
