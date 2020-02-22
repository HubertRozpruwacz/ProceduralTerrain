using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Physics
{
    public class Phisics : MonoBehaviour
    {
        [SerializeField, Range(0f, 100f)]
        float maxSpeed = 10f;
        [SerializeField, Range(0f, 100f)]
        float maxAcceleration = 10f;

        Rigidbody rigid;
        Vector3 velocity;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            velocity = rigid.velocity;
        }
        
        private void Update()
        {
            Vector2 playerInput;
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.y = Input.GetAxis("Vertical");

            Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

            float maxSpeedChange = maxAcceleration * Time.deltaTime;

            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

            playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        }

    }
}
