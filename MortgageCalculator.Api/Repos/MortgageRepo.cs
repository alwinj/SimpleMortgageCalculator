using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MortgageCalculator.Dto;
using AutoMapper;

namespace MortgageCalculator.Api.Repos
{
    public interface IMortgageRepo
    {
        IList<Mortgage> GetAllMortgages();
    }

    public class MortgageRepo : IMortgageRepo
    {
        public IList<Mortgage> GetAllMortgages()
        {
            using (var context = new MortgageData.MortgageDataContext())
            {
                var mortgages = context.Mortgages;

                if (mortgages == null)
                    return null;

                // Sort by mortgage type and interest rate
                var mortgageList = mortgages.OrderBy(x => x.MortgageType).ThenBy(x => x.InterestRate).ToList();

                List<Mortgage> result = new List<Mortgage>();
                foreach (var mortgage in mortgageList)
                {
                    MortgageCalculator.Dto.Mortgage obj = Mapper.Map<MortgageData.Mortgage, MortgageCalculator.Dto.Mortgage>(mortgage);
                    result.Add(obj);
                 
                    /* Removed the manual mapping of Model to DTO by using AutoMapper */
                    //result.Add(new Mortgage()
                    //    {
                    //        Name = mortgage.Name,
                    //        EffectiveStartDate = mortgage.EffectiveStartDate,
                    //        EffectiveEndDate = mortgage.EffectiveEndDate,
                    //        CancellationFee = mortgage.CancellationFee,
                    //        EstablishmentFee = mortgage.CancellationFee,
                    //        InterestRepayment = (InterestRepayment)Enum.Parse(typeof(InterestRepayment), mortgage.MortgageType.ToString()),
                    //        MortgageId = mortgage.MortgageId,
                    //        MortgageType = (MortgageType)Enum.Parse(typeof(MortgageType), mortgage.MortgageType.ToString()),
                    //        TermsInMonths = mortgage.TermsInMonths
                    //    }
                    //);
                }

                return result;
            }
        }
    }
}