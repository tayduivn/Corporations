﻿namespace Assets.Core
{
    public static partial class Teams
    {
        // ------------------- Management cost
        
        public static Bonus<float> GetManagerPointChange(GameEntity company, GameContext gameContext)
        {
            var teams = company.team.Teams;
        
            var bonus = new Bonus<float>("Point gain");

            bool teamsOnly = teams.Count > 3;

            foreach (var team in teams)
            {
                var b = GetTeamManagementBonus(team, company, gameContext, true);

                if (teamsOnly)
                {
                    bonus.Append(team.Name, b.Sum());
                }
                else
                {
                    bonus.Append(b);
                }
            }

            return bonus;
        }

        public static float GetPolicyValueModified(GameEntity company, CorporatePolicy policy, float min,
            float centerValue,
            float max)
        {
            var flatness = Companies.GetPolicyValue(company, policy);

            var center = 5;

            // ---- structure
            var multiplier = centerValue;
            if (flatness < center)
                multiplier = min;

            if (flatness > center)
                multiplier = max;

            return multiplier;
        }

        public static bool IsDirectManagement(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            return false;
        }
        
        public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext gameContext, bool shortDescription = false)
        {
            var bonus = new Bonus<float>("points");
        
            var role = GetMainManagerRole(team);

            var flatness  = GetPolicyValueModified(company, CorporatePolicy.DecisionsManagerOrTeam, 1f, 0.5f, 0.25f);
            var processes = GetPolicyValueModified(company, CorporatePolicy.PeopleOrProcesses, 1f, 0.5f, 0.25f);


            var gain = GetTeamManagementGain(team, company, gameContext);
            
            var directMaintenance = GetDirectManagementCostOfTeam(team) * processes;
            var indirectMaintenance = GetIndirectManagementCostOfTeam(team) * flatness;

            var formattedManagerRole = Humans.GetFormattedRole(role);
            
            bool noManager = !HasMainManagerInTeam(team);
            bool isDirectManagement = noManager || team.isCoreTeam;
            
            
            // if not managed properly
            // spend points from CEO

            var managingBadly = directMaintenance > gain;

            bool applyDirectManagementCost = isDirectManagement || managingBadly;
            bool applyIndirectManagementCost = !isDirectManagement;

            if (applyDirectManagementCost)
            {
                if (shortDescription)
                {
                    bonus.AppendAndHideIfZero("Direct management in " + team.Name, gain - directMaintenance);
                }
                else
                {
                    bonus.AppendAndHideIfZero(formattedManagerRole, gain);
                    bonus.AppendAndHideIfZero($"Base maintenance for {team.Rank}", -directMaintenance);
                }
            }


            if (applyIndirectManagementCost)
            {
                bonus.Append($"Indirect management for {team.Rank}", -indirectMaintenance);
            }
            
            return bonus;
        }

        static float GetTeamManagementGain(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, gameContext);

            var rating = GetEffectiveManagerRating(company, manager);
        
            return rating / 10f;
        }

        public static int GetDirectManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return team.isCoreTeam ? 4 : C.MANAGEMENT_COST_SOLO;
                case TeamRank.SmallTeam: return C.MANAGEMENT_COST_SMALL_TEAM;
                case TeamRank.BigTeam: return C.MANAGEMENT_COST_BIG_TEAM;
                case TeamRank.Department: return C.MANAGEMENT_COST_DEPARTMENT;

                default: return 0;
            }
        }

        public static int GetIndirectManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return 1;
                case TeamRank.SmallTeam: return 2;
                case TeamRank.BigTeam: return 3;
                case TeamRank.Department: return 4;

                default: return 0;
            }
        }
    }
}