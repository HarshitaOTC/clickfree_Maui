using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Security.Policy;
using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using clickfree_Maui.Properties;
using clickfree_Maui.Windows;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace clickfree_Maui.Instagram
{
    public class InstagramManager
    {

        public const string RedirectURL = "https://www.datalogixxmemory.com/";
        public const string AppID = "884907049081721";//884907049081721
        // public const string AppID = "2798931540324100"; // ClickFree
        public const string AppSecret = "0f2e10f5334fea4860aa2b35e2a2e286";
        public static string OAuthURL = "https://instagram.com/oauth/authorize?client_id=" + AppID + "&redirect_uri=" + RedirectURL + "&response_type=code&scope=user_profile,user_media";
        // public const string OAuthURL = "https://facebook.com/dialog/oauth?client_id= "+ AppID + "&redirect_uri=" + RedirectURL + "&scope=user_photos,user_videos&display=popup";
        public const string AccessTokenURL = "https://api.instagram.com/oauth/access_token";
        // https://api.instagram.com/oauth/access_token?client_id=884907049081721&redirect_uri=https://www.datalogixxmemory.com/&grant_type=authorization_code&redirect_uri=https://www.datalogixxmemory.com/&code=
        // public const string auth1 = "https://instagram.com/oauth/authorize?client_id=884907049081721&redirect_uri=https://www.datalogixxmemory.com&response_type=code&scope=user_profile,user_media";
        public const string GetPicturesURL = "https://graph.instagram.com/{MediaId}?fields=id,media_type,media_url,username,timestamp,thumbnail_url&access_token=";
        public const string GetChildPicturesURL = "https://graph.instagram.com/{MediaId}/children?fields=id,media_type,media_url,username,timestamp,thumbnail_url&access_token=";
        public const string GetPhotosURL = "https://graph.instagram.com/me/media";
        public const string GetVideosURL = "https://graph.facebook.com/v10.0/me/videos";
        public const string UserInfoURL = "https://graph.instagram.com/v14.0/me?fields=username&access_token=";
        public const string UserAlbumsURL = "https://graph.facebook.com/v10.0/me/albums?access_token=";
        public const string UserAlbumPhotosURL = "https://graph.facebook.com/v10.0/{0}/photos";
        
        private static async Task<AccessTokenResult> postrequest(string strURL, string strAuthCode)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
{
    { "client_id", AppID },
    { "client_secret", AppSecret },
    { "code",strAuthCode },
                {"grant_type","authorization_code" },
                 {"redirect_uri",RedirectURL }
};

            string url = "http://www.example.com";
            var data = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(strURL, data);
            
            return await response.Content.ReadAsAsync<AccessTokenResult>();

        }




        #region Properties

        public static WebException LastRequestException { get; private set; }

        #endregion

        #region Methods

        public static void Logout()
        {
          
               Settings.Default.InstagramCode = null;
               Settings.Default.InstagramAccessToken = null;
               Settings.Default.InstagramUserName = null;
               Settings.Default.Save();
        }

        public static Task<bool> GetAccessCode(string authCode)
        {
            return Task.Run<bool>(() =>
            {
                bool result = false;

                if (!string.IsNullOrWhiteSpace(authCode))
                {
                    try
                    {
                        AccessTokenResult accessToken = Task.Run(async () => {
                            return await postrequest(AccessTokenURL, authCode);
                        }).Result;
                        
                        // AccessTokenResult accessToken = postrequest(AccessTokenURL, authCode).Result;
                        if (accessToken != null && !string.IsNullOrWhiteSpace(accessToken.AccessToken))
                        {
                            Settings.Default.InstagramCode = authCode;
                            //commneted for get access token
                            Settings.Default.InstagramAccessToken = accessToken.AccessToken;

                            UserInfoResult userInfo = GetRequest<UserInfoResult>(UserInfoURL + accessToken.AccessToken);
                            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Name))
                            {
                                Settings.Default.InstagramUserName = userInfo.Name;
                                //Settings.Default.InstagramUserName = "";

                                result = true;
                            }

                            
                            Settings.Default.Save();
                        }
                    }
                    catch { /* add log*/}
                }

                return result;
            });
        }

        public static Task<MediaResult[]> LoadALLPhotos(CancellationTokenSource token = null)
        {
            return Task.Run<MediaResult[]>(() =>
            {
                List<MediaResult> result = new List<MediaResult>();
                try
                {
                    result.AddRange(LoadImages(GetPhotosURL, token));

                    //load photos from albums
                    //UserAlbumsContainerResult userAlbumsContainer = GetRequest<UserAlbumsContainerResult>(UserAlbumsURL + Settings.Default.InstagramAccessToken);
                    //if (userAlbumsContainer?.Albums != null && userAlbumsContainer.Albums.Length > 0)
                    //{
                    //    foreach (var album in userAlbumsContainer.Albums)
                    //    {
                    //        token?.Token.ThrowIfCancellationRequested();

                    //        result.AddRange(LoadImages(string.Format(UserAlbumPhotosURL, album.Id), token));
                    //    }
                    //}

                    //remove dublicates
                    List<string> dublicates = new List<string>();
                    foreach (var item in result.ToArray())
                    {
                        if (dublicates.Contains(item.Id))
                        {
                            result.Remove(item);
                            continue;
                        }

                        dublicates.Add(item.Id);
                    }
                }
                catch (OperationCanceledException) { throw; }
                catch { /* add log*/}
               
                return result.ToArray();
                result.Clear();
               
            });
        }

        //public static Task<MediaResult[]> LoadALLVideos(CancellationTokenSource token = null)
        //{
        //    return Task.Run<MediaResult[]>(() =>
        //    {
        //        List<MediaResult> result = new List<MediaResult>();
        //        try
        //        {
        //            result.AddRange(LoadVideos(token: token));
        //            result.AddRange(LoadVideos("uploaded", token));
        //        }
        //        catch (OperationCanceledException) { throw; }
        //        catch { /* add log*/}

        //        return result.ToArray();
        //    });
        //}

        public static bool CheckNetworkConnection()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                /* MessageBoxWindow.ShowMessageBox("No Internet connection", "Please connect to internet and try again.", MessageBoxWindow.MessageBoxType.Error);*/

                return false;
            }

            return true;
        }

        public static bool CheckAuthorization(bool showForm = true)
        {
            if (!CheckNetworkConnection())
                return false;

            bool login = true;

            if (!string.IsNullOrWhiteSpace(Settings.Default.InstagramAccessToken))
            {
                UserInfoResult userInfo = GetRequest<UserInfoResult>(UserInfoURL + Settings.Default.InstagramAccessToken);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Name))
                {
                    Settings.Default.InstagramUserName = userInfo.Name;
                    Settings.Default.Save();

                    login = false;
                }
            }
            if (showForm)
            {
                /* var result = InstagramLoginWindow.Show1(login);

                 if (result == InstagramLoginWindow.LoginState.LoggedOut)
                 {
                     //    return InstagramLoginWindow.Show(true) == InstagramLoginWindow.LoginState.Success;
                 }
                 else
                 {
                     return result == InstagramLoginWindow.LoginState.Success;
                 }*/
            }
            else if (login)
                return false;
            //return InstagramLoginWindow.Show1(true) == InstagramLoginWindow.LoginState.Success;
            return true;
        }
        public static bool CheckAuthorization1(INavigationService _navigationService, bool showForm = true, bool logout = false)
        {
            if (!CheckNetworkConnection())
                return false;

            bool login = true;
            if (!string.IsNullOrWhiteSpace(Settings.Default.InstagramAccessToken))
            {
                UserInfoResult userInfo = GetRequest<UserInfoResult>(UserInfoURL + Settings.Default.InstagramAccessToken);
                if (userInfo != null && !string.IsNullOrEmpty(userInfo.Name))
                {
                    Settings.Default.InstagramUserName = userInfo.Name;
                    Settings.Default.Save();
                    login = false;
                    logout = false;
                   
                }
            }
            if (showForm)
            {
                var result = InstagramLoginWindow.Show1(login, _navigationService,logout);

                if (result == InstagramLoginWindow.LoginState.LoggedOut)
                {
                    var result1 = InstagramLoginWindow.Show1(true, _navigationService) == InstagramLoginWindow.LoginState.Success;
                
                    return result1;
                }
                else
                {
                  
                    return result == InstagramLoginWindow.LoginState.Success;

                }

            }
            else if (login)
            {
               // return InstagramLoginWindow.Show1(true, _navigationService,logout) == InstagramLoginWindow.LoginState.Success;
                var result = InstagramLoginWindow.Show1(true, _navigationService, logout);
                if (result == InstagramLoginWindow.LoginState.LoggedOut)
                {
                    return InstagramLoginWindow.Show1(true, _navigationService, logout) == InstagramLoginWindow.LoginState.Success;
                }
                else
                {

                    return result == InstagramLoginWindow.LoginState.Success;

                }

            }
            else return true;
         }
        private static List<MediaResult> LoadImages(string url, CancellationTokenSource token = null)
        {
            List<MediaResult> result = new List<MediaResult>();
            try
            {
                string after = null;
                string getImages = $"{url}?fields=id,caption&access_token={Settings.Default.InstagramAccessToken}";

                do
                {
                    string request = getImages;
                    if (!string.IsNullOrWhiteSpace(after))
                        request += $"&after={after}";

                    UserImagesContainerResult resultContainer = GetRequest<UserImagesContainerResult>(request);
                    if (resultContainer != null && resultContainer.Images != null && resultContainer.Images.Length > 0)
                    {
                        foreach (var data in resultContainer.Images)
                        {
                            token?.Token.ThrowIfCancellationRequested();


                            //to access children
                            //code start
                            List<MediaResult> childresult = new List<MediaResult>();
                            childresult = LoadChildrenImages(GetChildPicturesURL.Replace("{MediaId}", data.Id) + Settings.Default.InstagramAccessToken, token);
                            if (childresult != null && childresult.Count > 0)
                            {
                                result.AddRange(childresult);
                            }
                            //code end
                            else
                            {
                                InstagramMediaResult resultMedia = GetRequest<InstagramMediaResult>(GetPicturesURL.Replace("{MediaId}", data.Id) + Settings.Default.InstagramAccessToken);
                                if (resultMedia != null)
                                {
                                    result.Add(new MediaResult()
                                    {
                                        Id = resultMedia.Id,
                                        Source = resultMedia.media_url,
                                        IsVideo = resultMedia.MediaType == "VIDEO" ? true : false,
                                        Thumbnail = resultMedia.MediaType == "VIDEO" ? resultMedia.thumbnail_url : resultMedia.media_url
                                    });
                                }
                            }
                        }

                        after = resultContainer.Paging?.Cursors?.After;
                    }
                    else after = null;
                }
                while (!string.IsNullOrWhiteSpace(after));
            }
            catch { /* add log*/}

            return result;
        }

        private static List<MediaResult> LoadChildrenImages(string url, CancellationTokenSource token = null)
        {
            List<MediaResult> result = new List<MediaResult>();
            try
            {
                string after = null;
                string getImages = $"{url}";

                do
                {
                    string request = getImages;
                    if (!string.IsNullOrWhiteSpace(after))
                        request += $"&after={after}";

                    UserImagesContainerResult resultContainer = GetRequest<UserImagesContainerResult>(request);
                    if (resultContainer != null && resultContainer.Images != null && resultContainer.Images.Length > 0)
                    {
                        foreach (var data in resultContainer.Images)
                        {
                            token?.Token.ThrowIfCancellationRequested();


                            InstagramMediaResult resultMedia = GetRequest<InstagramMediaResult>(GetPicturesURL.Replace("{MediaId}", data.Id) + Settings.Default.InstagramAccessToken);
                            if (resultMedia != null)
                            {
                                result.Add(new MediaResult()
                                {
                                    Id = resultMedia.Id,
                                    Source = resultMedia.media_url,
                                    IsVideo = resultMedia.MediaType == "VIDEO" ? true : false,
                                    Thumbnail = resultMedia.MediaType == "VIDEO" ? resultMedia.thumbnail_url : resultMedia.media_url
                                });
                            }
                        }

                        after = resultContainer.Paging?.Cursors?.After;
                    }
                    else after = null;
                }
                while (!string.IsNullOrWhiteSpace(after));
            }
            catch { /* add log*/}

            return result;
        }
        private static T GetRequest<T>(string url) where T : class
        {
            T result = null;

            try
            {
                LastRequestException = null;

#pragma warning disable SYSLIB0014 // Type or member is obsolete
                WebRequest request = WebRequest.Create(url);
#pragma warning restore SYSLIB0014 // Type or member is obsolete
                request.Method = "GET";


                using (WebResponse response = request.GetResponse())
                {
                    using (var dataStream = response.GetResponseStream())
                    {
                        result = SerializationManager.Deserialize<T>(dataStream);
                    }
                }
            }
            catch (WebException exception)
            {
                LastRequestException = exception;
            }
            catch { }

            return result;
        }


        #endregion

        #region Private methods

        //private static List<MediaResult> LoadVideos(string type = "tagged"/*uploaded*/, CancellationTokenSource token = null)
        //{
        //    List<MediaResult> result = new List<MediaResult>();
        //    try
        //    {
        //        string after = null;
        //        string getImages = $"{GetVideosURL}?fields=source,thumbnails&type={type}&access_token={Settings.Default.InstagramAccessToken}";

        //        do
        //        {
        //            string request = getImages;
        //            if (!string.IsNullOrWhiteSpace(after))
        //                request += $"&after={after}";

        //            UserVideosContainerResult resultContainer = GetRequest<UserVideosContainerResult>(request);
        //            if (resultContainer != null && resultContainer.Videos != null && resultContainer.Videos.Length > 0)
        //            {
        //                foreach (var data in resultContainer.Videos)
        //                {
        //                    token?.Token.ThrowIfCancellationRequested();

        //                    if (data != null && !string.IsNullOrEmpty(data.Source) && data.Thumbnails?.Data != null && data.Thumbnails.Data.Length > 0)
        //                    {
        //                        result.Add(new MediaResult()
        //                        {
        //                            Id = data.Id,
        //                            Source = data.Source,
        //                            Thumbnail = data.Thumbnails.Data?.FirstOrDefault()?.Uri,
        //                            IsVideo = true
        //                        });
        //                    }
        //                }

        //                after = resultContainer.Paging?.Cursors?.After;
        //            }
        //            else after = null;
        //        }
        //        while (!string.IsNullOrWhiteSpace(after));
        //    }
        //    catch { /* add log*/}

        //    return result;
        //}



        [DataContract]
        public class InstagramMediaResult
        {
            #region Properties

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "media_type")]
            public string MediaType { get; set; }

            [DataMember(Name = "media_url")]
            public string media_url { get; set; }

            [DataMember(Name = "username")]
            public string username { get; set; }

            [DataMember(Name = "timestamp")]
            public string timestamp { get; set; }

            [DataMember(Name = "thumbnail_url")]
            public string thumbnail_url { get; set; }
            #endregion
        }



        #region Nested types

        [DataContract]
        public class AccessTokenResult
        {
            #region Properties

            [DataMember(Name = "access_token")]
            public string AccessToken { get; set; }

            [DataMember(Name = "user_id")]
            public string user_id { get; set; }

            #endregion
        }

        [DataContract]
        public class UserInfoResult
        {
            #region Properties

            [DataMember(Name = "username")]
            public string Name { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }

            #endregion
        }




        [DataContract]
        public class UserImagesContainerResult
        {
            #region Properties

            [DataMember(Name = "data")]
            public ImageData[] Images { get; set; }

            [DataMember(Name = "paging")]
            public PagingResult Paging { get; set; }
            #endregion

            #region Nested types

            [DataContract]
            public class ImageData
            {
                #region Properties

                //[DataMember(Name = "album")]
                //public AlbumInfoResult Album { get; set; }
                //[DataMember(Name = "images")]
                //public UserImageResult[] Images { get; set; }
                [DataMember(Name = "id")]
                public string Id { get; set; }

                [DataMember(Name = "caption")]
                public string caption { get; set; }

                #endregion
            }

            #endregion
        }

        public interface IInstagramMediaResult
        {
            string Source { get; set; }
            string Id { get; set; }
        }

        public interface IInstagramImageResult : IInstagramMediaResult
        {
        }

        public interface IInstagramVideoResult : IInstagramMediaResult
        {
        }

        public class MediaResult
        {
            public string Thumbnail { get; set; }
            public string Source { get; set; }
            public string Id { get; set; }
            public bool IsVideo { get; set; }
        }

        [DataContract]
        public class UserImageResult : IInstagramImageResult
        {
            #region Properties

            [DataMember(Name = "height")]
            public int Height { get; set; }
            [DataMember(Name = "width")]
            public int Width { get; set; }
            [DataMember(Name = "source")]
            public string Source { get; set; }
            [IgnoreDataMember]
            public string Id { get; set; }

            #endregion
        }

        [DataContract]
        public class UserAlbumsContainerResult
        {
            #region Properties

            [DataMember(Name = "data")]
            public AlbumInfoResult[] Albums { get; set; }

            [DataMember(Name = "paging")]
            public PagingResult Paging { get; set; }

            #endregion
        }

        [DataContract]
        public class AlbumInfoResult
        {
            #region Properties

            [DataMember(Name = "id")]
            public string Id { get; set; }
            [DataMember(Name = "name")]
            public string Name { get; set; }
            [DataMember(Name = "created_time")]
            public string CreatedTime { get; set; }

            #endregion
        }

        [DataContract]
        public class PagingResult
        {
            #region Properties

            [DataMember(Name = "cursors")]
            public CursorsResult Cursors { get; set; }
            [DataMember(Name = "previous")]
            public string Previous { get; set; }
            [DataMember(Name = "next")]
            public string Next { get; set; }

            #endregion

            #region Nested types

            [DataContract]
            public class CursorsResult
            {
                #region Properties

                [DataMember(Name = "before")]
                public string Before { get; set; }
                [DataMember(Name = "after")]
                public string After { get; set; }

                #endregion
            }

            #endregion
        }

        [DataContract]
        public class UserVideosContainerResult
        {
            #region Properties

            [DataMember(Name = "data")]
            public UserVideoResult[] Videos { get; set; }

            [DataMember(Name = "paging")]
            public PagingResult Paging { get; set; }

            #endregion
        }

        [DataContract]
        public class UserVideoResult : IInstagramVideoResult
        {
            #region Properties

            [DataMember(Name = "source")]
            public string Source { get; set; }
            [DataMember(Name = "thumbnails")]
            public VideoThumbnailContainerResult Thumbnails { get; set; }
            [DataMember(Name = "id")]
            public string Id { get; set; }
            #endregion

            #region Nested types

            [DataContract]
            public class VideoThumbnailContainerResult
            {
                #region Properties

                [DataMember(Name = "data")]
                public VideoThumbnailResult[] Data { get; set; }

                #endregion
            }

            [DataContract]
            public class VideoThumbnailResult
            {
                #region Properties

                [DataMember(Name = "id")]
                public string Id { get; set; }
                [DataMember(Name = "scale")]
                public float Scale { get; set; }
                [DataMember(Name = "height")]
                public int Height { get; set; }
                [DataMember(Name = "width")]
                public int Width { get; set; }
                [DataMember(Name = "uri")]
                public string Uri { get; set; }
                [DataMember(Name = "is_preferred")]
                public bool IsPreferred { get; set; }

                #endregion
            }

            #endregion
        }

        #endregion
    }
    #endregion
}

