namespace clickfree_Maui.Contracts.Services
{
    public interface IFolderPicker
    {
        Task<string> PickFolder();
    }
}