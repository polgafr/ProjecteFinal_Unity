var car : Transform;
var smoothSpeed : float = 40;

var lookSmoothDamp : float = 0.1;

 var yRotation : float;
 var xRotation : float;
 
 var yRotationVel : float;
 var xRotationVel : float;
 
 var carRotation : float;
 
 function CameraInhead(){
 
     carRotation = car.transform.position.y;
 
     yRotation += Input.GetAxis("Mouse X");
     xRotation -= Input.GetAxis("Mouse Y");
     
     xRotation = Mathf.Clamp(xRotation, -60, 60);
     yRotation = Mathf.SmoothDamp(yRotation, carRotation, yRotationVel, Time.deltaTime * smoothSpeed);
 
     transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
 
 }