package crc64f0082c958c44238f;


public class BorderDrawable
	extends android.graphics.drawable.ColorDrawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Border.BorderDrawable, Telerik.Maui.Core", BorderDrawable.class, __md_methods);
	}


	public BorderDrawable ()
	{
		super ();
		if (getClass () == BorderDrawable.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Border.BorderDrawable, Telerik.Maui.Core", "", this, new java.lang.Object[] {  });
	}


	public BorderDrawable (int p0)
	{
		super (p0);
		if (getClass () == BorderDrawable.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Border.BorderDrawable, Telerik.Maui.Core", "Android.Graphics.Color, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public void draw (android.graphics.Canvas p0)
	{
		n_draw (p0);
	}

	private native void n_draw (android.graphics.Canvas p0);

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
