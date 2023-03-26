using UnityEngine;

public class swipeDetector : MonoBehaviour
{
    // Variables to keep track of the position of the finger when swiping
    private Vector2 fingerDown;
    private Vector2 fingerUp;

    // Set this to true if you only want to detect swipes after the finger is lifted
    private bool detectSwipeOnlyAfterRelease = false;

    // Variables to store whether a swipe was detected and in which direction
    public static bool swipeLeft;
    public static bool swipeRight;
    public static bool swipeUp;

    // The minimum distance the finger needs to move in order to register a swipe
    [SerializeField] private float SWIPE_THRESHOLD = 20f;

    // The minimum distance the finger needs to move in order to not register a tap
    [SerializeField] private float TAP_THRESHOLD = 10f;

    void Update()
    {
        // Loop through all touches that are currently on the screen
        foreach (Touch touch in Input.touches)
        {
            // If the touch just started, store its position as the finger down position
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            // If we're detecting swipes even while the finger is moving
            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                // Update the finger down position to the current position
                fingerDown = touch.position;

                // Check if a swipe has occurred
                DetectSwipe();
            }

            // If the finger was lifted, check if a swipe has occurred
            if (touch.phase == TouchPhase.Ended)
            {
                // Store the finger down position as the current position
                fingerDown = touch.position;

                // Check if a swipe has occurred
                DetectSwipe();
            }
        }
    }

    // Check if a swipe has occurred
    void DetectSwipe()
    {
        // Check if the finger has moved enough distance to be considered a swipe
        if (SwipeDistanceCheck())
        {
            // Check if the swipe was in the left or right direction
            if (fingerDown.x - fingerUp.x > 0)
            {
                // Set swipeLeft to false and swipeRight to true to indicate a right swipe
                swipeLeft = false;
                swipeRight = true;
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                // Set swipeLeft to true and swipeRight to false to indicate a left swipe
                swipeLeft = true;
                swipeRight = false;
            }

            // Check if the swipe was in the up direction
            else if (fingerDown.y - fingerUp.y > 0)
            {
                // Set swipeUp to true to indicate an up swipe
                swipeUp = true;
            }

            // Store the finger down position as the finger up position for the next swipe
            fingerUp = fingerDown;
        }
        else
        {
            // If a swipe hasn't occurred, set all swipe variables to false
            swipeLeft = false;
            swipeRight = false;
            swipeUp = false;
        }
    }

    // Check if the distance between the finger down position and finger up position is greater than the swipe threshold
    bool SwipeDistanceCheck()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x) > SWIPE_THRESHOLD || Mathf.Abs(fingerDown.y - fingerUp.y) > SWIPE_THRESHOLD;
    }
}
