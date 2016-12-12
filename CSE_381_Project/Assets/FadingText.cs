using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingText : MonoBehaviour {

	public Text headerText;
	public Text chapterText;
	float deltaAlpha;
	bool fadeInComplete;
	bool fadeOut;
	bool fadeOutComplete;
	public float fadeDuration;
	float trueFadeDuration;
	Color fadeColor;
	public float textStayDuration;
	public float delayBeforeStart;
    public bool active = true;
	// Use this for initialization
	void Start () {
        GameManager.instance.levelText = this;
		fadeColor = headerText.color;
		fadeColor.a = 0;
		headerText.color = fadeColor;
		chapterText.color = fadeColor;
		trueFadeDuration = 1.0f / fadeDuration;

	}
	
	// Update is called once per frame
	void Update () {
        if (active) { 
		    if (fadeOutComplete && fadeInComplete) {
			    Destroy (this);
		    }

		    if (delayBeforeStart > 0) {
			    delayBeforeStart -= Time.deltaTime;
		    }else {
			    if (!fadeInComplete) {
				    deltaAlpha = trueFadeDuration * Time.deltaTime;	
				    if (deltaAlpha + fadeColor.a >= 1.0f) {
					    fadeColor = headerText.color;
					    fadeColor.a = 1.0f;
					    fadeInComplete = true;
				    } else {
					    fadeColor = headerText.color;
					    fadeColor.a += deltaAlpha;
				    }
				    headerText.color = fadeColor;
				    chapterText.color = fadeColor;
			    }

			    if (fadeInComplete && textStayDuration > 0) {
				    textStayDuration -= Time.deltaTime;
				    if (textStayDuration <= 0)
					    fadeOut = true;
			    }

			    if (fadeOut) {
				    deltaAlpha = trueFadeDuration * Time.deltaTime;	
				    if (fadeColor.a - deltaAlpha <= 0.0f) {
					    fadeColor = headerText.color;
					    fadeColor.a = 0.0f;
					    fadeOutComplete = true;
				    } else {
					    fadeColor = headerText.color;
					    fadeColor.a -= deltaAlpha;
				    }
				    headerText.color = fadeColor;
				    chapterText.color = fadeColor;
			    }
		    }

        }
    }
}