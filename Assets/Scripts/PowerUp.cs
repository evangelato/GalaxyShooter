using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpId; // 0 = Triple Shot, 1 = Speed, 2 = Shields
    [SerializeField]
    private AudioClip _audioClip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * _speed);

        if (transform.position.y < -4.5f) 
        {
            Destroy(this.gameObject);
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            if (player != null) 
            {
                switch(_powerUpId){
                    case 0:
                        player.ActivateTriplePowerUp();
                        break;
                    case 1:
                        player.ActivateSpeedPowerUp();
                        break;
                    case 2:
                        player.ActivateShieldPowerUp();
                        break;
                    default:
                        Debug.Log("Default Power Up Case");
                        return;
                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
