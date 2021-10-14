using UnityEngine;
using UnityEngine.SceneManagement;

public class FlightControl : MonoBehaviour
{
    public float speed = 10f;
    public float rollSpeed = 100f;
    public float pitchSpeed = 100f;
    public float maxAltitude = 500f;

    float pitch, roll;

    void Update() {

        roll = Input.GetAxisRaw("Horizontal");
        pitch = Input.GetAxisRaw("Vertical");

        transform.Rotate( Vector3.back * roll * rollSpeed * Time.deltaTime, Space.Self );
        transform.Rotate( Vector3.right * pitch * pitchSpeed * Time.deltaTime, Space.Self );

        if ( Input.GetKey("space") ) {
            transform.Translate( Vector3.forward * speed * Time.deltaTime );
        }

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
