using UnityEngine;

public class ImageFollowCursor : MonoBehaviour
{
    [HideInInspector]
    public static ImageFollower selectedFollower;
    public ImageFollower associatedFollower;

    private void Start()
    {
        // Makes sure the associated follower is disabled at the start
        associatedFollower.gameObject.SetActive(false);
        selectedFollower = null;
    }

    private void Update()
    {
        if (associatedFollower != selectedFollower)
        {
            associatedFollower.StopFollowing();
            associatedFollower.gameObject.SetActive(false);
            return;
        }

        if (selectedFollower != null)
        {
            selectedFollower.StartFollowing();
            associatedFollower.gameObject.SetActive(true);
        }
    }

    public void SelectImage()
    {
        if (selectedFollower != null)
        {
            selectedFollower.StopFollowing();
            associatedFollower.gameObject.SetActive(false);
        }

        selectedFollower = associatedFollower;
        associatedFollower.StartFollowing();
        associatedFollower.gameObject.SetActive(true);
    }

    public static void DeselectAllImages()
    {
        if (selectedFollower != null)
        {
            selectedFollower.StopFollowing();
            selectedFollower.gameObject.SetActive(false);
            selectedFollower = null;
        }
    }
}
