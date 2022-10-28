using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trello;

public class TrelloUI : MonoBehaviour
{
	private static readonly string[] TrelloCardPositions = { "top", "bottom" };

	[SerializeField]
	private TrelloPoster trelloPoster;
	[SerializeField]
	private GameObject trelloCanvas;
	[SerializeField]
	private GameObject reportPanel;
	[SerializeField]
	private InputField cardName;
	[SerializeField]
	private InputField cardDesc;
	[SerializeField]
	private Dropdown cardPosition;
	[SerializeField]
	private Dropdown cardList;
	[SerializeField]
	private Dropdown cardLabel;
	[SerializeField]
	private Toggle includeScreenshot;

	private Texture2D screenshot;
	private bool noLabels = false;

	public GetTest getTest ;

	private void Start()
	{
		cardList.AddOptions(GetDropdownOptions(trelloPoster.TrelloCardListOptions));
		TrelloCardOption[] cardLabels = trelloPoster.TrelloCardLabelOptions;
		if (cardLabels == null || cardLabels.Length == 0)
		{
			noLabels = true;
			cardLabel.gameObject.SetActive(false);
		}
		else
		{
			cardLabel.AddOptions(GetDropdownOptions(cardLabels));
		}
	}

	public void StartPostCard()
	{
		//샌드 눌렀을 때 카드 리스트를 가져올 수 있음 
		//List<string> idlist = new List<string>();
		//idlist.Add(trelloPoster.TrelloCardListOptions[cardList.value].Id);
		/*for(int i =0; i < getTest.idLists.Count; i++)
        {
			//idList에 없을 때 추가 (중복안되게)
			if(getTest.idLists.Contains(trelloPoster.TrelloCardListOptions[cardList.value].Id) == false)
            {
				getTest.idLists.Add(trelloPoster.TrelloCardListOptions[cardList.value].Id);

				print(getTest.idLists[i]);
			}
			

        }*/
		StartCoroutine(trelloPoster.PostCard(new TrelloCard(cardName.text, cardDesc.text, TrelloCardPositions[cardPosition.value], trelloPoster.TrelloCardListOptions[cardList.value].Id, noLabels ? null : trelloPoster.TrelloCardLabelOptions[cardLabel.value].Id, includeScreenshot.isOn ? screenshot.EncodeToPNG() : null)));
	}

	private List<Dropdown.OptionData> GetDropdownOptions(TrelloCardOption[] cardOptions)
	{
		List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
		for (int i = 0; i < cardOptions.Length; i++)
		{
			dropdownOptions.Add(new Dropdown.OptionData(cardOptions[i].Name));
		}
		return dropdownOptions;
	}

	public void ToggleCanvas()
	{
		trelloCanvas.SetActive(!trelloCanvas.activeSelf);
	}

	public void ToggleCanvas(bool isEnabled)
	{
		trelloCanvas.SetActive(isEnabled);
	}

	public void TogglePanel()
	{
		reportPanel.SetActive(!reportPanel.activeSelf);
	}

	public void TakeScreenshot()
	{
		screenshot = ScreenCapture.CaptureScreenshotAsTexture();
	}

	public void ResetUI()
	{
		cardName.text = "";
		cardDesc.text = "";
	}
}
