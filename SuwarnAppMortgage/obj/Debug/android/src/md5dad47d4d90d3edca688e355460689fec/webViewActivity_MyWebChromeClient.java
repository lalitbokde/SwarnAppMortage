package md5dad47d4d90d3edca688e355460689fec;


public class webViewActivity_MyWebChromeClient
	extends android.webkit.WebChromeClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGeolocationPermissionsShowPrompt:(Ljava/lang/String;Landroid/webkit/GeolocationPermissions$Callback;)V:GetOnGeolocationPermissionsShowPrompt_Ljava_lang_String_Landroid_webkit_GeolocationPermissions_Callback_Handler\n" +
			"";
		mono.android.Runtime.register ("SuwarnAppMortgage.webViewActivity+MyWebChromeClient, SuwarnAppMortgage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", webViewActivity_MyWebChromeClient.class, __md_methods);
	}


	public webViewActivity_MyWebChromeClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == webViewActivity_MyWebChromeClient.class)
			mono.android.TypeManager.Activate ("SuwarnAppMortgage.webViewActivity+MyWebChromeClient, SuwarnAppMortgage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public webViewActivity_MyWebChromeClient (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == webViewActivity_MyWebChromeClient.class)
			mono.android.TypeManager.Activate ("SuwarnAppMortgage.webViewActivity+MyWebChromeClient, SuwarnAppMortgage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onGeolocationPermissionsShowPrompt (java.lang.String p0, android.webkit.GeolocationPermissions.Callback p1)
	{
		n_onGeolocationPermissionsShowPrompt (p0, p1);
	}

	private native void n_onGeolocationPermissionsShowPrompt (java.lang.String p0, android.webkit.GeolocationPermissions.Callback p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
