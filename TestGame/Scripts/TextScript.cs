using Core.Components;
using Core.Graphics;

namespace TestGame.Scripts
{
    public class TextScript : Script
    {
        private int _counter = 0;
        private  Renderer? _renderer;

        private string _fullText; // 전체 텍스트를 담을 변수
        private string _currentText = ""; // 현재 렌더링 중인 텍스트

        
        private bool _isAnim = true;
        
        private float _textTime = 0.0f;
        private float _lifeTime = 0.0f;
        
        private const float TextTime = 0.1f;
        private const float LifeTime = 1.0f;
        
        public override void Initialize()
        {
            _renderer = Owner.GetComponent<Renderer>();

            // Sprite 안의 데이터를 "한 줄"의 텍스트로 처리
            _fullText = string.Join("", _renderer.Sprite.Data);
        }

        public override void Update(float deltaTime)
        {
            if (_isAnim)
            {
                Animate(deltaTime);
            }
            else
            {
                LifeTimer(deltaTime);
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
                _renderer.Sprite.Data = new[] { _currentText };
            }
        }

        private void LifeTimer(float deltaTime)
        {
            _lifeTime += deltaTime;
            if (_textTime >= TextTime) // 0.1초 간격으로 갱신
            {
            }
        }
    }
}