
using System.Collections;
using TMPro;
using UnityEngine;
public class KeyHandler : MonoBehaviour
{
    public bool hasBrownKey;
    public bool hasBlackkey;
    public bool hasBluekey;
    public bool hasRedkey;

    [SerializeField] private Sprite[] texture;
    [SerializeField] private SpriteRenderer[] _sprites;
    [SerializeField] private TextMeshProUGUI _collectedText;
    [SerializeField] int i = 0;
    bool notChecked;
    private void Update()
    {
        if (hasBrownKey &&  i == 0)
        {
            _sprites[0].sprite = texture[0];
            _collectedText.text = "BROWN KEY COLLECTED";
            StartCoroutine(TextEnabling());
        }
        if (hasBlackkey && i==1)
        {
            _sprites[1].sprite = texture[1];
            _collectedText.text = "BLACK KEY COLLECTED";
            StartCoroutine(TextEnabling());
        }
        if (hasBluekey && i==2)
        {
            _sprites[2].sprite = texture[2];
            _collectedText.text = "BLUE KEY COLLECTED";
            StartCoroutine(TextEnabling());
        }
        if (hasRedkey && i==3)
        {
            _sprites[3].sprite = texture[3];
            _collectedText.text = "RED  KEY COLLECTED";
            StartCoroutine(TextEnabling());
        }
    }
    IEnumerator TextEnabling()
    {  
        notChecked = true;
        _collectedText.enabled = true;
        yield return new WaitForSeconds(1.2f);
        _collectedText.enabled = false;
        if (i == 0 && notChecked)
        {
            i = 1;
            notChecked = false;
        }
        if (i == 1 && notChecked)
        {
            i = 2;
            notChecked = false;
        }
        if (i == 2 && notChecked)
        {
            i = 3;
            notChecked = false;
        }
        if (i == 3 && notChecked)
        {
            i = 4;
            notChecked = false;
        }
    }
}
