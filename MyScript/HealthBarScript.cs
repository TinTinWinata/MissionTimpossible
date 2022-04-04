using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] PlayerScript player;
    private Slider healthSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = player.getHealth();
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
