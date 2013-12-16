<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Admin
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Admin</h2>
    <table id="adminUserTable">
        <thead>
            <tr class="tableHead">
                <td>ID</td>
                <td>Name</td>
                <td>Firstname</td>
                <td>LogIn</td>
                <td>AccessState</td>
            </tr>
        </thead>
        <tbody>

        </tbody>
    </table>
    <table id="adminCarTable">
        <thead>
            <tr class="tableHead">
                <td>ID</td>
                <td>Name</td>
                <td>Licence</td>
                <td>State</td>
                <td>Type</td>
            </tr>
        </thead>
        <tbody>

        </tbody>
    </table>
    <%--<div id="admin_users">Here is space for the users table</div>
    <div id="admin_cars">Here is space for the cars table</div>--%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
