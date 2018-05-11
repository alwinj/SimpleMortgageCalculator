using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MortgageCalculator.Web.Models
{
    public class MortgageViewModel
    {
        [Required(ErrorMessage="Amount is required")]
        [DisplayName("Loan Amount")]
        [Range(1, 1000000, ErrorMessage = "Loan amount must be between {1} and {2}")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Loan Duration is required")]
        [DisplayName("Loan Duration")]
        [Range(1, 300, ErrorMessage="Loan duration must be between {1} and {2}")]
        public int TermsInMonths { get; set; }

        [DisplayName("Total Loan Repayment")]
        public decimal TotalLoanRepayment { get; set; }

        [DisplayName("Total Interest Repayment")]
        public decimal TotalInterestRepayment { get; set; }
    }
}