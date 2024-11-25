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

    [Header("UI Components")]
    public GameObject teamNameComponent;
    public GameObject deptNameComponent;
    public GameObject yourRankRow;

    [Header("Your Rank UI")]
    public TextMeshProUGUI yourRankText;
    public TextMeshProUGUI yourNameText;
    public TextMeshProUGUI yourTeamOrDeptText;
    public TextMeshProUGUI yourScoreText;

    [Header("Individual Name Components")]
    public GameObject nameHeader;
    public TextMeshProUGUI[] individualNameComponents;

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
        // Handle TeamName/DeptName visibility
        teamNameComponent.SetActive(viewType.ToLower() != "dept");
        deptNameComponent.SetActive(viewType.ToLower() == "dept");

        // Handle individual name components visibility
        bool showIndividualNames = viewType.ToLower() == "solo";
        nameHeader.SetActive(showIndividualNames);

        // Hide/Show all individual name components
        foreach (var nameComponent in individualNameComponents)
        {
            if (nameComponent)
            {
                nameComponent.gameObject.SetActive(showIndividualNames);
            }
        }

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
        string currentUserEmail = PlayerPrefs.GetString("Email");
        bool userInTopFive = false;

        for (int i = 0; i < individualRankTexts.Length; i++)
        {
            if (i < entries.Count)
            {
                var entry = entries[i];
                if (entry.user_email == currentUserEmail)
                {
                    userInTopFive = true;
                }

                // Show the row (parent GameObject)
                individualRankTexts[i].transform.parent.gameObject.SetActive(true);

                // Update the texts
                individualRankTexts[i].text = entry.rank.ToString();
                individualNameTexts[i].text = entry.user_email;
                individualTeamTexts[i].text = entry.team_name;
                individualScoreTexts[i].text = entry.endless_score.ToString();
            }
            else
            {
                // Hide unused rows
                individualRankTexts[i].transform.parent.gameObject.SetActive(false);
            }
        }

        // Handle user rank display
        if (!userInTopFive)
        {
            yourRankRow.SetActive(true);
            // Fetch user's actual rank from API
            var userRank = entries.Find(e => e.user_email == currentUserEmail);
            if (userRank != null)
            {
                yourRankText.text = userRank.rank.ToString();
                yourNameText.text = userRank.user_email;
                yourTeamOrDeptText.text = userRank.team_name;
                yourScoreText.text = userRank.endless_score.ToString();
            }
        }
        else
        {
            yourRankRow.SetActive(false);
        }
    }

    private void UpdateTeamUI(List<TeamLeaderboardEntry> entries)
    {
        string currentUserTeam = "McDonalds"; // Get this from your auth system
        bool userTeamInTopFive = false;

        for (int i = 0; i < teamRankTexts.Length; i++)
        {
            if (i < entries.Count)
            {
                var entry = entries[i];
                if (entry.team_name == currentUserTeam)
                {
                    userTeamInTopFive = true;
                }

                // Show the row
                teamRankTexts[i].transform.parent.gameObject.SetActive(true);

                // Update the texts
                teamRankTexts[i].text = entry.rank.ToString();
                teamNameTexts[i].text = entry.team_name;
                teamScoreTexts[i].text = entry.total_score.ToString();
            }
            else
            {
                teamRankTexts[i].transform.parent.gameObject.SetActive(false);
            }
        }

        // Handle user's team rank display
        if (!userTeamInTopFive)
        {
            yourRankRow.SetActive(true);
            var userTeamRank = entries.Find(e => e.team_name == currentUserTeam);
            if (userTeamRank != null)
            {
                yourRankText.text = userTeamRank.rank.ToString();
                yourNameText.text = userTeamRank.team_name;
                yourScoreText.text = userTeamRank.total_score.ToString();
            }
        }
        else
        {
            yourRankRow.SetActive(false);
        }
    }

    private void UpdateDepartmentUI(List<DepartmentLeaderboardEntry> entries)
    {
        string currentUserDept = "McDonalds";
        bool userDeptInTopFive = false;

        for (int i = 0; i < deptRankTexts.Length; i++)
        {
            if (i < entries.Count)
            {
                var entry = entries[i];
                if (entry.department == currentUserDept)
                {
                    userDeptInTopFive = true;
                }

                // Show the row
                deptRankTexts[i].transform.parent.gameObject.SetActive(true);

                // Update the texts
                deptRankTexts[i].text = entry.rank.ToString();
                deptNameTexts[i].text = entry.department;
                deptScoreTexts[i].text = entry.total_score.ToString();
            }
            else
            {
                deptRankTexts[i].transform.parent.gameObject.SetActive(false);
            }
        }

        // Handle user's department rank display
        if (!userDeptInTopFive)
        {
            yourRankRow.SetActive(true);
            var userDeptRank = entries.Find(e => e.department == currentUserDept);
            if (userDeptRank != null)
            {
                yourRankText.text = userDeptRank.rank.ToString();
                yourNameText.text = userDeptRank.department;
                yourScoreText.text = userDeptRank.total_score.ToString();
            }
        }
        else
        {
            yourRankRow.SetActive(false);
        }
    }

    // private void SetIndividualEntryVisibility(int index, bool visible)
    // {
    //     individualRankTexts[index].transform.parent.gameObject.SetActive(visible);
    // }

    // private void SetTeamEntryVisibility(int index, bool visible)
    // {
    //     teamRankTexts[index].transform.parent.gameObject.SetActive(visible);
    // }

    // private void SetDepartmentEntryVisibility(int index, bool visible)
    // {
    //     deptRankTexts[index].transform.parent.gameObject.SetActive(visible);
    // }
}