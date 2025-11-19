using UnityEngine;

namespace Menu
{
    public class MenuPanel
    {
        public GameObject PanelObject { get; private set; }

        public MenuPanel(Transform parent)
        {
            PanelObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PanelObject.name = "MenuPanel";
            PanelObject.transform.SetParent(parent, false);
            PanelObject.transform.localScale = new Vector3(0.15f, 0.1f, 0.01f);

            var r = PanelObject.GetComponent<Renderer>();
            r.material = new Material(Shader.Find("GorillaTag/UberShader"));
            r.material.color = new Color(0.45f, 0.08f, 1f);

            // Top highlight
            GameObject highlight = GameObject.CreatePrimitive(PrimitiveType.Quad);
            highlight.name = "Highlight";
            highlight.transform.SetParent(PanelObject.transform, false);
            highlight.transform.localPosition = new Vector3(0f, 0.03f, -0.006f);
            highlight.transform.localScale = new Vector3(1.2f, 0.4f, 1f);
            var hr = highlight.GetComponent<Renderer>();
            hr.material = new Material(Shader.Find("GorillaTag/UberShader"));
            hr.material.color = new Color(0.75f, 0.28f, 1f);
        }
    }
}