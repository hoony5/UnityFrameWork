namespace Diagram
{
    [System.Serializable]
    public class AnalysisResponseModel
    {
        public string name;
        // gpt output
        public string structure;
        public string role;
        public string recommendation;
        
        // specific output
        public string validationForPlan;
        public string codeReviewForSOLID;
        public string recommendationRefactoringCode;
        public string testCode;
        public string testProfilingCode;
        public string checkList;
    }
}