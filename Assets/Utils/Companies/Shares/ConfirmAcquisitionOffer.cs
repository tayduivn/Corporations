﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class Companies
    {
        public static void ConfirmAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            BuyCompany(gameContext, companyId, buyerInvestorId, offer.acquisitionOffer.SellerOffer.Price);
        }
        public static void ConfirmCorporateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            JoinCorporation(gameContext, companyId, buyerInvestorId);
        }

        public static void BuyCompany(GameContext gameContext, int companyId, int buyerInvestorId, long offer)
        {
            // can afford acquisition
            var inv = Investments.GetInvestorById(gameContext, buyerInvestorId);
            if (!IsEnoughResources(inv, offer))
                return;

            var target = GetCompany(gameContext, companyId);

            var shareholders = GetShareholders(target);
            int[] array = new int[shareholders.Keys.Count];
            shareholders.Keys.CopyTo(array, 0);

            foreach (var shareholderId in array)
                BuyShares(gameContext, companyId, buyerInvestorId, shareholderId, shareholders[shareholderId].amount, offer, true);



            RemoveAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            target.isIndependentCompany = false;

            NotifyAboutAcquisition(gameContext, buyerInvestorId, companyId, offer);
        }



        public static void JoinCorporation(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var target = GetCompany(gameContext, companyId);
            var corporation = Investments.GetCompanyByInvestorId(gameContext, buyerInvestorId);

            var shareholders = GetShareholders(target);
            int[] array = new int[shareholders.Keys.Count];


            var corporationCost = Economy.GetCompanyCost(gameContext, corporation);
            var targetCost = Economy.GetCompanyCost(gameContext, target);
            
            var corporationShares = Companies.GetTotalShares(gameContext, companyId);
            var emitedShares = corporationShares * targetCost / corporationCost;

            // give shares in corporation to shareholders of integratable company
            foreach (var shareholderId in array)
            {
                var percentOfSharesInPreviousCompany = GetShareSize(gameContext, companyId, shareholderId);

                var newShare = emitedShares * percentOfSharesInPreviousCompany / 100;

                AddShares(gameContext, corporation, shareholderId, (int)newShare);
                Debug.Log($"investor {GetInvestorName(gameContext, shareholderId)} will get {(int)newShare} shares of corporation {corporation.company.Name}");
            }


            foreach (var shareholderId in array)
            {
                RemoveShareholder(target, shareholderId);
            }
            AddShareholder(gameContext, companyId, buyerInvestorId, 100);
            target.isIndependentCompany = false;

            NotifyAboutCorporateAcquisition(gameContext, buyerInvestorId, companyId);
        }
    }
}
