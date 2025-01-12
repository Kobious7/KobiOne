using UnityEngine;

public class InputManager : GMono
{
    private static InputManager instance;

    public static InputManager Instance => instance;

    [SerializeField] private Vector3 mousePos;

    public Vector3 MousePos => mousePos;

    [SerializeField] private bool qPressed = false;

    public bool QPressed 
    {
        get => qPressed;
        set => qPressed = value;
    }

    [SerializeField] private bool ePressed = false;

    public bool EPressed 
    {
        get => ePressed;
        set => ePressed = value;
    }

    [SerializeField] private bool spacePressed = false;

    public bool SpacePressed 
    {
        get => spacePressed;
        set => spacePressed = value;
    }

    [SerializeField] private float horizontal;

    public float Horizontal
    {
        get => horizontal;
        set => horizontal = value;
    }

    [SerializeField] private float vertical;

    public float Vertical
    {
        get => vertical;
        set => vertical = value;
    }

    [SerializeField] private bool enterPressed;

    public bool EnterPressed => enterPressed;

    [SerializeField] private bool jump;

    public bool Jump => jump;

    [SerializeField] private float fire1;

    public float Fire1 => fire1;

    [SerializeField] private bool bPressed;

    public bool BPressed
    {
        get => bPressed;
        set => bPressed = value;
    }

    [SerializeField] private bool cPressed;

    public bool CPressed
    {
        get => cPressed;
        set => cPressed = value;
    }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1 InputManager is allowed to exist!");

        instance = this;
    }

    protected void Update()
    {
        GetMousePos();
        GetQ();
        GetHorizontal();
        GetVertical();
        GetJumpButton();
        GetE();
        GetSpace();
        GetEnter();
        GetFire1();
        GetB();
        GetC();
    }

    private void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void GetQ()
    {
        qPressed = Input.GetKeyUp(KeyCode.Q);
    }

    private void GetE()
    {
        ePressed = Input.GetKeyUp(KeyCode.E);
    }

    private void GetSpace()
    {
        spacePressed = Input.GetKeyUp(KeyCode.Space);
    }

    private void GetHorizontal()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    private void GetVertical()
    {
        vertical = Input.GetAxis("Vertical");
    }

    private void GetJumpButton()
    {
        jump = !Input.GetKeyDown(KeyCode.W) ? Input.GetKeyDown(KeyCode.UpArrow) : Input.GetKeyDown(KeyCode.W);
    }

    private void GetEnter()
    {
        enterPressed = Input.GetKeyUp(KeyCode.Return);
    }

    private void GetFire1()
    {
        fire1 = Input.GetAxis("Fire1");
    }

    private void GetB()
    {
        bPressed = Input.GetKeyUp(KeyCode.B);
    }

    private void GetC()
    {
        cPressed = Input.GetKeyUp(KeyCode.C);
    }
}