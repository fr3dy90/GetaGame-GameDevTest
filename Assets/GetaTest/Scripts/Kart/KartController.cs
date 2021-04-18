using UnityEngine;

public class KartController : MonoBehaviour
{
   public Rigidbody rb;
   public Transform tr;

   public float driveForce;
   public float turnStrenght;
   public float offset_Y;

   private void Update()
   {
      Accelerate(Input.GetAxis("Vertical"));
      Turn(Input.GetAxis("Horizontal"));
   }

   void Accelerate(float accelInput)
   { 
      rb.AddForce(tr.forward*(accelInput*driveForce), ForceMode.Acceleration);
      tr.position = rb.position + (Vector3.up * offset_Y);
   }

   void Turn(float turnInput)
   {
      tr.rotation = Quaternion.Euler(tr.rotation.eulerAngles + new Vector3(0f, turnInput*turnStrenght*Time.deltaTime*Input.GetAxis("Vertical"), 0f));
   }
}
