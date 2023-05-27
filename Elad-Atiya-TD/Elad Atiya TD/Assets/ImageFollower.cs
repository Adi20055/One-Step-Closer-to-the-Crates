using UnityEngine;

public class ImageFollower : MonoBehaviour
{
    public float offsetDistance = 50f; // Distance between cursor and image

    private RectTransform rectTransform;
    private bool isFollowing = false;

    private void Start()
    {
        // Get the RectTransform component of the image
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isFollowing)
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Apply the offset to the x-coordinate
            mousePosition.x -= offsetDistance;

            // Set the position of the image to the adjusted mouse position
            rectTransform.position = mousePosition;
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}