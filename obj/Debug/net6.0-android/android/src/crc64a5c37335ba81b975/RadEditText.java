package crc64a5c37335ba81b975;


public class RadEditText
	extends android.widget.EditText
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getTextSize:()F:GetGetTextSizeHandler\n" +
			"n_setTextSize:(F)V:GetSetTextSize_FHandler\n" +
			"n_onSizeChanged:(IIII)V:GetOnSizeChanged_IIIIHandler\n" +
			"n_onBeginBatchEdit:()V:GetOnBeginBatchEditHandler\n" +
			"n_onEndBatchEdit:()V:GetOnEndBatchEditHandler\n" +
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onTextChanged:(Ljava/lang/CharSequence;III)V:GetOnTextChanged_Ljava_lang_CharSequence_IIIHandler\n" +
			"n_onFocusChanged:(ZILandroid/graphics/Rect;)V:GetOnFocusChanged_ZILandroid_graphics_Rect_Handler\n" +
			"n_onSelectionChanged:(II)V:GetOnSelectionChanged_IIHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.InputElement.RadEditText, Telerik.Maui.Core", RadEditText.class, __md_methods);
	}


	public RadEditText (android.content.Context p0)
	{
		super (p0);
		if (getClass () == RadEditText.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.InputElement.RadEditText, Telerik.Maui.Core", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public RadEditText (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == RadEditText.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.InputElement.RadEditText, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public RadEditText (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == RadEditText.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.InputElement.RadEditText, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public RadEditText (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == RadEditText.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.InputElement.RadEditText, Telerik.Maui.Core", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, System.Private.CoreLib:System.Int32, System.Private.CoreLib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public float getTextSize ()
	{
		return n_getTextSize ();
	}

	private native float n_getTextSize ();


	public void setTextSize (float p0)
	{
		n_setTextSize (p0);
	}

	private native void n_setTextSize (float p0);


	public void onSizeChanged (int p0, int p1, int p2, int p3)
	{
		n_onSizeChanged (p0, p1, p2, p3);
	}

	private native void n_onSizeChanged (int p0, int p1, int p2, int p3);


	public void onBeginBatchEdit ()
	{
		n_onBeginBatchEdit ();
	}

	private native void n_onBeginBatchEdit ();


	public void onEndBatchEdit ()
	{
		n_onEndBatchEdit ();
	}

	private native void n_onEndBatchEdit ();


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);


	public void onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3)
	{
		n_onTextChanged (p0, p1, p2, p3);
	}

	private native void n_onTextChanged (java.lang.CharSequence p0, int p1, int p2, int p3);


	public void onFocusChanged (boolean p0, int p1, android.graphics.Rect p2)
	{
		n_onFocusChanged (p0, p1, p2);
	}

	private native void n_onFocusChanged (boolean p0, int p1, android.graphics.Rect p2);


	public void onSelectionChanged (int p0, int p1)
	{
		n_onSelectionChanged (p0, p1);
	}

	private native void n_onSelectionChanged (int p0, int p1);

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
