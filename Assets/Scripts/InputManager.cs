using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class InputManager : MonoBehaviour 
{
	/* ========= Input Config Constants ========= */
	private const int MAX_NUM_PLAYERS = 4;
	private const Player KB_PLAYER = Player.Player1;

	// Unity Tags. Must match tag of player!
	private static readonly string[] PLAYER_TAG_STRINGS = new string[] {
		"Done_Player1",
		"Done_Player2",
		"Done_Player3",
		"Done_Player4"
	};

	// relevant keyboard constants
//	private const string KB_USE = "Fire1";
//	private const string KB_ATTACK = "Fire2";
//	private const string KB_MOVEMENT_X = "KB_Move_Horizontal";
//	private const string KB_MOVEMENT_Y = "KB_Move_Vertical";
//	private const string KB_AIM_X = "KB_Aim_Horizontal";
//	private const string KB_AIM_Y = "KB_Aim_Vertical";

	// better keyboard constants
	private const KeyCode KB_USE = KeyCode.Space;
	private const KeyCode KB_ATTACK = KeyCode.X;
	private const KeyCode KB_MOVEMENT_LEFT = KeyCode.A;
	private const KeyCode KB_MOVEMENT_RIGHT = KeyCode.D;
	private const KeyCode KB_MOVEMENT_UP = KeyCode.W;
	private const KeyCode KB_MOVEMENT_DOWN = KeyCode.S;
	private const KeyCode KB_AIM_LEFT = KeyCode.LeftArrow;
	private const KeyCode KB_AIM_RIGHT = KeyCode.RightArrow;
	private const KeyCode KB_AIM_UP = KeyCode.UpArrow;
	private const KeyCode KB_AIM_DOWN = KeyCode.DownArrow;

	// Relevant Controller constants
	private const XboxButton CT_USE = XboxButton.A;
	private const XboxButton CT_SHOOT = XboxButton.RightBumper;
	private const XboxButton CT_ATTACK = XboxButton.B;
	private const XboxAxis CT_MOVEMENT_X = XboxAxis.LeftStickX;
	private const XboxAxis CT_MOVEMENT_Y = XboxAxis.LeftStickY;
	private const XboxAxis CT_AIM_X = XboxAxis.RightStickX;
	private const XboxAxis CT_AIM_Y = XboxAxis.RightStickY;

	/* ========= Important Implementation Types ========= */
	private enum Player {
		Player1,
		Player2,
		Player3,
		Player4,
		Noone
	}

	/* ========= Public Input Retrieval Methods ========= */

	public static bool GetShootButton(string playerTag) {
		return getButton (playerTag, KB_USE, CT_SHOOT);
	}

	public static bool GetUseButton(string playerTag) {
		return getButton (playerTag, KB_USE, CT_USE);
	}

	public static bool GetAttackButton(string playerTag) {
		return getButton (playerTag, KB_ATTACK, CT_ATTACK);
	}

	public static float GetMovementX(string playerTag) {
		return getAxis (playerTag, KB_MOVEMENT_LEFT, KB_MOVEMENT_RIGHT, CT_MOVEMENT_X);
	}

	public static float GetMovementY(string playerTag) {
		return getAxis (playerTag, KB_MOVEMENT_DOWN, KB_MOVEMENT_UP, CT_MOVEMENT_Y);
	}

	public static float GetAimX(string playerTag) {
		return getAxisSoft (playerTag, KB_AIM_LEFT, KB_AIM_RIGHT, CT_AIM_X);
	}

	public static float GetAimY(string playerTag) {
		return getAxisSoft (playerTag, KB_AIM_DOWN, KB_AIM_UP, CT_AIM_Y);
	}


	/* ========= Public Helper Methods ========= */

	public int GetNumPlayers() {
		int numCtrls = XCI.GetNumPluggedCtrlrs ();
		if (numCtrls == 0) {
			// due to keyboard input
			return 1;
		}
		if (numCtrls > MAX_NUM_PLAYERS) {
			return MAX_NUM_PLAYERS;
		}
		return numCtrls;
	}


	/* ========= Private Helper Methods ========= */

	// Old style input methods

	private static bool getButton(string playerTag, string kbBtn, XboxButton ctrlrBtn) {
		Player player = playerFromTag (playerTag);
		bool keyIn = false;
		if (player == KB_PLAYER) {
			keyIn = Input.GetButtonDown(kbBtn);
		}
		bool cntrlrIn = XCI.GetButtonDown (ctrlrBtn, ctrlrFromPlayer (player));
		return keyIn || cntrlrIn;
	}

	private static float getAxis(string playerTag, string kbAxis, XboxAxis ctrlrAxis) {
		Player player = playerFromTag (playerTag);
		// if the player is the keyboard player (and they're using the keyboard), 
		//  have that take precedence.
		if (player == KB_PLAYER) {
			float kbAxisVal = Input.GetAxis(kbAxis);
			if (kbAxisVal != 0.0f) {
				return kbAxisVal;
			}
		}
		// otherwise return the controller output
		return XCI.GetAxis(ctrlrAxis, ctrlrFromPlayer (player));
	}

	// New style input methods

	private static bool getButton(string playerTag, KeyCode kbBtn, XboxButton ctrlrBtn) {
		Player player = playerFromTag (playerTag);
		bool keyIn = false;
		if (player == KB_PLAYER) {
			keyIn = Input.GetKeyDown(kbBtn);
		}
		bool cntrlrIn = XCI.GetButtonDown (ctrlrBtn, ctrlrFromPlayer (player));
		return keyIn || cntrlrIn;
	}

	private static float getAxis(string playerTag, KeyCode kbKeyNeg, KeyCode kbKeyPos, XboxAxis ctrlrAxis) {
		return getAxisHelper (playerTag, kbKeyNeg, kbKeyPos, 1f, ctrlrAxis);
	}

	private static float getAxisSoft(string playerTag, KeyCode kbKeyNeg, KeyCode kbKeyPos, XboxAxis ctrlrAxis) {
		return getAxisHelper (playerTag, kbKeyNeg, kbKeyPos, 0.1f, ctrlrAxis);
	}

	private static float getAxisHelper(string playerTag, KeyCode kbKeyNeg, KeyCode kbKeyPos, float kbRange, XboxAxis ctrlrAxis) {
		Player player = playerFromTag (playerTag);
		// if the player is the keyboard player (and they're using the keyboard), 
		//  have that take precedence.
		if (player == KB_PLAYER) {
			bool kbIsNeg = Input.GetKey(kbKeyNeg);
			bool kbIsPos = Input.GetKey(kbKeyPos);
			if (kbIsNeg && kbIsPos) {
				return 0.0f;
			} else if (kbIsNeg && !kbIsPos) {
				return -kbRange;
			} else if (!kbIsNeg && kbIsPos) {
				return kbRange;
			}
		}
		// otherwise return the controller output
		return XCI.GetAxis(ctrlrAxis, ctrlrFromPlayer (player));
	}

	// returns a zero indexed player from the tag string
	private static Player playerFromTag(string playerTag) {
		for (int i = 0; i < PLAYER_TAG_STRINGS.Length; ++i) {
			if (playerTag == PLAYER_TAG_STRINGS[i]) {
				return (Player)i;
			}
		}
		throw new System.ArgumentException ("invalid player tag");
	}

	private static XboxController ctrlrFromPlayer(Player player) {
		switch (player) {
		case Player.Player1:
			return XboxController.First;
		case Player.Player2:
			return XboxController.Second;
		case Player.Player3:
			return XboxController.Third;
		case Player.Player4:
			return XboxController.Fourth;
		}
		throw new System.ArgumentException ("invalid Player");
	}
}
