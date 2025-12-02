using Core.Nodes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Editor.Nodes
{
    [NodeType(typeof(ScalarNode))]
    [Title("Math", "Scalar")]
    public class ScalarNodeView : NodeView
    {
        Label valueLabel;
        public ScalarNodeView()
        {
            title = "Scalar";
            node = ScriptableObject.CreateInstance<ScalarNode>();
            output = CreateOutputPort();
            
            valueLabel = new Label("0")
            {
                style =
                {
                    fontSize = 16,
                    //unityFontStyleAndWeight = FontStyle.Bold,
                    color = new Color(0.1f, 0.9f, 1f),
                    unityTextAlign = TextAnchor.MiddleCenter,
                    marginTop = 10,
                    marginBottom = 10,
                    marginLeft = 20,
                    marginRight = 20
                }
            };
            mainContainer.Add(valueLabel);
            mainContainer.style.backgroundColor = new Color(0.15f, 0.15f, 0.15f);
            RegisterCallback<ChangeEvent<float>>(evt => UpdateValueDisplay());

            UpdateValueDisplay();
        }
        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            UpdateValueDisplay();
        }

        public override void OnSelected()
        {
            base.OnSelected();
            UpdateValueDisplay();
        }
        void UpdateValueDisplay()
        {
            if (node is ScalarNode scalarNode)
            {
                float val = scalarNode.value;

                // 정수면 소수점 안 보이게
                string text = val % 1 == 0 ? ((int)val).ToString() : val.ToString("F3");

                valueLabel.text = text;

                // 보너스: 타이틀도 값으로 바꾸기 (더 깔끔!)
                //title = text;
            }
        }
    }
}