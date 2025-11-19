using UnityEngine;

namespace Menu
{
    public class MenuController
    {
        GameObject menuRoot;
        float animationScale = 0f;
        float animationSpeed = 8f;
        bool visible = false;

        public UnityEngine.Renderer ButtonRenderer { get; private set; }

        MenuPanel panel;
        MenuButton disconnectButton;

        public void Initialize()
        {
            menuRoot = new GameObject("DisconnectMenuRoot");
            menuRoot.transform.localScale = Vector3.zero;
            menuRoot.SetActive(false);

            panel = new MenuPanel(menuRoot.transform);
            disconnectButton = new MenuButton(menuRoot.transform, "Disconnect", OnDisconnectPressed);

            ButtonRenderer = disconnectButton.Renderer;

            // default position in case player not yet present
            menuRoot.transform.position = Vector3.zero;
            menuRoot.transform.rotation = Quaternion.identity;
        }

        public void Update()
        {
            // Toggle with A (right controller A button)
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                visible = !visible;
                if (visible) animationScale = 0f;
                menuRoot.SetActive(visible);
            }

            if (visible)
            {
                animationScale = Mathf.Lerp(animationScale, 1f, Time.deltaTime * animationSpeed);
                menuRoot.transform.localScale = Vector3.one * animationScale;

                // Follow right hand
                if (GorillaLocomotion.Player.Instance != null)
                {
                    Transform hand = GorillaLocomotion.Player.Instance.rightHandTransform;
                    menuRoot.transform.position = hand.position + hand.forward * 0.20f;
                    menuRoot.transform.rotation = hand.rotation;
                }
            }

            // Animate glow based on lobby state
            if (ButtonRenderer != null)
            {
                Color glow = Logic.LobbyStateManager.GetLobbyGlow();
                ButtonRenderer.material.SetColor("_EmissionColor", glow * disconnectButton.GlowMultiplier);
            }
        }

        void OnDisconnectPressed()
        {
            Photon.Pun.PhotonNetwork.LeaveRoom();
        }
    }
}