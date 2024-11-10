using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class LeaderboardManager : MonoBehaviour
{
    [Header("View Toggles")]
    public GameObject individualView;
    public GameObject teamView;
    public GameObject departmentView;

    [Header("Buttons")]
    public Button soloButton;
    public Button teamsButton;
    public Button deptButton;

    [Header("Individual View UI")]
    public TextMeshProUGUI[] individualRankTexts;
    public TextMeshProUGUI[] individualNameTexts;
    public TextMeshProUGUI[] individualTeamTexts;
    public TextMeshProUGUI[] individualScoreTexts;

    [Header("Team View UI")]
    public TextMeshProUGUI[] teamRankTexts;
    public TextMeshProUGUI[] teamNameTexts;
    public TextMeshProUGUI[] teamScoreTexts;

    [Header("Department View UI")]
    public TextMeshProUGUI[] deptRankTexts;
    public TextMeshProUGUI[] deptNameTexts;
    public TextMeshProUGUI[] deptScoreTexts;

    private LeaderboardAPIService apiService;

    private void Start()
    {
        apiService = new LeaderboardAPIService();

        // Setup button listeners
        soloButton.onClick.AddListener(() => SwitchView("solo"));
        teamsButton.onClick.AddListener(() => SwitchView("teams"));
        deptButton.onClick.AddListener(() => SwitchView("dept"));

        // Default to individual view
        SwitchView("solo");
    }

    public void SwitchView(string viewType)
    {
        switch (viewType.ToLower())
        {
            case "solo":
                StartCoroutine(FetchIndividualLeaderboard());
                break;
            case "teams":
                StartCoroutine(FetchTeamLeaderboard());
                break;
            case "dept":
                StartCoroutine(FetchDepartmentLeaderboard());
                break;
        }
    }

    private IEnumerator FetchIndividualLeaderboard()
    {
        yield return StartCoroutine(apiService.GetTopIndividuals());
        UpdateIndividualUI(apiService.IndividualLeaderboard);
    }

    private IEnumerator FetchTeamLeaderboard()
    {
        yield return StartCoroutine(apiService.GetTopTeams());
        UpdateTeamUI(apiService.TeamLeaderboard);
    }

    private IEnumerator FetchDepartmentLeaderboard()
    {
        yield return StartCoroutine(apiService.GetTopDepartments());
        UpdateDepartmentUI(apiService.DepartmentLeaderboard);
    }

    private void UpdateIndividualUI(List<IndividualLeaderboardEntry> entries)
    {
        for (int i = 0; i < individualRankTexts.Length; i++)
        {
            bool shouldShow = i < entries.Count;
            SetIndividualEntryVisibility(i, shouldShow);

            if (shouldShow)
            {
                var entry = entries[i];
                individualRankTexts[i].text = entry.rank.ToString();
                individualNameTexts[i].text = entry.user_email;
                individualTeamTexts[i].text = entry.team_name;
                individualScoreTexts[i].text = entry.endless_score.ToString();
            }
        }
    }

    private void UpdateTeamUI(List<TeamLeaderboardEntry> entries)
    {
        for (int i = 0; i < teamRankTexts.Length; i++)
        {
            bool shouldShow = i < entries.Count;
            SetTeamEntryVisibility(i, shouldShow);

            if (shouldShow)
            {
                var entry = entries[i];
                teamRankTexts[i].text = entry.rank.ToString();
                teamNameTexts[i].text = entry.team_name;
                teamScoreTexts[i].text = entry.total_score.ToString();
            }
        }
    }

    private void UpdateDepartmentUI(List<DepartmentLeaderboardEntry> entries)
    {
        for (int i = 0; i < deptRankTexts.Length; i++)
        {
            bool shouldShow = i < entries.Count;
            SetDepartmentEntryVisibility(i, shouldShow);

            if (shouldShow)
            {
                var entry = entries[i];
                deptRankTexts[i].text = entry.rank.ToString();
                deptNameTexts[i].text = entry.department;
                deptScoreTexts[i].text = entry.total_score.ToString();
            }
        }
    }

    private void SetIndividualEntryVisibility(int index, bool visible)
    {
        individualRankTexts[index].transform.parent.gameObject.SetActive(visible);
    }

    private void SetTeamEntryVisibility(int index, bool visible)
    {
        teamRankTexts[index].transform.parent.gameObject.SetActive(visible);
    }

    private void SetDepartmentEntryVisibility(int index, bool visible)
    {
        deptRankTexts[index].transform.parent.gameObject.SetActive(visible);
    }
}