using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_Handler : MonoBehaviour
{
    public bool brownChestOpened;
    public bool darkChestOpened;
    public bool blueChestOpened;
    public bool redChestOpened;
    [SerializeField] private GameObject _darkKey;
    [SerializeField] private GameObject _blueKey;
    [SerializeField] private GameObject _redKey;
    [SerializeField] private KeyHandler _keyHandler;
    private bool canPickKey;
    [SerializeField] private BoxCollider2D _brownCollider;
    [SerializeField] private BoxCollider2D _darkCollider;
    [SerializeField] private BoxCollider2D _blueCollider;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private GameObject[] _firePotions;
    private void Update()
    {
        if(brownChestOpened)
        {
            _darkKey.SetActive(true);
        }
        if(darkChestOpened && !_firePotions[0].GetComponent<FirePotion>().isAdded)
        {
            _blueKey.SetActive(true);
            _firePotions[0].SetActive(true);
        }
        if(blueChestOpened && !_firePotions[1].GetComponent<FirePotion>().isAdded)
        {
            _redKey.SetActive(true);
            _firePotions[1].SetActive(true);
        }
        if(redChestOpened)
        {
            PlayerController.obtainedPower = true;
            _playerController.GetComponent<Animator>().SetTrigger("PowerObtained");
            Invoke(nameof(DisablingPlayerSprite), 2.5f);

        }
        if(_darkKey.activeInHierarchy)
        {
            if(Input.GetKeyUp(KeyCode.E))
            {
                canPickKey= true;
                if(canPickKey)
                {
                    Invoke(nameof(PickKeyDark), 1f);
                }
            }
        }
        if(_blueKey.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                canPickKey = true;
                if (canPickKey)
                {
                    Invoke(nameof(PickKeyBlue), 1f);
                }
            }
        }
        if(_redKey.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                canPickKey = true;
                if (canPickKey)
                {
                    Invoke(nameof(PickKeyRed), 1f);
                }
            }
        }
    }
    private void PickKeyDark()
    {
        _keyHandler.hasBlackkey = true;
        _darkKey.GetComponentInChildren<SpriteRenderer>().enabled = false;
        _brownCollider.isTrigger = true;
        canPickKey = false;
    }
    private void PickKeyBlue()
    {
        _keyHandler.hasBluekey = true;
        _blueKey.GetComponentInChildren<SpriteRenderer>().enabled = false;
        _darkCollider.isTrigger = true;
        canPickKey = false;
    }
    private void PickKeyRed()
    {
        _keyHandler.hasRedkey = true;
        _redKey.GetComponentInChildren<SpriteRenderer>().enabled = false;
        _blueCollider.isTrigger = true;
        canPickKey = false;
    }
    private void DisablingPlayerSprite()
    {
        _playerController.GetComponent<SpriteRenderer>().enabled = false;
        gameWonPanel.SetActive(true);
    }

    private void OnDisable()
    {
        ClearStaticBool();
    }

    public static void ClearStaticBool()
    {
        SecondChestTrigger.canOnKarin = false;
        ThirdChestTrigger.canOnKarin = false;
        FourthChestTrigger.canOnBoss = false;
    }
}
