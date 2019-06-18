using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Xamarin.iOS.SignalR.Client
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override async void ViewDidLoad ()
        {
            
            base.ViewDidLoad ();
            var client = SignalRHubClient.Instance;
            try
            {
                await client.Connect();

                client.OnReceiveEvent += (userId, message) =>
                {
                    InvokeOnMainThread(() => {
                        lblMessage.Text = userId + ":" + message;
                    });
                   
                };
            }
            catch (Exception e)
            {
            }

        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }
    }
}