using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private Vector3 startScale;

    [SerializeField] private float desiredEndScaleValue;
    [SerializeField] private float desiredTime;
    private float elapsedTime;
    private Vector3 endScale;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private GameObject playerObject;


    void Start()
    {
        startScale = transform.localScale;
        endScale = new Vector3(desiredEndScaleValue, desiredEndScaleValue, desiredEndScaleValue);
    }

    // Update is called once per frame
    void Update()
    {
        ExpandBomb();
    }

    void ExpandBomb()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredTime;
        transform.localScale = Vector3.Lerp(startScale, endScale, curve.Evaluate(percentageComplete));

        if (transform.localScale == endScale)
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        elapsedTime = 0f;
    }

    private void OnEnable()
    {
        transform.position = playerObject.transform.position;
    }

}
