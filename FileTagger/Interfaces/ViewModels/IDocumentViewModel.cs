
namespace FileTagger.Interfaces.ViewModels
{
    public interface IDocumentViewModel
    {
        string Title { get; }

        bool CanClose { get; }

        string ContentId { get; }

        bool IsActive { get; }

        bool IsSelected { get; }

        string ToolTip { get; }
    }
}
