﻿using System;
using System.Collections.Generic;
using Entitas;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        internal static void AcceptProposal(GameContext gameContext, int companyId, int investorId)
        {
            var p = GetInvestmentProposal(gameContext, companyId, investorId);

            if (p == null)
                return;

            long cost = CompanyEconomyUtils.GetCompanyCost(gameContext, companyId);

            int shares = Convert.ToInt32(GetTotalShares(gameContext, companyId) * p.Offer / cost);

            AddShareholder(gameContext, companyId, investorId, shares);

            CompanyEconomyUtils.IncreaseCompanyBalance(gameContext, companyId, p.Offer);
            CompanyEconomyUtils.DecreaseInvestmentFunds(gameContext, investorId, p.Offer);

            MarkProposalAsAccepted(gameContext, companyId, investorId);
        }

        static void MarkProposalAsAccepted(GameContext gameContext, int companyId, int investorId)
        {
            var c = GetCompanyById(gameContext, companyId);

            var proposals = GetInvestmentProposals(gameContext, companyId);

            var index = proposals.FindIndex(p => p.ShareholderId == investorId);

            proposals[index].WasAccepted = true;

            c.ReplaceInvestmentProposals(proposals);
        }
    }
}
