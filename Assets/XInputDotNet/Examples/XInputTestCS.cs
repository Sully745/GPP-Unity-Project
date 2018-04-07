//using UnityEngine;
//using XInputDotNetPure; // Required in C#

//public class XInputTestCS : MonoBehaviour
//{
//    bool playerIndexSet = false;
//    PlayerIndex playerIndex;
//    GamePadState state;
//    GamePadState prevState;

//    // Use this for initialization
//    void Start()
//    {
//        // No need to initialize anything for the plugin
//    }

//    void FixedUpdate()
//    {
//        // SetVibration should be sent in a slower rate.
//        // Set vibration according to triggers
//        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Find a PlayerIndex, for a single player game
//        // Will find the first controller that is connected ans use it
//        if (!playerIndexSet || !prevState.IsConnected)
//        {
//            for (int i = 0; i < 4; ++i)
//            {
//                PlayerIndex testPlayerIndex = (PlayerIndex)i;
//                GamePadState testState = GamePad.GetState(testPlayerIndex);
//                if (testState.IsConnected)
//                {
//                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
//                    playerIndex = testPlayerIndex;
//                    playerIndexSet = true;
//                }
//            }
//        }
//    }
//}
