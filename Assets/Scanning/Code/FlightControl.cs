using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FlightControl : MonoBehaviour
{
    public float speed = 10f;
    public float maxAltitude = 150f;
    // HUD
    public GameObject altitudeNeedle;
    public GameObject signalsNeedle;
    
    [HideInInspector]
    public int signals_collected = 0;
    public int limit = 10;
    float limitAdjust;

    public FadeDriver fadeDriver;
    public FadeBanner winFadeBanner;
    public FadeBanner loseFadeBanner;

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

    void Start() {
        limitAdjust = ( 264f / limit );
    }

    void Update() {

        if (frozen) return;

        altitudeNeedle.transform.rotation = Quaternion.Euler( 0, 0, ( 238 - (transform.position.y * 1.881f) ) );
        signalsNeedle.transform.rotation = Quaternion.Slerp( 
            signalsNeedle.transform.rotation, 
            Quaternion.Euler( 0, 0, ( 223 - ( signals_collected * limitAdjust ) ) ), 
            hozSlerpSpped );

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
                    speed = 10f;
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
        winFadeBanner.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Exit());
    }

    IEnumerator ExitOnLose() {
        loseFadeBanner.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Exit());
    }

    IEnumerator Exit() {
        fadeDriver.TriggerFade();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }
}