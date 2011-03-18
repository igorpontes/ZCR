<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs"
    Inherits="GED_TCESE.WebForm4" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
form.cmxform fieldset {
  margin-bottom: 10px;
}
form.cmxform legend {
  padding: 0 2px;
  font-weight: bold;
}
form.cmxform label {
  display: inline-block;
  line-height: 1.8;
  vertical-align: top;
}
form.cmxform fieldset ol {
  margin: 0;
  padding: 0;
}
form.cmxform fieldset li {
  list-style: none;
  padding: 5px;
  margin: 0;
}
form.cmxform fieldset fieldset {
  border: none;
  margin: 3px 0 0;
}
form.cmxform fieldset fieldset legend {
  padding: 0 0 5px;
  font-weight: normal;
}
form.cmxform fieldset fieldset label {
  display: block;
  width: auto;
}
form.cmxform em {
  font-weight: bold;
  font-style: normal;
  color: #f00;
}
form.cmxform label {
  width: 120px; /* Width of labels */
}
form.cmxform fieldset fieldset label {
  margin-left: 123px; /* Width plus 3 (html space) */
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <legend>Delivery Details</legend>
        <ol>
            <li>
                <label for="name">
                    Name<em>*</em></label>
                <input id="name" />
            </li>
            <li>
                <label for="address1">
                    Address<em>*</em></label>
                <input id="address1" />
            </li>
            <li>
                <label for="address2">
                    Address 2</label>
                <input id="address2" />
            </li>
            <li>
                <label for="town-city">
                    Town/City</label>
                <input id="town-city" />
            </li>
            <li>
                <label for="county">
                    County<em>*</em></label>
                <input id="county" />
            </li>
            <li>
                <label for="postcode">
                    Postcode<em>*</em></label>
                <input id="postcode" />
            </li>
            <li>
                <fieldset>
                    <legend>Is this address also your invoice » address?<em>*</em></legend>
                    <label>
                        <input type="radio" » name="invoice-address" />
                        Yes</label>
                    <label>
                        <input type="radio" » name="invoice-address" />
                        No</label>
                </fieldset>
            </li>
        </ol>
    </fieldset>
</asp:Content>
