package crc6444abc9571be52d08;


public class RadPopup_ScrollListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnScrollChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollChanged:()V:GetOnScrollChangedHandler:Android.Views.ViewTreeObserver/IOnScrollChangedListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.RadPopup+ScrollListener, Telerik.Maui.Controls", RadPopup_ScrollListener.class, __md_methods);
	}


	public RadPopup_ScrollListener ()
	{
		super ();
		if (getClass () == RadPopup_ScrollListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.RadPopup+ScrollListener, Telerik.Maui.Controls", "", this, new java.lang.Object[] {  });
	}


	public void onScrollChanged ()
	{
		n_onScrollChanged ();
	}

	private native void n_onScrollChanged ();

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
