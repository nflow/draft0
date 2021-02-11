using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public float progress {
        get => slider.value;
        set {
            slider.value = value;
        }
    }

    void LateUpdate() {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
