using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
	private float speed = 10f;

	[SerializeField]
	private float lookSensitivity = 10f;

    private PlayerMotor motor;

    void Start ()
	{
		motor = GetComponent<PlayerMotor>();
	}

    void Update() {
		if (PauseGame.IsOn)
			return;

        //Calculate movement velocity as a 3D vector
		// float _xMov = Input.GetAxis("Horizontal");
		float _zMov = Input.GetAxis("Vertical");

		// Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		// Final movement vector
		Vector3 _velocity = _movVertical * speed;

        //Apply movement
		motor.Move(_velocity);

		float _yRot = Input.GetAxis("Horizontal");
		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		motor.Rotate(_rotation);
    }
}
