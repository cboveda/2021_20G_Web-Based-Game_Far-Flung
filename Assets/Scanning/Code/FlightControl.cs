using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FlightControl : MonoBehaviour
{
    public float speed = 10f;
    public float maxAltitude = 150f;
    // HUD
    public Text altitude;
    public Text signals;
    int signals_collected = 0;
    public int limit = 10;

    public FadeDriver fadeDriver;
    public FadeBanner bannerFader;

    [Range(0,1)]
    public float hozSlerpSpped;
    [Range(0,1)]
    public float vertSlerpSpeed;

    public Vector3 rlTranslate = new Vector3(10, 0, 0);
    public Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    Quaternion bankRight = Quaternion.Euler(0, 30, -30);
    Quaternion bankLeft = Quaternion.Euler(0, -30, 30);
    Quaternion noseUp = Quaternion.Euler(-30, 0, 0);
    Quaternion noseDown = Quaternion.Euler(30, 0, 0);
    Quaternion noQuat = Quaternion.Euler(0, 0, 0);

    bool frozen = false;

    Collider prevCollision;

    void Update() {

        if (frozen) return;

        altitude.text = Mathf.RoundToInt(transform.position.y).ToString();
        signals.text = signals_collected.ToString() + "/" + limit.ToString();

        float roll  = Input.GetAxis("Horizontal");
        float pitch = Input.GetAxis("Vertical");

        if ( roll > 0 ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, bankRight, hozSlerpSpped );
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + rlTranslate, ref velocity, smoothTime);
        } else if ( roll < 0 ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, bankLeft, hozSlerpSpped );
            transform.position = Vector3.SmoothDamp(transform.position, transform.position - rlTranslate, ref velocity, smoothTime);
        } else {
            transform.rotation = Quaternion.Slerp( transform.rotation, noQuat, hozSlerpSpped );
        }

        if ( pitch < 0 && ( transform.position.y < maxAltitude ) ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, noseUp, vertSlerpSpeed );
        } else if ( pitch > 0 ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, noseDown, vertSlerpSpeed );
        } else {
            transform.rotation = Quaternion.Slerp( transform.rotation, noQuat, vertSlerpSpeed );
        }

        transform.Translate( Vector3.forward * speed * Time.deltaTime ); 
    }

    void OnTriggerEnter( Collider collider ) {

        if ( collider.gameObject.CompareTag("NeutronSignal") ) {
            Debug.Log("Hit Signal");

            if ( collider != prevCollision ) {
                signals_collected++;

                if ( signals_collected >= limit ) {
                    frozen = true;
                    StartCoroutine(ExitOnWin());
                }
            }
            prevCollision = collider;

        } else {
            Debug.Log("Terrain Collision!");
            frozen = true;
            StartCoroutine(ExitOnLose());
        }        
    }

    IEnumerator ExitOnWin() {
        bannerFader.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ExitOnLose());
    }

    IEnumerator ExitOnLose() {
        fadeDriver.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }
}