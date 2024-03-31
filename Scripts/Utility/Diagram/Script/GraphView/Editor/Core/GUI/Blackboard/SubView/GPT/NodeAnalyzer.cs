using GPT;
using Share;

namespace Diagram
{
    public class NodeAnalyzer
    {
        private GPTBetaViewModel binder;
        private DiagramNodeModel nodeModel;
        
        public NodeAnalyzer(GPTBetaViewModel binder)
        {
            this.binder = binder;
        }
        
        public void SetNodeModel(DiagramNodeModel model)
        {
            nodeModel = model;
        }
        
        public void AnalyzeWith(string idea)
        {
            if (nodeModel.Header.SummarizeDescription.IsNullOrEmpty())
                return;
            
            AnalysisRequestModel request = new AnalysisRequestModel();
            
            request.name = nodeModel.Note.Title.IsNullOrEmptyThen(nodeModel.Header.Name);
            request.referenceNote = nodeModel.Header.SummarizeDescription;
            request.referenceIdea = idea;
            
            //binder.GetSummary()
        }
    }

}