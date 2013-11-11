<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Fehler - Meine ASP.NET MVC-Anwendung
</asp:Content>

<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup class="title">
        <h1 class="error">Fehler.</h1>
        <h2 class="error">Fehler bei der Verarbeitung Ihrer Anforderung.</h2>
    </hgroup>
</asp:Content>
