using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessing : MonoBehaviour
{
    PostProcessVolume volume;
    Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = gameObject.GetComponent<PostProcessVolume>();
        vignette = volume.profile.GetSetting<Vignette>();
        vignette.intensity.Override(1f);
        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, vignette);
        vignette.intensity.value = 0.5f;

    }

// Update is called once per frame
void Update()
    {
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, Random.Range(0.5f,0.8f), Time.deltaTime);
    }
}
