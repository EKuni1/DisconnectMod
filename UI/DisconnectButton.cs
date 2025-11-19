using UnityEngine;

namespace Menu
{
    public class MenuButton
    {
        public GameObject ButtonObject { get; private set; }
        public Renderer Renderer { get; private set; }
        public float GlowMultiplier { get; private set; } = 1f;

        System.Action onPressed;

        public MenuButton(Transform parent, string label, System.Action onPressed)
        {
            this.onPressed = onPressed;

            ButtonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ButtonObject.name = "Button_" + label;
            ButtonObject.transform.SetParent(parent, false);
            ButtonObject.transform.localPosition = new Vector3(0f, -0.015f, 0.015f);
            ButtonObject.transform.localScale = new Vector3(0.12f, 0.04f, 0.01f);

            Renderer = ButtonObject.GetComponent<Renderer>();
            Renderer.material = new Material(Shader.Find("GorillaTag/UberShader"));
            Renderer.material.color = new Color(0.8f, 0.2f, 1f);
            Renderer.material.EnableKeyword("_EMISSION");

            // Add Gorilla's pressable button component for finger interaction
            var press = ButtonObject.AddComponent<GorillaPressableButton>();
            press.debounceTime = 0.2f;
            press.onPress.AddListener(() => HandlePress());
        }

        void HandlePress()
        {
            // flash glow
            GlowMultiplier = 4f;
            ButtonObject.AddComponent<GlowResetHelper>().StartReset(this, 0.15f);

            onPressed?.Invoke();
        }

        class GlowResetHelper : MonoBehaviour
        {
            public void StartReset(MenuButton target, float delay)
            {
                StartCoroutine(DoReset(target, delay));
            }

            System.Collections.IEnumerator DoReset(MenuButton target, float delay)
            {
                float t = 0f;
                while (t < delay)
                {
                    t += Time.deltaTime;
                    float frac = 1f - (t / delay);
                    target.GlowMultiplier = Mathf.Lerp(1f, 4f, frac);
                    yield return null;
                }
                target.GlowMultiplier = 1f;
                Destroy(this);
            }
        }
    }
}