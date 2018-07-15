using UnityEngine;

public class BakeBread : GoapAction 
{
	bool completed = false;
	float startTime = 0;
	public float workDuration = 2;

	public BakeBread()
	{
		addPrecondition("hasFlour", true);
		addEffect("doJob", true);
		name = "BakeBread";
	}

	public override void reset()
	{
		completed = false;
		startTime = 0;
	}

	public override bool isDone()
	{
		return completed;
	}

	public override bool requiresInRange()
	{
		return true;
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		return true;
	}

	public override bool perform(GameObject agent)
	{
		if(startTime==0)
		{
			Debug.Log("Starting: " + name);
			startTime = Time.time;
		}

		if(Time.time - startTime > workDuration)
		{
			Debug.Log("Finished: " + name);
			GetComponent<Inventory>().flourLevel -= 2;
			GetComponent<Inventory>().breadLevel += 1;
			completed = true;
		}
		return true;
	}
}
