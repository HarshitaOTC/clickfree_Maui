package crc643961f13c85a497f7;


public class ConvertibleObjectNotify_1
	extends crc643961f13c85a497f7.ConvertibleObject_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Telerik.Maui.Controls.Compatibility.Common.Android.ConvertibleObjectNotify`1, Telerik.Maui.Controls.Compatibility", ConvertibleObjectNotify_1.class, __md_methods);
	}


	public ConvertibleObjectNotify_1 ()
	{
		super ();
		if (getClass () == ConvertibleObjectNotify_1.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Controls.Compatibility.Common.Android.ConvertibleObjectNotify`1, Telerik.Maui.Controls.Compatibility", "", this, new java.lang.Object[] {  });
	}

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
