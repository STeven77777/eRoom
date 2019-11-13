using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MetroOil.LoyaltyOps.Helpers
{
    public static class CustomInputExtensions
    {
        public static MvcHtmlString CustomNgTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object lblHtmlAttributes = null, object txtHtmlAttributes = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);

            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." + additionalModel + GetPropertyName(expression));

            if (txtRouteValueDictionary.ContainsKey("class"))
                txtRouteValueDictionary["class"] = txtRouteValueDictionary["class"] + " form-control";
            else
                txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary.Add("autocomplete", "off");

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metaData.IsRequired)
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }

            //if (txtRouteValueDictionary.ContainsKey("help"))
            //{
            //    Label = Label.Insert(Label.IndexOf("</label>"), "<i class='help material-icons'>help_outline</i>");1
            //}
            var _HelpText = string.Empty;
            if (txtRouteValueDictionary["helpText"] != null)
            {
                _HelpText = txtRouteValueDictionary["helpText"].ToString();
                //_HelpText = "<span class='help-text'>" + txtRouteValueDictionary["helpText"] + "</span>";
            }

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, txtRouteValueDictionary);
            if (txtRouteValueDictionary.ContainsKey("date_time"))
            {
                textBox = GetDivDateInput(textBox.ToHtmlString());
            }

            //var helpInfoValue = "";
            //if (txtRouteValueDictionary.ContainsKey("helpInfo"))
            //{
            //    helpInfoValue = txtRouteValueDictionary["helpInfo"].ToString();
            //}
            if (txtRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(textBox.ToString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(textBox.ToHtmlString(), validationspan.ToString(), Label, true, _HelpText);
            }
        }

        public static MvcHtmlString CustomNgDateRangeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression1, Expression<Func<TModel, TProperty>> expression2, string LabelText, object lblHtmlAttributes = null, object txtHtmlAttributes1 = null, object txtHtmlAttributes2 = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression1, LabelText, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes1);
            var txtRouteValueDictionary2 = new RouteValueDictionary(txtHtmlAttributes2);

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." + additionalModel + GetPropertyName(expression1));
            txtRouteValueDictionary2.Add("ng-model", "_Object." + additionalModel + GetPropertyName(expression2));
            txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary2.Add("class", "form-control");


            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-daterange input-group");
            //wrapper.MergeAttribute("date-range", "");

            //var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression1);
            //var ValidationMessage2 = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression2);
            var ValidationMessageCustom = new TagBuilder("span");
            ValidationMessageCustom.AddCssClass("message-custom");

            //var validationspan = new TagBuilder("span");
            //validationspan.AddCssClass("help-block");
            //validationspan.InnerHtml = ValidationMessage.ToHtmlString() + ValidationMessage2.ToHtmlString();

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression1, htmlHelper.ViewData);
            //if (lblRouteValueDictionary.ContainsKey("required"))
            if(metaData.IsRequired)
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }

            //if (NotAuthorized(htmlHelper, expression1, txtRouteValueDictionary))
            //{
            //    wrapper.InnerHtml = System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression1, txtRouteValueDictionary).ToHtmlString();
            //}
            //else
            //{
            //    if (IsReadOnly(htmlHelper, expression1, txtRouteValueDictionary))
            //        txtRouteValueDictionary["readonly"] = "readonly";

                var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression1, txtRouteValueDictionary);
                wrapper.InnerHtml = textBox.ToHtmlString();
            //}
            wrapper.InnerHtml += " <span class='input-group-addon'>to</span>";
            //if (NotAuthorized(htmlHelper, expression2, txtRouteValueDictionary))
            //{
            //    wrapper.InnerHtml += System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression2, txtRouteValueDictionary).ToHtmlString();
            //}
            //else
            //{
            //    if (IsReadOnly(htmlHelper, expression2, txtRouteValueDictionary))
            //        txtRouteValueDictionary2["readonly"] = "readonly";
                var textBox2 = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression2, txtRouteValueDictionary2);
                wrapper.InnerHtml += textBox2.ToHtmlString();
            //}

            var final = WrapinFormGroup(wrapper.ToString(), /*validationspan.ToString() +*/ ValidationMessageCustom.ToString(), Label);
            return final;
        }

        public static MvcHtmlString CustomNgTextRangeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression1, Expression<Func<TModel, TProperty>> expression2, string LabelText, object lblHtmlAttributes = null, object txtHtmlAttributes1 = null, object txtHtmlAttributes2 = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression1, LabelText, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes1);
            var txtRouteValueDictionary2 = new RouteValueDictionary(txtHtmlAttributes2);
            txtRouteValueDictionary.Add("ng-model", "_Object." + GetPropertyName(expression1));
            txtRouteValueDictionary2.Add("ng-model", "_Object." + GetPropertyName(expression2));
            txtRouteValueDictionary.Add("class", "form-control");
            txtRouteValueDictionary2.Add("class", "form-control");


            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("input-daterange input-group");
            wrapper.MergeAttribute("date-picker", "");

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression1);
            var ValidationMessage2 = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression2);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString() + ValidationMessage2.ToHtmlString();


            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression1, htmlHelper.ViewData);
            //if (lblRouteValueDictionary.ContainsKey("required"))
            if (metaData.IsRequired)
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }

            //if (NotAuthorized(htmlHelper, expression1, txtRouteValueDictionary))
            //{
            //    wrapper.InnerHtml = System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression1, txtRouteValueDictionary).ToHtmlString();
            //}
           // else
            //{
               // if (IsReadOnly(htmlHelper, expression1, txtRouteValueDictionary))
                 //   txtRouteValueDictionary["readonly"] = "readonly";

                var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression1, txtRouteValueDictionary);
                wrapper.InnerHtml = textBox.ToHtmlString();
           // }
            wrapper.InnerHtml += " <span class='input-group-addon'>to</span>";
            //if (NotAuthorized(htmlHelper, expression2, txtRouteValueDictionary))
            //{
            //    wrapper.InnerHtml += System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression2, txtRouteValueDictionary).ToHtmlString();
            //}
            //else
            //{
                //if (IsReadOnly(htmlHelper, expression2, txtRouteValueDictionary))
                //    txtRouteValueDictionary2["readonly"] = "readonly";
                var textBox2 = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression2, txtRouteValueDictionary2);
                wrapper.InnerHtml += textBox2.ToHtmlString();
            //}

            var final = WrapinFormGroup(wrapper.ToString(), validationspan.ToString(), Label);
            return final;
        }

        public static MvcHtmlString CustomNgTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object lblHtmlAttributes = null, object txtHtmlAttributes = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var txtRouteValueDictionary = new RouteValueDictionary(txtHtmlAttributes);

            //if (NotAuthorized(htmlHelper, expression, txtRouteValueDictionary))
            //{
            //    return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, txtRouteValueDictionary);
            //}
            //else
            //{
            //    if (IsReadOnly(htmlHelper, expression, txtRouteValueDictionary))
            //        txtRouteValueDictionary["readonly"] = "readonly";
            //}

            additionalModel = additionalModel == null ? null : additionalModel + ".";
            txtRouteValueDictionary.Add("ng-model", "_Object." + additionalModel + GetPropertyName(expression));
            txtRouteValueDictionary.Add("class", "form-control");
            var textBox = System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(htmlHelper, expression, txtRouteValueDictionary);

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //if (lblRouteValueDictionary.ContainsKey("required"))
            if (metaData.IsRequired)
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }


            if (txtRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(textBox.ToHtmlString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(textBox.ToHtmlString(), validationspan.ToString(), Label);
            }
        }

        public static MvcHtmlString CustomNgSelectListForOld<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, object selectChoiceOptions = null, string Context = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);
            var choicesRouteValueDictionary = new RouteValueDictionary(selectChoiceOptions);

            var expressionName = GetPropertyName(expression);
            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            //validationspan.InnerHtml = "<span class='field-validation-valid' data-valmsg-for='" + selectRouteValueDictionary["Name"].ToString() + "' data-valmsg-replace='true'></span>";
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();
            var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);
            
            var uiSelect = new TagBuilder("ui-select");
            additionalModel = additionalModel == null ? null : additionalModel + ".";
            uiSelect.MergeAttribute("ng-model", "_Object." + additionalModel + expressionName);
            //add css to select2 or selectize
            uiSelect.MergeAttribute("theme", "selectize");
            uiSelect.MergeAttribute("style", "width:100%");
            uiSelect.MergeAttributes(dictionary);
            var uiSelectmatch = new TagBuilder("ui-select-match");
            uiSelectmatch.InnerHtml = "{{$select.selected.Text}}";

            var uiSelectChoices = new TagBuilder("ui-select-choices");
            uiSelectChoices.MergeAttributes(choicesRouteValueDictionary);
            uiSelectChoices.MergeAttribute("value", "{{$select.selected.Value}}");
            uiSelectChoices.MergeAttribute("repeat", "item.Value as item in _Selects." + selectRouteValueDictionary["Name"] + "| filter: $select.search");
            var uiBindHtml = new TagBuilder("div");
            uiBindHtml.MergeAttribute("ng-bind-html", "item.Text | highlight: $select.search");
            uiBindHtml.MergeAttribute("ng-bind-html", "item.Text");


            uiSelect.InnerHtml = uiSelectmatch.ToString();
            uiSelectChoices.InnerHtml = uiBindHtml.ToString();
            uiSelect.InnerHtml += uiSelectChoices.ToString();

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //if (lblRouteValueDictionary.ContainsKey("required"))
            if (metaData.IsRequired)
            {
                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }

            if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(uiSelect.ToString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(uiSelect.ToString(), validationspan.ToString(), Label);
            }

        }

        public static MvcHtmlString CustomNgSelectListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> List_Item, object lblHtmlAttributes = null, object SelectHtmlAttributes = null, object selectChoiceOptions = null, string Context = null, string additionalModel = null)
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();

            var selectRouteValueDictionary = new RouteValueDictionary(SelectHtmlAttributes);
            var choicesRouteValueDictionary = new RouteValueDictionary(selectChoiceOptions);

            var expressionName = GetPropertyName(expression);

            var ValidationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(htmlHelper, expression);
            var validationspan = new TagBuilder("span");
            validationspan.AddCssClass("help-block");
            validationspan.InnerHtml = ValidationMessage.ToHtmlString();

            var dictionary = selectRouteValueDictionary.Select(p => new { p.Key, p.Value }).ToDictionary(p => p.Key, p => p.Value);

            if (dictionary.ContainsKey("class"))
                dictionary["class"] = dictionary["class"] + " form-control";
            else
                dictionary.Add("class", "form-control");

            var uiSelect = new TagBuilder("select");
            additionalModel = additionalModel == null ? null : additionalModel + ".";
            uiSelect.MergeAttribute("ng-model", "_Object." + additionalModel + expressionName);
            uiSelect.MergeAttribute("ng-options", "item.Value as item.Text for item in _Selects." + selectRouteValueDictionary["LstName"]);
            uiSelect.MergeAttribute("id", expressionName);
            uiSelect.MergeAttribute("name", expressionName);
            uiSelect.MergeAttributes(dictionary);

            // default value
            var defaultOption = new TagBuilder("option");
            defaultOption.MergeAttribute("value", "");
            if(selectRouteValueDictionary["PlaceholderTxt"] != null)
            {
                defaultOption.MergeAttribute("disabled", "disabled");
                defaultOption.InnerHtml = selectRouteValueDictionary["PlaceholderTxt"] + "";
            }
            if (selectRouteValueDictionary.ContainsKey("defaultOptLabel"))
            {
                defaultOption.MergeAttribute("class", "label-option");
                defaultOption.MergeAttribute("label", selectRouteValueDictionary["defaultOptLabel"] + "");
            }
            uiSelect.InnerHtml += defaultOption.ToString();

            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            
            if (metaData.IsRequired)
            {
                //"data-val="true" data-val-required="The First Name / Given Name field is required.""
                var labelText = Label.Substring(Label.IndexOf('>') + 1, Label.LastIndexOf('<') - Label.IndexOf('>') - 1);

                uiSelect.MergeAttribute("data-val", "true");
                uiSelect.MergeAttribute("data-val-required", "The " + labelText + " field is required.");

                Label = Label.Insert(Label.IndexOf("</label>"), "<span>(required)</span>");
            }

            if (selectRouteValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(uiSelect.ToString());
            }
            else
            {
                return WrapinFormGroup(uiSelect.ToString(), validationspan.ToString(), Label);
            }

        }

        public static MvcHtmlString CustomNgCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool?>> expression, object lblHtmlAttributes = null, object checkHtmlAttributes = null, string Context = null)//, object txtHtmlAttributes = null
        {
            var lblRouteValueDictionary = new RouteValueDictionary(lblHtmlAttributes);
            var CheckBoxRouteValueDictionary = new RouteValueDictionary(checkHtmlAttributes);
            var checkHtmlValueDictionary = new RouteValueDictionary(checkHtmlAttributes);
            var Label = System.Web.Mvc.Html.LabelExtensions.LabelFor(htmlHelper, expression, lblRouteValueDictionary).ToHtmlString();
            var expressionName = GetPropertyName(expression);

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var displayName = metadata.DisplayName;

            var controlGroupDiv = new TagBuilder("div");
            controlGroupDiv.AddCssClass("mt-checkbox-list");

            var label = new TagBuilder("label");
            label.AddCssClass("mt-checkbox mt-checkbox-outline");

            var checkBox = new TagBuilder("input");
            checkBox.Attributes.Add("type", "checkbox");
            checkBox.MergeAttributes(CheckBoxRouteValueDictionary);
            checkBox.Attributes.Add("ng-model", "_Object." + expressionName);
            if (CheckBoxRouteValueDictionary.ContainsKey("ng_disabled"))
            {
                checkBox.Attributes.Add("ng-disabled", CheckBoxRouteValueDictionary["ng_disabled"].ToString());
            }

            if (CheckBoxRouteValueDictionary.ContainsKey("ng_click"))
            {
                checkBox.Attributes.Add("ng-click", CheckBoxRouteValueDictionary["ng_click"].ToString());
            }

            //if (IsReadOnly(htmlHelper, expression, CheckBoxRouteValueDictionary))
            //{
            //    checkBox.Attributes.Add("disabled", "disabled");
            //}

            var validationspan = new TagBuilder("span");
            //var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(htmlHelper, expression, CheckBoxRouteValueDictionary);

            //if (NotAuthorized(htmlHelper, expression, CheckBoxRouteValueDictionary))
            //    return System.Web.Mvc.Html.InputExtensions.HiddenFor(htmlHelper, expression, CheckBoxRouteValueDictionary);


            label.InnerHtml = checkBox.ToString(TagRenderMode.SelfClosing);
            label.InnerHtml += htmlHelper.Encode(displayName);
            label.InnerHtml += "<span></span>";

            controlGroupDiv.InnerHtml = label.ToString();

            var helpInfoValue = "";
            if (checkHtmlValueDictionary.ContainsKey("helpInfo"))
            {
                helpInfoValue = checkHtmlValueDictionary["helpInfo"].ToString();
            }
            
            if (checkHtmlValueDictionary.ContainsKey("emptyWrapper"))
            {
                return MvcHtmlString.Create(controlGroupDiv.ToString() + validationspan.ToString());
            }
            else
            {
                return WrapinFormGroup(controlGroupDiv.ToString(), validationspan.ToString(), Label, false, helpInfoValue);
            }

            //return MvcHtmlString.Create(controlGroupDiv.ToString());
        }

        private static MvcHtmlString WrapinFormGroup(string inputHtml, string validationHtml, string Label, bool showLable = true, string helpInfo = "")
        {
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            if (showLable)
            {
                div.InnerHtml = Label;
            }
            
            div.InnerHtml += inputHtml;

            // Add helpInfo
            //<h6 class="help-block">Maximum 15 characters.</h6>
            if (!string.IsNullOrEmpty(helpInfo))
            {
                var helpInfoDiv = new TagBuilder("span");
                helpInfoDiv.AddCssClass("help-info");
                helpInfoDiv.SetInnerText(helpInfo);
                div.InnerHtml += helpInfoDiv;
            }
            div.InnerHtml += validationHtml;

            return MvcHtmlString.Create(div.ToString());
        }

        /// <summary>
        /// Wall: This function will build the html code for Date picker as below:
	    ///     <div class="input-group input-medium date date-picker" data-date-format="dd-mm-yyyy">
		/// 	    <input type="text" class="form-control">
		///  	    <span class="input-group-btn">
		/// 		    <button class="btn default" type="button">
		/// 			    <i class="fa fa-calendar"></i>
		/// 		    </button>
		/// 	    </span>
		///     </div>
        /// </summary>
        /// <param name="inputDiv"></param>
        /// <returns></returns>
        private static MvcHtmlString GetDivDateInput(string inputDiv)
        {
            var divInputGrp = new TagBuilder("div");
            divInputGrp.AddCssClass("input-icon right input-medium");

            string pickerDiv = "<i class='material-icons'>today</i>";
            divInputGrp.InnerHtml = pickerDiv + inputDiv;

            return MvcHtmlString.Create(divInputGrp.ToString());
        }

        private static string GetPropertyName<TModel, TProperty>(System.Linq.Expressions.Expression<Func<TModel, TProperty>> property)
        {
            System.Linq.Expressions.LambdaExpression lambda = (System.Linq.Expressions.LambdaExpression)property;
            System.Linq.Expressions.MemberExpression memberExpression;

            if (lambda.Body is System.Linq.Expressions.UnaryExpression)
            {
                System.Linq.Expressions.UnaryExpression unaryExpression = (System.Linq.Expressions.UnaryExpression)(lambda.Body);
                memberExpression = (System.Linq.Expressions.MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (System.Linq.Expressions.MemberExpression)(lambda.Body);
            }
            var xx = (PropertyInfo)memberExpression.Member;
            return ((PropertyInfo)memberExpression.Member).Name;
        }
    }
}