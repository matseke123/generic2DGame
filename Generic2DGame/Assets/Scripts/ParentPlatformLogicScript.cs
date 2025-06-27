using UnityEngine;
using UnityEngine.Rendering;

public class ParentPlatformLogic : MonoBehaviour
{
    private Vector3 PreviousPosition;
    public Vector3 PlatformVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        PreviousPosition = transform.position;
    }
    private void Update()
    {
        PlatformVelocity = (transform.position - PreviousPosition) / Time.deltaTime;
        PreviousPosition = transform.position;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControllerScript>()._platform = gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControllerScript>()._platform = null;
        }
    }




}
