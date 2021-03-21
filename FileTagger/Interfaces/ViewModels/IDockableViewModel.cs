
namespace FileTagger.Interfaces.ViewModels
{
    public interface IDockableViewModel
    {
        string Title { get; }

        string ContentId { get; }

        bool CanClose { get; }

        bool IsSelected { get; set; }

        bool IsActive { get; set; }
    }
}
