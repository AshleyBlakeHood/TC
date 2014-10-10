using UnityEngine;
using System.Collections;

public class AgentCreator: MonoBehaviour
{
	public GameObject agentPrefab;

	public GUIM_AgentViewer agentViewer;

	public TextAsset englishNames;
	public TextAsset maleHeights;
	public TextAsset femaleHeights;
	public TextAsset birthYears;
	public TextAsset hairColours;
	public TextAsset eyeColours;

	public TextAsset primaryStats;
	public TextAsset skills;

	public TextAsset verbs;
	public TextAsset codenameTemplates;

	private string[] forenames = {"John", "Thomas", "George"};
	private string[] surnames = {"Saint", "Smith"};
	private string[] codenames = {"Duchess", "007", "001"};
	private string[] aliases = {"None"};

	private string gender = "";

	private ItemWeightHolder[] agesList;

	private string height = "";

	//Height Variables
	private ItemWeightHolder[] maleHeight18t24;
	private ItemWeightHolder[] maleHeight25t34;
	private ItemWeightHolder[] maleHeight35t44;
	private ItemWeightHolder[] maleHeight45t54;
	private ItemWeightHolder[] maleHeight55t64;
	private ItemWeightHolder[] maleHeight65;

	private ItemWeightHolder[] femaleHeight18t24;
	private ItemWeightHolder[] femaleHeight25t34;
	private ItemWeightHolder[] femaleHeight35t44;
	private ItemWeightHolder[] femaleHeight45t54;
	private ItemWeightHolder[] femaleHeight55t64;
	private ItemWeightHolder[] femaleHeight65;

	private ItemWeightHolder[] eyess;
	private ItemWeightHolder[] hairs;

	private AvatarHolder[] data;

	private string[] verbList;

	private ItemWeightHolder[] charisma;
	private ItemWeightHolder[] intelligence;
	private ItemWeightHolder[] agility;

	private ItemWeightHolder[] stealth;
	private ItemWeightHolder[] steal;
	private ItemWeightHolder[] firearms;
	private ItemWeightHolder[] unarmed;
	private ItemWeightHolder[] firstaid;
	private ItemWeightHolder[] lockpick;
	private ItemWeightHolder[] hacking;
	private ItemWeightHolder[] perception;
	private ItemWeightHolder[] investigation;
	private ItemWeightHolder[] deception;
	private ItemWeightHolder[] persuasion;

