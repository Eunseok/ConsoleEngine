using Core;
using Core.Components;
using Core.Input;
using Core.MyMath;
using Core.Objects;
using Core.Scenes;
using TestGame.Scripts;
using static Core.Objects.Object;

namespace TestGame.Singletons;

public class CreationManager : Script
{
    // 싱글톤 인스턴스
    public static CreationManager? Instance { get; private set; }

    public bool IsInputEnabled { get; set; } = false;

    public string PlayerName{ get; private set; }  = "Unknown";
    public string PlayerJob { get; private set; } = "Unknown";

    private static readonly string[] WelcomeMessages =
    {
        "스파르타 던전에 오신 여러분 환영합니다.",
        "당신의 이름을 알려주세요.",
        "님 반갑습니다!",
        "님의 직업은 무엇인가요?",
        "모든 준비가 끝났습니다.",
        "스파르타 마을로 이동합니다..."
    };

    private static readonly string[] InputMessages =
    {
        "당신의 이름은?",
        "당신의 직업은?"
    };

    private static readonly string[] ClassName =
    {
        "전사",
        "도적"
    };

    private static readonly string[] ConfirmSelect =
    {
        "확인",
        "취소"
    };

    public enum Select
    {
        Agree,
        Cancel
    }

    public enum State
    {
        Name,
        ClassName,
        Done
    }

    // 상태 관리 변수 (StateMachine으로 대체 가능)
    private int _count = 0;
    private int _messageIndex = 0;
    private int _inputIndex = 0;

    private State _state = State.Name;
    
    private LabelObject? _message = null;
    private ButtonObject[]? _confirmBtn = new ButtonObject[2];
    

    public CreationManager()
    {
        if (Instance == null)
            Instance = this;
    }

    public override void Initialize()
    {
        CreateConfirmButton();
        NextMessage();
    }

    protected override void OnUpdate(float deltaTime)
    {
        // 입력 활성 상태에서만 키 입력 처리
        if (_message?.IsActive() ?? false)
        {
            // 왼쪽/오른쪽 화살표로 버튼 선택
            if (InputManager.GetKey("LeftArrow"))
                Game.CursorPosition = _confirmBtn[(int)Select.Agree].GlobalPosition;
            if (InputManager.GetKey("RightArrow"))
                Game.CursorPosition = _confirmBtn[(int)Select.Cancel].GlobalPosition;
        }
    }
    public void NextMessage()
    {
        // 배열 범위 초과 방지 처리 추가
        if (_messageIndex >= WelcomeMessages.Length)
        {
            SceneManager.SetActiveScene("MainScene");
            return;
        }

        switch (_count++)
        {
            case 2:
                CreateInputMessage(InputMessages[_inputIndex]);
                break;
            case 3:
            case 4:
                CreateWelcomeMessage(PlayerName + WelcomeMessages[_messageIndex++]);
                break;
            case 5:
                SetActiveConfirmButton(true, ClassName);
                _message.SetText(InputMessages[_inputIndex]);
                break;

            default:
                CreateWelcomeMessage(WelcomeMessages[_messageIndex++]);
                break;
        }
    }

    private void CreateWelcomeMessage(string text)
    {
        var label = Instantiate<LabelObject>(Game.ConsoleCenter);
        label.SetText(text, ConsoleColor.Yellow);
        label.AddComponent<TextScript>();
        label.RegisterEventHandler("OnDestroy", _ => NextMessage());
    }

    private void CreateInputMessage(string text)
    {
        var label = Instantiate<LabelObject>(Game.ConsoleCenter - new Vector2<int>(0, 2));
        label.SetText(text);
        label.AddComponent<TextScript>();
        label.RegisterEventHandler("AnimEnd", _ => IsInputEnabled = true);
        label.LfeTime = 0.0f;
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
        IsInputEnabled = false;
        SetActiveConfirmButton(true, ConfirmSelect);
        _message.SetText($"당신의 이름은 \"{PlayerName}\" 이 맞습니까?");
    }

    public void SetClassName(string name)
    {
        PlayerJob = name;
        SetActiveConfirmButton(true, ConfirmSelect);
        _message.SetText($"당신의 직업이 \"{PlayerJob}\" 맞습니까?");
    }


    private ButtonObject CreateButton(Vector2<int> position, Action onClick)
    {
        var button = Instantiate<ButtonObject>(Game.ConsoleCenter + position);
        button.RegisterEventHandler("OnClick", _ => onClick());
        return button;
    }

    public void CreateConfirmButton()
    {
        // null 체크 및 초기화
        _message = Instantiate<LabelObject>(Game.ConsoleCenter + new Vector2<int>(0, -2));

        // 버튼 생성 로직 통합
        _confirmBtn[(int)Select.Agree] = CreateButton(new Vector2<int>(-7, 1), () => { ConfirmHandler(true); });

        _confirmBtn[(int)Select.Cancel] = CreateButton(new Vector2<int>(7, 1), () => { ConfirmHandler(false); });

        SetActiveConfirmButton(false, ConfirmSelect);
    }

    public void SetActiveConfirmButton(bool isActive, string[] text)
    {
        Game.CursorPosition = _confirmBtn[(int)Select.Agree].GlobalPosition;
        _message?.SetActive(isActive);
        int i = 0;
        foreach (var btn in _confirmBtn ?? Array.Empty<ButtonObject>())
        {
            btn?.SetLabel(text[i++]);
            btn?.SetActive(isActive);
        }
    }

    public void ConfirmHandler(bool isAgree)
    {
        if (_state == State.ClassName)
        {
            if (isAgree)
                SetClassName(ClassName[0]);
            else
                SetClassName(ClassName[1]);
            _state = State.Done;
        }
        else
        {
            // 입력/취소 처리 상태 반영
            if (isAgree)
            {
                _state =_state == State.Done? State.Done : State.ClassName;
                _inputIndex++;
            }
            else
            {
                _state =_state == State.Done? State.ClassName : State.Name;
                _count--; // 이전 메시지로 돌아감
            }
            SetActiveConfirmButton(false, ConfirmSelect);
            NextMessage();
        }
    }
}