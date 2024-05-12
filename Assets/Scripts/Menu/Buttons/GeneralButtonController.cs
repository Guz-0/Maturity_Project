using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GeneralButtonController : MonoBehaviour
{

    

    public float scaleFrom = 0f;
    public float scaleTo = 1f;
    public float scaleDuration = 1f;
    public float scaleDelay = 0f;

    public float yOffset = 10f;
    public float yOffsetDuration = 0.5f;

    public float pointerEnterScaleOffset = 0.1f;
    public float pointerEnterScaleOffsetDuration = 0.2f;

    private bool initialScaleCompleted = false;

    private Button button;

    private float fromYPosition;
    private float endYPosition;

    private Vector3 fromScale;
    private Vector3 endScale;

    public GameObject leftImage;
    public GameObject rightImage;

    public TMP_Text textToFade;

    public TextController myTextController;

    

    void Start()
    {
        transform.localScale = new Vector3(scaleFrom, scaleFrom, scaleFrom);

        button = GetComponent<Button>();
        button.enabled = false;

        StartCoroutine(ScaleButton());

        leftImage.SetActive(false);
        rightImage.SetActive(false);

        myTextController = GetComponent<TextController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ScaleButton()
    {
        yield return new WaitForSeconds(scaleDelay);
        transform.DOScale(scaleTo, scaleDuration).onComplete = OnScaleCompleted;
        /* ADD TEXT FADE
        textToFade.CrossFadeAlpha(1f, 1f,false);
        textToFade. */
        yield return null;
    }

    private void OnScaleCompleted()
    {
        initialScaleCompleted = true;

        if (!myTextController.enabled){
            button.enabled = true;
        }

        fromYPosition = transform.position.y;
        endYPosition = transform.position.y + yOffset;

        fromScale = transform.localScale;
        endScale = transform.localScale + new Vector3(pointerEnterScaleOffset, pointerEnterScaleOffset, pointerEnterScaleOffset);
    }

    public void OnPointerEnter()
    {
        if (initialScaleCompleted)
        {
            transform.DOMoveY(endYPosition, yOffsetDuration);
            transform.DOScale(endScale, pointerEnterScaleOffsetDuration);

            leftImage.SetActive(true);
            rightImage.SetActive(true);
        }
    }

    public void OnPointerExit()
    {
        if (initialScaleCompleted)
        {
            transform.DOMoveY(fromYPosition, yOffsetDuration);
            transform.DOScale(fromScale, pointerEnterScaleOffsetDuration);
            leftImage.SetActive(false);
            rightImage.SetActive(false);
        }
    }

}
