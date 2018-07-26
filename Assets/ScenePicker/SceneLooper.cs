using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLooper : MonoBehaviour 
{

	[ScenePicker]
	public string[] m_scenes;
	private int index = 0;

	private void Awake() 
	{
		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += OnChangeScene;
	}

	private void Update() 
	{
		if(Input.GetKeyDown(KeyCode.E))
			FadeInOut.Instance.fadeIn(.5f, LoadNextScene);
		else if(Input.GetKeyDown(KeyCode.Q))
			FadeInOut.Instance.fadeIn(.5f, LoadPreviousScene);
	}

	public void LoadNextScene()
	{
		if(m_scenes.Length <= 0) return;

		index++;
		
		if(index >= m_scenes.Length)
			index = 0;

		SceneManager.LoadScene(m_scenes[index]);
	}

	public void LoadPreviousScene()
	{
		if(m_scenes.Length <= 0) return;

		index--;

		if(index < 0)
			index = m_scenes.Length-1;
		
		SceneManager.LoadScene(m_scenes[index]);
	}

	public void OnChangeScene(Scene previous, Scene next)
	{
		FadeInOut.Instance.fadeOut(.5f, null);
	}
}
