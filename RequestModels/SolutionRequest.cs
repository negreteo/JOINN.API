using System;

namespace JOINN.API.RequestModels
{
   public class SolutionRequest
   {
      public int ID { get; set; }
      public string ProjectType { get; set; }
      public string IBPML1 { get; set; }
      public string IBPML2 { get; set; }
      public string IBPML3 { get; set; }
      public bool InCMDB { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public string TryingToSolve { get; set; }
      public string Version { get; set; }
      public string ProjectStatus { get; set; }
      public string ITGuideline { get; set; }
      public string ProjectFunction { get; set; }
      public string FunctionRemarks { get; set; }
      public string BenefitType { get; set; }
      public string BenefitComments { get; set; }
      public string BenefitUnit { get; set; }
      public string BenefitValue { get; set; }
      public decimal InvestmentCost { get; set; }
      public decimal RealizedAnnualSavings { get; set; }
      public string CostCategory { get; set; }
      public string Location { get; set; }
      public string Region { get; set; }
      public string Site { get; set; }
      public string ProductOwner { get; set; }
      public string ProcessOwner { get; set; }
      public bool HasDocumentation { get; set; }
      public string MedicalValidation { get; set; }
      public string SolutionStatus { get; set; }
      public string FinalDecision { get; set; }
      public DateTime FinalDecisionDate { get; set; }
      public string Comments { get; set; }
      public string SubWorkStream { get; set; }
      public string IsPackageReady { get; set; }
      public bool AttendedAllRegions { get; set; }
      public string SimilarApp { get; set; }
      public string CreatedBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public string UpdatedBy { get; set; }
      public DateTime LastUpdate { get; set; }
   }
}
