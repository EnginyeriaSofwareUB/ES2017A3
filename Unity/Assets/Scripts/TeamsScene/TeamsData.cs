
public static class TeamsData
{
	private static int playersRed = 2, playersBlue = 2, currentTotem = 0;

	public static int PlayersRed 
	{
		get 
		{
			return playersRed;
		}
		set 
		{
			playersRed = value;
		}
	}

	public static int PlayersBlue 
	{
		get 
		{
			return playersBlue;
		}
		set 
		{
			playersBlue = value;
		}
	}

	public static int CurrentTotem 
	{
		get 
		{
			return currentTotem;
		}
		set 
		{
			currentTotem = value;
		}
	}

}