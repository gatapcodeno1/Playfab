using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class testnow : MonoBehaviour
{
    [SerializeField] protected TMP_InputField text;
    public TMP_InputField Text => text;
    private static testnow instance;

    public static testnow Instance { get => instance; }

    private void Reset()
    {
        this.text = transform.parent.GetComponent<TMP_InputField>();
    }

    public void Awake()
    {
        testnow.instance = this;
    }

    public string Get()
    {
        return this.text.text;
    }
}
