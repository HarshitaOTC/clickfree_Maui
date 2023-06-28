package crc64b72313ee0f5a60c4;


public class RadMauiEntry
	extends com.google.android.material.textfield.TextInputLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getTypeface:()Landroid/graphics/Typeface;:GetGetTypefaceHandler\n" +
			"n_setTypeface:(Landroid/graphics/Typeface;)V:GetSetTypeface_Landroid_graphics_Typeface_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Platform.RadMauiEntry, Telerik.Maui.Core", RadMauiEntry.class, __md_methods);
	}


	public RadMauiEntry (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadMauiEntry.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiEntry, Telerik.Maui.Core", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RadMauiEntry (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadMauiEntry.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiEntry, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RadMauiEntry (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RadMauiEntry.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RadMauiEntry, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public android.graphics.Typeface getTypeface ()
	{
		return n_getTypeface ();
	}

	private native android.graphics.Typeface n_getTypeface ();


	public void setTypeface (android.graphics.Typeface p0)
	{
		n_setTypeface (p0);
	}

	private native void n_setTypeface (android.graphics.Typeface p0);

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
