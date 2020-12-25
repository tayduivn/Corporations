﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public class MergeTeamCandidatesListView : ListView
{
    public GameObject Label;
    
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TeamPreview>().SetEntity((TeamInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();
        
        var company = Flagship;
        var teams = company.team.Teams;

        var team = teams[SelectedTeam];

        var candidates = teams.Where(t => Teams.IsCanMergeTeams(team, t));
        
        SetItems(candidates);
        
        Draw(Label, candidates.Any());
    }
}