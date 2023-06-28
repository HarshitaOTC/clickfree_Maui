package crc64b72313ee0f5a60c4;


public class RoundRectLayerDrawable
	extends android.graphics.drawable.LayerDrawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Platform.RoundRectLayerDrawable, Telerik.Maui.Core", RoundRectLayerDrawable.class, __md_methods);
	}


	public RoundRectLayerDrawable (android.graphics.drawable.Drawable[] p0)
	{
		super (p0);
		if (getClass () == RoundRectLayerDrawable.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.RoundRectLayerDrawable, Telerik.Maui.Core", "Android.Graphics.Drawables.Drawable[], Mono.Android", this, new java.lang.Object[] { p0 });
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
