package crc64b72313ee0f5a60c4;


public class AllCharsNumberKeyListener
	extends android.text.method.NumberKeyListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getInputType:()I:GetGetInputTypeHandler\n" +
			"n_filter:(Ljava/lang/CharSequence;IILandroid/text/Spanned;II)Ljava/lang/CharSequence;:GetFilter_Ljava_lang_CharSequence_IILandroid_text_Spanned_IIHandler\n" +
			"n_getAcceptedChars:()[C:GetGetAcceptedCharsHandler\n" +
			"";
		mono.android.Runtime.register ("Telerik.Maui.Platform.AllCharsNumberKeyListener, Telerik.Maui.Core", AllCharsNumberKeyListener.class, __md_methods);
	}


	public AllCharsNumberKeyListener ()
	{
		super ();
		if (getClass () == AllCharsNumberKeyListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.AllCharsNumberKeyListener, Telerik.Maui.Core", "", this, new java.lang.Object[] {  });
	}

	public AllCharsNumberKeyListener (int p0, char[] p1)
	{
		super ();
		if (getClass () == AllCharsNumberKeyListener.class)
			mono.android.TypeManager.Activate ("Telerik.Maui.Platform.AllCharsNumberKeyListener, Telerik.Maui.Core", "Android.Text.InputTypes, Mono.Android:System.Char[], System.Private.CoreLib", this, new java.lang.Object[] { p0, p1 });
	}


	public int getInputType ()
	{
		return n_getInputType ();
	}

	private native int n_getInputType ();


	public java.lang.CharSequence filter (java.lang.CharSequence p0, int p1, int p2, android.text.Spanned p3, int p4, int p5)
	{
		return n_filter (p0, p1, p2, p3, p4, p5);
	}

	private native java.lang.CharSequence n_filter (java.lang.CharSequence p0, int p1, int p2, android.text.Spanned p3, int p4, int p5);


	public char[] getAcceptedChars ()
	{
		return n_getAcceptedChars ();
	}

	private native char[] n_getAcceptedChars ();

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
