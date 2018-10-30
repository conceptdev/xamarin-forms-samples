using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using Plugin.Connectivity;
using System.IO;
using System.Diagnostics;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
namespace XamarinBorderlessEntry
{
    public partial class MainPage : ContentPage
    {

        //private readonly VisionServiceClient visionClient;
        public ImageSource SourceImage;
       EmotionServiceClient emotionClient;
        //public AnalysisResult Result;
        //MediaFile file;
        public MainPage()
        {
            InitializeComponent();
            imgBanner.Source = ImageSource.FromResource("XamarinBorderlessEntry.images.banner.png");
            //emotionClient = new EmotionServiceClient("63cef7c5ff04433da7924c7429cebd7c");
            //this.visionClient = new VisionServiceClient("bb39467297834f0c8af0c0d9f519cc98");
        }

        private async void btnTakeImage_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            try
            {
                

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await  DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                imgBanner.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
                var result=await GetImageDescription(file.GetStream());
                file.Dispose();



                foreach (string tag in result.Description.Tags)
                {
                    lblLanguage.Text = lblLanguage.Text + "\n" + tag;
                }


                //using (var stream = file.GetStream())
                //{
                //    var faceServiceClient = new FaceServiceClient("{FACE_API_SUBSCRIPTION_KEY}");

                //    // Step 4a - Detect the faces in this photo.
                //    var faces = await faceServiceClient.DetectAsync(stream);
                //    var faceIds = faces.Select(face => face.FaceId).ToArray();

                //    // Step 4b - Identify the person in the photo, based on the face.
                //    var results = await faceServiceClient.IdentifyAsync(personGroupId, faceIds);
                //    var result = results[0].Candidates[0].PersonId;

                //    // Step 4c - Fetch the person from the PersonId and display their name.
                //    var person = await faceServiceClient.GetPersonAsync(personGroupId, result);
                //    UserDialogs.Instance.ShowSuccess($"Person identified is {person.Name}.");
                //}
            }
            catch
            (Exception ex)
            {
                string test = ex.Message;
            }

        }

        private async void btnPickImage_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {

                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });


                if (file == null)
                    return;

                imgBanner.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    //file.Dispose();
                    return stream;
                });
                var result = await GetImageDescription(file.GetStream());
                file.Dispose();
                foreach (string tag in result.Description.Tags)
                {
                    lblLanguage.Text = lblLanguage.Text + "\n" + tag;
                }


            }
            catch
            (Exception ex)
            {
                string test = ex.Message;
            }
        }
        public async Task<AnalysisResult> GetImageDescription(Stream imageStream)
        {
            VisionServiceClient visionClient = new VisionServiceClient("a338648c0df347c6b3b9e46ea2022fcd", "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0");
            VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description };
            return await visionClient.AnalyzeImageAsync(imageStream, features.ToList(), null);
        }
        public async Task Recognizeemotion(MediaFile file)
        {
            try
            {
                if (file != null)
                {
                    using (var photoStream = file.GetStream())
                    {

                        Emotion[] emotionResult = await emotionClient.RecognizeAsync(photoStream);
                        if (emotionResult.Any())
                        {
                            // Emotions detected are happiness, sadness, surprise, anger, fear, contempt, disgust, or neutral.
                            lblLanguage.Text = emotionResult.FirstOrDefault().Scores.ToRankedList().FirstOrDefault().Key;
                            //emotion.IsVisible = true;
                        }
                        file.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        //public async Task<SpellCheckResult> SpellCheckTextAsync(string text)
        //{
        //    string requestUri = GenerateRequestUri(Constants.BingSpellCheckEndpoint, text, SpellCheckMode.Spell);
        //    var response = await SendRequestAsync(requestUri);
        //    var spellCheckResults = JsonConvert.DeserializeObject<SpellCheckResult>(response);
        //    return spellCheckResults;
        //}

        private void btnSpeak_Clicked(object sender, EventArgs e)
        {
            CrossTextToSpeech.Current.Speak(txtInput.Text);
        }
    }

}
