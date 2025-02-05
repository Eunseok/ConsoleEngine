using Core.Components;
using Core.Graphics;
using static Core.Objects.Object;

namespace TestGame.Scripts
{
    public class TextScript : Script
    {
        private int _counter = 0;
        private  LabelComponent? _label;

        private  string _fullText = ""; // 전체 텍스트를 담을 변수
        private string _currentText = ""; // 현재 렌더링 중인 텍스트

        
        private bool _isAnim = true;
        
        private float _textTime = 0.0f;
        
        private const float TextTime = 0.1f;
        private const float LifeTime = 1.5f;
        
        public override void Initialize()
        {
            _label = Owner.GetComponent<LabelComponent>();

            // Sprite 안의 데이터를 "한 줄"의 텍스트로 처리
            _fullText = _label?.ToString() ?? "";
            _label?.SetLabel("");
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (_isAnim)
            {
                Animate(deltaTime);
            }
            else
            {
                SendMessage("AnimEnd");
                Destroy(Owner, LifeTime);
                IsActive = false;
            }
        }

        private void Animate(float deltaTime)       //Text 애니메이션
        {
            _textTime += deltaTime;
            if (_textTime >= TextTime) // 0.1초 간격으로 갱신
            {
                _textTime = 0.0f;

                if (_counter < _fullText.Length) // 아직 모든 텍스트를 보여주지 않았다면
                {
                    _currentText += _fullText[_counter]; // 한 글자 추가
                    _counter++;
                }
                else
                {
                    _isAnim = false;
                }

                // Sprite의 Data에 업데이트된 텍스트 반영 (한 글자씩 처리)
                _label?.SetLabel(_currentText);
            }
        }
        
    }
}