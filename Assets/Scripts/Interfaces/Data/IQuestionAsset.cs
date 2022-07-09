using System.Collections.Generic;
using Enums;

namespace Interfaces.Data
{
    public interface IQuestionAsset
    {
        TileChilds TileChilds { get; }
        string QuestionId { get; }
        IQuestionCategoryInfo CategoryInfo { get; }
        IQuestionInfo QuestionInfo { get; }
        IEnumerable<(TileChilds tileChildDirection, IQuestionAsset questionAsset)> QuestionChilds { get; }
    }
}