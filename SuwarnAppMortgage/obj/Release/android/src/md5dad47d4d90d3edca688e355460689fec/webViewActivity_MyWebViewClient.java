package md5dad47d4d90d3edca688e355460689fec;


public class webViewActivity_MyWebViewClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("SuwarnAppMortgage.webViewActivity+MyWebViewClient, SuwarnAppMortgage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", webViewActivity_MyWebViewClient.class, __md_methods);
	}


	public webViewActivity_MyWebViewClient () throws java.lang.Throwable
	{
		super ();
		if (getClass () == webViewActivity_MyWebViewClient.class)
			mono.android.TypeManager.Activate ("SuwarnAppMortgage.webViewActivity+MyWebViewClient, SuwarnAppMortgage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1)
	{
		return n_shouldOverrideUrlLoading (p0, p1);
	}

	private native boolean n_shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1);

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
