using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBall : MonoBehaviour
{
    private bool hasDestination = false;
    private Vector2 _destination;
    [SerializeField] private float _speed;
    private bool isDestroyed = false;
    private bool blindDestructCalled = false;
    private AudioManager _audioManager;
    void Update()
    {
        if(!hasDestination)
        {
            _destination = PlayerController.isFacingRight? PlayerController.rightBlindEnd.position : PlayerController.leftBlindEnd.position;
            hasDestination = true;  
        }
        transform.position = Vector2.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
        if (transform.position.x == _destination.x)
        {
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            if (!blindDestructCalled)
            {
                Invoke(nameof(DisappearBlindDedect), 7f);
                blindDestructCalled = true;
            }
            BlindManager.blindDedect.SetActive(true);
            BlindManager. canCheck = true;
            GetComponent<SpriteRenderer>().enabled = false;
            if (isDestroyed) return;
            if (transform.position.x < GameObject.FindGameObjectWithTag("Player").transform.position.x)
            {
                
                BlindManager.RgtblindSpin.SetActive(true);
                _audioManager.PlayingBlindSFX();
                Invoke(nameof(R_DisappearSpin), 0.35f);
               
            }
            else
            {
                BlindManager.LftblindSpin.SetActive(true);
                Invoke(nameof(PlayingSFXblind), 0.01f);
                Invoke(nameof(L_DisappearSpin), 0.35f);
            }
            Invoke(nameof(DisappearPanel), 1.5f);
        }
    }
    private void L_DisappearSpin()
    {
        if (isDestroyed) return;
        BlindManager.LftblindSpin.SetActive(false);
        BlindManager.blindPanel.SetActive(true);
        isDestroyed = true;
    }
    private void R_DisappearSpin()
    {
        if (isDestroyed) return;
        BlindManager.RgtblindSpin.SetActive(false);
        BlindManager.blindPanel.SetActive(true);
        isDestroyed = true;
    }
    private void DisappearPanel()
    {
        BlindManager.blindPanel.SetActive(false);
    }
    private void DisappearBlindDedect()
    {
        BlindManager.blindDedect.SetActive(false);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        BlindManager.canCheck = false;
    }
    private void PlayingSFXblind()
    {
        _audioManager.PlayingBlindSFX();
    }

}
