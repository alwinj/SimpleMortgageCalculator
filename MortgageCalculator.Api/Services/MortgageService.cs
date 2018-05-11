using System.Linq;
using System.Collections.Generic;
using MortgageCalculator.Api.Repos;
using MortgageCalculator.Dto;
using System;

namespace MortgageCalculator.Api.Services
{
    public interface IMortgageService
    {
        IList<Mortgage> GetAllMortgages();
        Mortgage GetMortgage(int id);
        Mortgage CalculateMortgage(Mortgage mortgage);
    }

    public class MortgageService : IMortgageService
    {

        private readonly IMortgageRepo _mortgageRepo;
        public MortgageService() : this(new MortgageRepo())
        { }

        public MortgageService(IMortgageRepo mortgageRepo)
        {
            this._mortgageRepo = mortgageRepo;
        }

        public IList<Mortgage> GetAllMortgages()
        {
            return _mortgageRepo.GetAllMortgages();
        }

        public Mortgage GetMortgage(int id)
        {
            var mortgageList = _mortgageRepo.GetAllMortgages();

            if (mortgageList == null)
            {
                return null;
            }

            var mortgage = mortgageList.FirstOrDefault(x => x.MortgageId == id);

            if (mortgage == null)
            {
                return null;
            }

            return mortgage;
        }

        public Mortgage CalculateMortgage(Mortgage mortgage)
        {
            decimal totalInterestRepayment = 0;
            decimal totalLoanRepayment = 0;

            decimal noOfYears = mortgage.TermsInMonths / 12m;

            totalInterestRepayment = (mortgage.LoanAmount * (mortgage.InterestRate / 100m)) * noOfYears;
            totalLoanRepayment = mortgage.LoanAmount + totalInterestRepayment;

            mortgage.TotalLoanRepayment = Math.Round(totalLoanRepayment, 5);
            mortgage.TotalInterestRepayment = Math.Round(totalInterestRepayment, 5);

            return mortgage;
        }
    }
}