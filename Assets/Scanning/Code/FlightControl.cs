using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightControl : MonoBehaviour
{
    public float speed = 10f;
    public float rollSpeed = 100f;
    public float pitchSpeed = 100f;
    public float maxAltitude = 500f;

    [Range(0,1)]
    public float hozSlerpSpped;
    [Range(0,1)]
    public float vertSlerpSpeed;

    public Vector3 rlTranslate = new Vector3(10, 0, 0);

    Quaternion bankRight = Quaternion.Euler(0, 30, -30);
    Quaternion bankLeft = Quaternion.Euler(0, -30, 30);
    Quaternion noseUp = Quaternion.Euler(-30, 0, 0);
    Quaternion noseDown = Quaternion.Euler(30, 0, 0);
    Quaternion noQuat = Quaternion.Euler(0, 0, 0);
    
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;


    void Update() {

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

        if ( pitch > 0 ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, noseUp, vertSlerpSpeed );
        } else if ( pitch < 0 ) {
            transform.rotation = Quaternion.Slerp( transform.rotation, noseDown, vertSlerpSpeed );
        } else {
            transform.rotation = Quaternion.Slerp( transform.rotation, noQuat, vertSlerpSpeed );
        }

        transform.Translate( Vector3.forward * speed * Time.deltaTime ); 
        
        if ( transform.position.y > maxAltitude ) {
            Debug.Log("Leaving orbit");
            ExitScene();
        }
    }

    void OnTriggerEnter( Collider collider ) {

        if ( collider.gameObject.CompareTag("NeutronSignal") ) {
            Debug.Log("Hit Signal");

        } else {
            Debug.Log("Terrain Collision!");
            ExitScene();
        }        
    }

    void ExitScene() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }

}
