using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool open {  get; private set; }
    CanvasGroup canvasGroup;
    [SerializeField] float openTime;

    [SerializeField] InputActionProperty openAction;

	

    [ContextMenu("Open")]
    public void Open()
    {
        if (open)
            return;
		DOTween.Kill(this,true);
		open = true;
		Sequence s = DOTween.Sequence(this);
		s.SetUpdate(true);
		s.Append(canvasGroup.DOFade(1, openTime));
		s.AppendCallback(() =>
		{
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
			Time.timeScale = 0;
			Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
			
		});
	}
	[ContextMenu("Close")]
	public void Close()
    {
        if(!open) 
            return;
        DOTween.Kill(this,true);
        open = false;
        Sequence s = DOTween.Sequence(this);
        s.SetUpdate(true);
		s.AppendCallback(() =>
		{
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
			Time.timeScale = 1;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			
		});
		s.Append(canvasGroup.DOFade(0, openTime));
        
	}

    public void Toggle()
    {
        if(open)
            Close();
        else
            Open();
    }
    void ToggleAction(InputAction.CallbackContext context)
    {
        Toggle();
    }
	// Start is called before the first frame update
	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
        open = true;
        Close();
        openAction.action.performed += ToggleAction;
		
	}
	private void OnEnable()
	{
        openAction.action.Enable();
	}
	private void OnDisable()
	{
		openAction.action.Disable();
	}
	private void OnDestroy()
	{
		openAction.action.performed -= ToggleAction;
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
