<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Search Page - CarSharing  
</asp:Content>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="">
        <div class="">
            <hgroup class="title">
                <h1>Search Page.</h1>
              <%--  <h2><%: ViewBag.Message %></h2>--%>
            </hgroup>
        </div>
    </section>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="search_form">
            <p class="name">Country</p>
            <select class="drop_down">
             <option value="">Germany</option>
             <option value="">Ukraine</option>
             <option value="">India</option>
             <option value="">USA</option>
           </select>

            <p class="name">City</p>
            <select class="drop_down">
             <option value="">Chemnitz</option>
             <option value="">Kiev</option>
             <option value="">Deli</option>
             <option value="">New York</option>
           </select>

           <p class="name">Location</p>
            <select class="drop_down">
             <option value="">????</option>
             <option value=""></option>
             <option value=""></option>
             <option value=""></option>
           </select>

            <input class="" value="Start parking position"></input>
            <button class="google_maps">Google Map</button>
            <input class="" value="End parking position"></input>
            <button class="google_maps">Google Map</button>

            <input class="" value="Pick Up date"></input>
            <button class="calender">Calender</button>
            <input class="" value="Drop Off date"></input>
            <button class="calender">Calender</button>
            <input class="" value="Time"></input>
            <button class="calender">Time</button>
            <input class="" value="Time"></input>
            <button class="calender">Time</button>

            <p class="name">Car</p>  
            <select class="drop_down"></select>
            <p class="name">Number of seats</p>  
            <select class="drop_down"></select>
            <p class="name">Class</p>  
            <select class="drop_down"></select>
            <p class="name">Price</p>
            <select class="drop_down"></select>
        
            <button class="search_button">Search</button>
    
</asp:Content>
