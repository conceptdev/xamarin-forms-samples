using Android.Speech.Tts;
using Xamarin.Forms;
using System.Collections.Generic;
using Java.Lang;
using TodoMvvm;

[assembly: Dependency (typeof (Speech))]

namespace TodoMvvm
{
	public class Speech : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
	{
		TextToSpeech speaker;
		string toSpeak;
		public Speech ()
		{
			/*
System.Diagnostics.Debugger.Mono_UnhandledException (ex={Java.Lang.NoClassDefFoundError: Exception of type 'Java.Lang.NoClassDefFoundError' was thrown.
  at Android.Runtime.JNIEnv.FindClass (System.String classname) [0x00087] in /Users/builder/data/lanes/monodroid-mlion-monodroid-4.12-series/b5dc5ce9/source/monodroid/src/Mono.Android/src/Runtime/JNIEnv.cs:396 
  at Android.Runtime.JNIEnv.FindClass (System.Type type) [0x00009] in /Users/builder/data/lanes/monodroid-mlion-monodroid-4.12-series/b5dc5ce9/source/monodroid/src/Mono.Android/src/Runtime/JNIEnv.cs:352 
  --- End of managed exception stack trace ---
java.lang.NoClassDefFoundError: todomvvm/Speech
	at todomvvm.Activity1.n_onCreate(Native Method)
	at todomvvm.Activity1.onCreate(Activity1.java:28)
	at android.app.Activity.performCreate(Activity.java:5104)
	at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1080)
	at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2144)
	at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2230)
	at android.app.ActivityThread.access$600(ActivityThread.java:141)
	at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1234)
	at android.os.Handler.dispatchMessage(Handler.java:99)
	at android.os.Looper.loop(Looper.java:137)
	at android.app.ActivityThread.main(ActivityThread.java:5039)
	at java.lang.reflect.Method.invokeNative(Native Method)
	at java.lang.reflect.Method.invoke(Method.java:511)
	at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:793)
	at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:560)
	at dalvik.system.NativeStart.main(Native Method)
Caused by: java.lang.ClassNotFoundException: Didn't find class "todomvvm.Speech" on path: /data/app/TodoMvvm.Android-1.apk
	at dalvik.system.BaseDexClassLoader.findClass(BaseDexClassLoader.java:65)
	at java.lang.ClassLoader.loadClass(ClassLoader.java:501)
	at java.lang.ClassLoader.loadClass(ClassLoader.java:461)
	... 16 more
}) in 
			*/
		}

		public void Speak (string text)
		{
			var c = Forms.Context; 
			toSpeak = text;
			if (speaker == null)
				speaker = new TextToSpeech (c, this);
			else
			{
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
			}
		}


		#region IOnInitListener implementation
		public void OnInit (OperationResult status)
		{
			if (status.Equals (OperationResult.Success)) {
				System.Diagnostics.Debug.WriteLine ("spoke");
				var p = new Dictionary<string,string> ();
				speaker.Speak (toSpeak, QueueMode.Flush, p);
			} else {
				System.Diagnostics.Debug.WriteLine ("was quiet");
			}
		}
		#endregion
	}
}
