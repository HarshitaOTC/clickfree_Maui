using clickfree_Maui.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;

namespace clickfree_Maui.Platforms.Windows
{
    public class FolderPicker : IFolderPicker
    {
        public Task<string> PickFolder()
        {
            throw new NotImplementedException();
        }

        //async Task PickFolder(CancellationToken cancellationToken)
        //{
        //    var result = await FolderPicker.Default.PickAsync(cancellationToken);
        //    if (result.IsSuccessful)
        //    {
        //        await Toast.Make($"The folder was picked: Name - {result.Folder.Name}, Path - {result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
        //    }
        //    else
        //    {
        //        await Toast.Make($"The folder was not picked with error: {result.Exception.Message}").Show(cancellationToken);
        //    }
        //}
    }
}
