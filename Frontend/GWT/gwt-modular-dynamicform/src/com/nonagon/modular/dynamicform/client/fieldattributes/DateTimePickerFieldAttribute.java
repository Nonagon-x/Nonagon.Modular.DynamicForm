package com.nonagon.modular.dynamicform.client.fieldattributes;

import java.util.Date;

import com.google.gwt.core.client.JavaScriptObject;
import com.nonagon.modular.dynamicform.client.FieldAttribute;

public class DateTimePickerFieldAttribute extends FieldAttribute {

	public static DateTimePickerFieldAttribute create() {
		
		DateTimePickerFieldAttribute instance = ((DateTimePickerFieldAttribute)JavaScriptObject.createObject());
		instance.init();
		
		return instance;
	}
	
	protected final native void init()/*-{
		
		this.__type = "Nonagon.Modular.DynamicForm.FieldAttributes.DateTimePickerFieldAttribute, Nonagon.Modular.DynamicForm";
	}-*/;

	protected DateTimePickerFieldAttribute() {}
	
	public final native Date getMinDate()/*-{ return this.MinDate; }-*/;
	public final native void setMinDate(Date value)/*-{ this.MinDate = value; }-*/;
	
	public final native Date getMaxDate()/*-{ return this.MaxDate; }-*/;
	public final native void setMaxDate(Date value)/*-{ this.MaxDate = value; }-*/;
	
	public final native boolean getEnableMinRange()/*-{ return this.EnableMinRange; }-*/;
	public final native void setEnableMinRange(boolean value)/*-{ this.EnableMinRange = value; }-*/;
	
	public final native boolean getEnableMaxRange()/*-{ return this.EnableMaxRange; }-*/;
	public final native void setEnableMaxRange(boolean value)/*-{ this.EnableMaxRange = value; }-*/;
}
