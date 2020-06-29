using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class SwipeControls : MonoBehaviour
{


    private Touch touch;
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 1.0f;
    public float ySpeed = 2.0f;

    public float yMinLimit = 10f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;



    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        if (tower != null)
        {
            target = tower.transform;
        }
    }

    void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);

            if (target && touch.phase == TouchPhase.Moved)
            {
                x += touch.deltaPosition.x * xSpeed * distance * 0.02f;
                y -= touch.deltaPosition.y * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diffrence = currentMagnitude - prevMagnitude;

            distance = Mathf.Clamp(distance - diffrence * 0.01f, distanceMin, distanceMax);



        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}