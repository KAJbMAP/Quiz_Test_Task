namespace Interfaces.Data
{
    public interface IQuestionsContainerAsset
    {
        IQuestionAsset RootQuestion { get; }
        IQuestionAsset[] AllQuestionAssets { get; }
    }
}