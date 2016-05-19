using UnityEngine;

namespace Assets.Graphic
{
    public class CameraController : MonoBehaviour
    {
        public float Speed = 4.5f;
        public float MinX = -360.0f;
        public float MaxX = 360.0f;

        public float MinY = -90.0f;
        public float MaxY = 90.0f;

        public float SensX = 500.0f;
        public float SensY = 500.0f;

        private float _rotationY;
        private float _rotationX;
        // Use this for initialization
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-Speed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(new Vector3(0, -Speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, Speed * Time.deltaTime, 0));
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -Speed * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));
            }
            if (Input.GetMouseButton(0))
            {
                _rotationX += Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
                _rotationY += Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;
                _rotationY = Mathf.Clamp(_rotationY, MinY, MaxY);
                transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0);
            }
        }
    }
}
