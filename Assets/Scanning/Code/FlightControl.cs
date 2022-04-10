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
    public int limit = 20;
    float limitAdjust;

    public FadeDriver fadeDriver;
    public FadeBanner winFadeBanner;
    public FadeBanner loseFadeBanner;

    [Range(0,1)]
    public float SlerpSpeed;

    public Vector3 rlTranslate = new Vector3(10, 0, 0);
    public Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;

    bool frozen = false;

    Collider prevCollision;

    void Start() {
        limitAdjust = ( 264f / limit );
    }

    void Update() {

        if (frozen) return; 

        altitudeNeedle.transform.rotation = Quaternion.Euler( 0, 0, ( 238 - (transform.position.y * 1.881f) ) );
        signalsNeedle.transform.rotation = Quaternion.Slerp( signalsNeedle.transform.rotation, 
                                                             Quaternion.Euler( 0, 0, ( 223 - ( signals_collected * limitAdjust ) ) ), 
                                                             SlerpSpeed );

        float roll  = Input.GetAxis("Horizontal");
        float pitch = Input.GetAxis("Vertical");

        Quaternion motion = Quaternion.Euler( ( 30 * pitch ), ( roll * 30), ( roll * -30) );

        transform.rotation = Quaternion.Slerp( transform.rotation, motion, SlerpSpeed );

        Vector3 target = new Vector3( transform.position.x + (10 * roll), 
                                      Mathf.Clamp(transform.position.y - (5 * pitch), 0, maxAltitude), 
                                      transform.position.z + speed );

        transform.position = Vector3.SmoothDamp( transform.position, target, ref velocity, smoothTime);
    }

    // Scoring and crashing
    void OnTriggerEnter( Collider collider ) {

        if ( collider.gameObject.CompareTag("NeutronSignal") ) {
            Debug.Log("Hit Signal");

            if ( collider != prevCollision ) {
                signals_collected++;

                if ( signals_collected >= limit ) {
                    speed = 1f;
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

    // Animations
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