	// Use this for initialization
	void Start ()
	{
		ReadInNameData ();
		ReadInHeightData ();
		ReadInAgeData ();
		ReadInHairColours ();
		ReadInEyeColours ();
		ReadInVerbs ();
		ReadInCodenameTemplates ();
		ReadInPrimaryStats ();
		ReadInSkills ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return))
		{
			CreateNewAgent ();
		}
	}

	public void ReadInNameData()
	{
		string[] lines = englishNames.text.Trim ('\n').Split ('\n');
		
		data = new AvatarHolder[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 4)
				continue;
			
			data[i] = new AvatarHolder(lineSplit[0], lineSplit[1], lineSplit[2], lineSplit[3]);
		}
	}

	public void ReadInHeightData()
	{
		//Read in file.
		string[] maleLines = maleHeights.text.Trim ('\n').Split ('\n');
		string[] femaleLines = femaleHeights.text.Trim ('\n').Split ('\n');

		//Instantiate Male arrays.
		maleHeight18t24 = new ItemWeightHolder[maleLines.Length - 1];
		maleHeight25t34 = new ItemWeightHolder[maleLines.Length - 1];
		maleHeight35t44 = new ItemWeightHolder[maleLines.Length - 1];
		maleHeight45t54 = new ItemWeightHolder[maleLines.Length - 1];
		maleHeight55t64 = new ItemWeightHolder[maleLines.Length - 1];
		maleHeight65 = new ItemWeightHolder[maleLines.Length - 1];

		//Instantiate Female arrays.
		femaleHeight18t24 = new ItemWeightHolder[femaleLines.Length - 1];
		femaleHeight25t34 = new ItemWeightHolder[femaleLines.Length - 1];
		femaleHeight35t44 = new ItemWeightHolder[femaleLines.Length - 1];
		femaleHeight45t54 = new ItemWeightHolder[femaleLines.Length - 1];
		femaleHeight55t64 = new ItemWeightHolder[femaleLines.Length - 1];
		femaleHeight65 = new ItemWeightHolder[femaleLines.Length - 1];

		//Populate Male Arrays
		for (int i = 0; i < maleLines.Length - 1; i++)
		{
			string[] lineSplit = maleLines[i + 1].Split (',');
			
			if (lineSplit.Length < 7)
				continue;

			//Assign Values
			maleHeight18t24[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[1]));
			maleHeight25t34[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[2]));
			maleHeight35t44[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[3]));
			maleHeight45t54[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[4]));
			maleHeight55t64[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[5]));
			maleHeight65[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[6]));
		}

		//Populate Female Arrays
		for (int i = 0; i < femaleLines.Length - 1; i++)
		{
			string[] lineSplit = femaleLines[i + 1].Split (',');
			
			if (lineSplit.Length < 7)
				continue;

			//Assign Values
			femaleHeight18t24[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[1]));
			femaleHeight25t34[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[2]));
			femaleHeight35t44[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[3]));
			femaleHeight45t54[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[4]));
			femaleHeight55t64[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[5]));
			femaleHeight65[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[6]));
		}
	}

	public void ReadInAgeData()
	{
		string[] lines = birthYears.text.Trim ('\n').Split ('\n');
		
		agesList = new ItemWeightHolder[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			agesList[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[1]));
		}
	}

	public void ReadInHairColours()
	{
		string[] lines = hairColours.text.Trim ('\n').Split ('\n');
		
		hairs = new ItemWeightHolder[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			hairs[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[1]));
		}
	}

	public void ReadInEyeColours()
	{
		string[] lines = eyeColours.text.Trim ('\n').Split ('\n');
		
		eyess = new ItemWeightHolder[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 2)
				continue;
			
			eyess[i] = new ItemWeightHolder(lineSplit[0], float.Parse(lineSplit[1]));
		}
	}

	public void ReadInVerbs()
	{
		string[] lines = verbs.text.Trim ('\n').Split ('\n');
		
		verbList = new string[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 1)
				continue;
			
			verbList[i] = lineSplit[0];
		}
	}

	public void ReadInCodenameTemplates()
	{
		string[] lines = codenameTemplates.text.Trim ('\n').Split ('\n');
		
		codenames = new string[lines.Length];
		
		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (lineSplit.Length < 1)
				continue;
			
			codenames[i] = lineSplit[0];
		}
	}

	public void ReadInPrimaryStats()
	{
		string[] lines = primaryStats.text.Trim ('\n').Split ('\n');
		
		charisma = new ItemWeightHolder[lines.Length - 1];
		intelligence = new ItemWeightHolder[lines.Length - 1];
		agility = new ItemWeightHolder[lines.Length - 1];

		int charIndex = 0;
		int intelIndex = 0;
		int agilIndex = 0;

		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');

			if (i == 0)
			{
				for (int ls = 0; ls < lineSplit.Length; ls++)
				{
					switch (lineSplit[ls])
					{
					case "Charisma":
						charIndex = ls;
						break;
					case "Intelligence":
						intelIndex = ls;
						break;
					case "Agility":
						agilIndex = ls;
						break;
					}
				}
			}
			else
			{
				if (lineSplit.Length < 4)
					continue;
				
				charisma[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[charIndex]));
				intelligence[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[intelIndex]));
				agility[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[agilIndex]));
			}
		}
	}

	public void ReadInSkills()
	{
		string[] lines = skills.text.Trim ('\n').Split ('\n');
		
		stealth = new ItemWeightHolder[lines.Length - 1];
		steal = new ItemWeightHolder[lines.Length - 1];
		firearms = new ItemWeightHolder[lines.Length - 1];
		unarmed = new ItemWeightHolder[lines.Length - 1];
		firstaid = new ItemWeightHolder[lines.Length - 1];
		lockpick = new ItemWeightHolder[lines.Length - 1];
		hacking = new ItemWeightHolder[lines.Length - 1];
		perception = new ItemWeightHolder[lines.Length - 1];
		investigation = new ItemWeightHolder[lines.Length - 1];
		deception = new ItemWeightHolder[lines.Length - 1];
		persuasion = new ItemWeightHolder[lines.Length - 1];
		
		int stealthIndex = 0;
		int stealIndex = 0;
		int firearmsIndex = 0;
		int unarmIndex = 0;
		int firstIndex = 0;
		int lockpickIndex = 0;
		int hackIndex = 0;
		int percepIndex = 0;
		int investIndex = 0;
		int decepIndex = 0;
		int persIndex = 0;

		for (int i = 0; i < lines.Length; i++)
		{
			string[] lineSplit = lines[i].Split (',');
			
			if (i == 0)
			{
				for (int ls = 0; ls < lineSplit.Length; ls++)
				{
					switch (lineSplit[ls])
					{
					case "Stealth":
						stealthIndex = ls;
						break;
					case "Steal":
						stealIndex = ls;
						break;
					case "Firearms":
						firearmsIndex = ls;
						break;
					case "Unarmed":
						unarmIndex = ls;
						break;
					case "First Aid":
						firstIndex = ls;
						break;
					case "Lockpick":
						lockpickIndex = ls;
						break;
					case "Hacking":
						hackIndex = ls;
						break;
					case "Perception":
						percepIndex = ls;
						break;
					case "Investigation":
						investIndex = ls;
						break;
					case "Deception":
						decepIndex = ls;
						break;
					case "Persuasion":
						persIndex = ls;
						break;
					}
				}
			}
			else
			{
				stealth[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[stealthIndex]));
				steal[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[stealIndex]));
				firearms[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[firearmsIndex]));
				unarmed[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[unarmIndex]));
				firstaid[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[firstIndex]));
				lockpick[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[lockpickIndex]));
				hacking[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[hackIndex]));
				perception[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[percepIndex]));
				investigation[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[investIndex]));
				deception[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[decepIndex]));
				persuasion[i - 1] = new ItemWeightHolder(lineSplit[0], float.Parse (lineSplit[persIndex]));
			}
		}
	}

	//System. is used to prevent a duplicate occurence of 'Random'.
	public int CalculateAge(System.DateTime reference, int day, int month, int year)
	{
		System.DateTime birthday = new System.DateTime (year, month, day);
		
		int age = reference.Year - birthday.Year;
		
		if (reference < birthday.AddYears (age))
			age--;

		return age;
	}

	public string GetHeightForAgent(int age, string gender)
	{
		if (gender.ToUpper() == "MALE")
		{
			if (age <= 24)
				return SelectionEngine.GetItem (maleHeight18t24);
			else if (age >= 25 && age <= 34)
				return SelectionEngine.GetItem (maleHeight25t34);
			else if (age >= 35 && age <= 44)
				return SelectionEngine.GetItem (maleHeight35t44);
			else if (age >= 45 && age <= 54)
				return SelectionEngine.GetItem (maleHeight45t54);
			else if (age >= 55 && age <= 64)
				return SelectionEngine.GetItem (maleHeight55t64);
			else if (age >= 65)
				return SelectionEngine.GetItem (maleHeight65);
		}
		else if (gender.ToUpper() == "FEMALE")
		{
			if (age <= 24)
				return SelectionEngine.GetItem (femaleHeight18t24);
			else if (age >= 25 && age <= 34)
				return SelectionEngine.GetItem (femaleHeight25t34);
			else if (age >= 35 && age <= 44)
				return SelectionEngine.GetItem (femaleHeight35t44);
			else if (age >= 45 && age <= 54)
				return SelectionEngine.GetItem (femaleHeight45t54);
			else if (age >= 55 && age <= 64)
				return SelectionEngine.GetItem (femaleHeight55t64);
			else if (age >= 65)
				return SelectionEngine.GetItem (femaleHeight65);
		}

		return "";
	}

	public Name GetAlias(string forename, string surname, string gender)
	{
		int path = Random.Range (0, 3);
		int startPoint = Random.Range (0, data.Length);
		int direction = Random.Range (0, 2); // 0 = Up, 1 = Down

		//Decides if name should be based on forename, surname or both.
		if (path == 0)
		{
			//Forename
			string fName = "";
			string sName = "";

			int i = startPoint;

			while (fName == "")
			{
				if (data[i].gender.ToUpper () != gender.ToUpper () || data[i].forename[0] != forename[0])
				{
					if (direction == 0)
					{
						i++;

						if (i >= data.Length - 1)
							i = 0;
					}
					else
					{
						i--;
						
						if (i < 0)
							i = data.Length - 1;
					}
				}
				else
				{
					fName = data[i].forename;
					sName = data[startPoint].surname;
				}
			}
			return new Name(fName, sName);
		}
		else if (path == 1)
		{
			//Surname
			string fName = "";
			string sName = "";
			
			int i = startPoint;

			//Find any surname but with right gender.
			while (fName == "")
			{
				if (data[i].gender.ToUpper () != gender.ToUpper ())
				{
					if (direction == 0)
					{
						i++;
						
						if (i >= data.Length - 1)
							i = 0;
					}
					else
					{
						i--;
						
						if (i < 0)
							i = data.Length - 1;
					}
				}
				else
				{
					fName = data[i].forename;
				}
			}

			//Find a matching surname.
			while (sName == "")
			{
				if (data[i].surname[0] != surname[0])
				{
					if (direction == 0)
					{
						i++;
						
						if (i >= data.Length - 1)
							i = 0;
					}
					else
					{
						i--;
						
						if (i < 0)
							i = data.Length - 1;
					}
				}
				else
				{
					sName = data[i].surname;
				}
			}
			return new Name(fName, sName);
		}
		else if (path == 2)
		{
			//Both
			string fName = "";
			string sName = "";
			
			int i = startPoint;

			//Firstname Part
			while (fName == "")
			{
				if (data[i].gender.ToUpper () != gender.ToUpper () || data[i].forename[0] != forename[0])
				{
					if (direction == 0)
					{
						i++;
						
						if (i >= data.Length - 1)
							i = 0;
					}
					else
					{
						i--;
						
						if (i < 0)
							i = data.Length - 1;
					}
				}
				else
				{
					fName = data[i].forename;
				}
			}

			i = startPoint;

			//Last Name Part
			while (sName == "")
			{
				if (data[i].gender.ToUpper () != gender.ToUpper () || data[i].surname[0] != surname[0])
				{
					if (direction == 0)
					{
						i++;
						
						if (i >= data.Length - 1)
							i = 0;
					}
					else
					{
						i--;
						
						if (i < 0)
							i = data.Length - 1;
					}
				}
				else
				{
					sName = data[i].surname;
				}
			}

			return new Name(fName, sName);
		}

		return new Name ("J", "Smith");
	}

	public string GetCodename()
	{
		string codename = codenames [Random.Range (0, codenames.Length)];

		while (codename.Contains ("[VERB]"))
		{
			int index = codename.IndexOf ("[VERB]");
			codename = codename.Remove(index, 6);

			codename = codename.Insert (index, verbList[Random.Range (0, verbList.Length)]);
		}

		return codename;
	}

	public PrimaryStats GetPrimaryStats()
	{
		int c = int.Parse (SelectionEngine.GetItem (charisma));
		int i = int.Parse (SelectionEngine.GetItem (intelligence));
		int a = int.Parse (SelectionEngine.GetItem (agility));

		return new PrimaryStats (c, i, a);
	}

	public SkillsHolder GetSkills()
	{
		int stlth = int.Parse (SelectionEngine.GetItem (stealth));
		int stl = int.Parse (SelectionEngine.GetItem (steal));
		int fire = int.Parse (SelectionEngine.GetItem (firearms));
		int u = int.Parse (SelectionEngine.GetItem (unarmed));
		int first = int.Parse (SelectionEngine.GetItem (firstaid));
		int l = int.Parse (SelectionEngine.GetItem (lockpick));
		int h = int.Parse (SelectionEngine.GetItem (hacking));
		int perc = int.Parse (SelectionEngine.GetItem (perception));
		int invest = int.Parse (SelectionEngine.GetItem (investigation));
		int d = int.Parse (SelectionEngine.GetItem (deception));
		int pers = int.Parse (SelectionEngine.GetItem (persuasion));

		return new SkillsHolder (stlth, stl, fire, u, first, l, h, perc, invest, d, pers);
	}

	public AgentData CreateNewAgent()
	{
//		string forename = forenames [Random.Range (0, forenames.Length)];
//		string surname = forenames [Random.Range (0, surnames.Length)];
		Random.seed = Random.seed;
		int seed = Random.seed;

		int row = Random.Range (0, data.Length);

		string forename = data[row].forename;
		string surname = data [Random.Range (0, data.Length)].surname;
		string codename = GetCodename ();

		gender = data [row].gender;

		int count = Random.Range (0, 5);

		string[] aliases = new string[count];
		string alias = "";

		for (int i = 0; i < count; i++)
		{
			Name aliasName = GetAlias (forename, surname, gender);

			aliases[i] = aliasName.forename + " " + aliasName.surname;
			alias += aliasName.forename + " " + aliasName.surname + ", ";
		}

		alias = alias.Trim ((", ").ToCharArray ());

		int day = Random.Range (1, 31);
		int month = Random.Range (1, 13);
		int year = int.Parse (SelectionEngine.GetItem (agesList));

		System.DateTime birthday = new System.DateTime(year, month, 1);
		birthday = birthday.AddDays (day);

		height = GetHeightForAgent (CalculateAge (System.DateTime.Now, day, month, year), gender);

		string hair = SelectionEngine.GetItem (hairs);
		string eyes = SelectionEngine.GetItem (eyess);

		PrimaryStats stats = GetPrimaryStats ();
		SkillsHolder skills = GetSkills ();

		Debug.Log (string.Format ("Stats: Charisma: {0} Inteligence: {1} Agility: {2}.", stats.charisma, stats.intelligence, stats.agility));
		Debug.Log (string.Format ("Skills: Stealth: {0} Steal: {1} Firearms: {2} Unarmed: {3} First Aid: {4} Lockpick: {5} Hacking: {6} Perception: {7} Investigation: {8} Deception: {9} Persuasion: {10}",
		                          skills.stealth, skills.steal, skills.firearms, skills.unarmed, skills.firstaid, skills.lockpick, skills.hacking,
		                          skills.perception, skills.investigation, skills.deception, skills.persuasion));

		agentViewer.txtName.text = "Name: " + forename + " " + surname;
		agentViewer.txtCodename.text = "Codename: " + codename;
		agentViewer.txtAliases.text = "Aliases: " + alias;
		agentViewer.txtGender.text = "Gender: " + gender;
		agentViewer.txtDOB.text = "Date of Birth: " + birthday.Day + "/" + birthday.Month + "/" + birthday.Year + " (" + CalculateAge (System.DateTime.Now, birthday.Day, birthday.Month, birthday.Year) + ")";
		agentViewer.txtHeight.text = "Height: " + height;
		agentViewer.txtEyes.text = "Eyes: " + eyes;
		agentViewer.txtHair.text = "Hair: " + hair;
		agentViewer.txtSeed.text = "Seed: " + seed;

		return new AgentData (forename, surname, codename, aliases, gender, birthday.Day.ToString (), birthday.Month.ToString (), birthday.Year.ToString (), height, eyes, hair, stats, skills);
	}
}

public class AvatarHolder
{
	public string forename = "";
	public string surname = "";
	public string gender = "";
	public string country = "";

	public AvatarHolder(string fore, string sur, string g, string c)
	{
		forename = fore;
		surname = sur;
		gender = g;
		country = c;
	}
}

public class Name
{
	public string forename = "";
	public string surname = "";

	public Name(string iForename, string iSurname)
	{
		forename = iForename;
		surname = iSurname;
	}
}