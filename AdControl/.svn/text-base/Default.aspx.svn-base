<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AdControl" Namespace="AdControl" TagPrefix="BannerRotator" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BannerRotator Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div> AdControl
        <p>This is a flash banner only.</p>
        <BannerRotator:BannerRotator ID="BannerRotator2" KeywordFilter="flash" AdvertisementFile="~/App_Data/banners.xml" runat="server" />
    </div>
    <hr />
    <div>
        <p>This is a rotating banner, refresh the page few times to change.</p>
        <BannerRotator:BannerRotator ID="BannerRotator1" AdvertisementFile="~/App_Data/banners.xml" runat="server" />
    </div>
    <hr />
    <div>
        <p>This is an image banner, note that click on the link below is redirected through ban-click.aspx web page</p>
        <BannerRotator:BannerRotator ID="BannerRotator3" KeywordFilter="image" NavigateUrlBase="~/ban-click.aspx?url={0}" AdvertisementFile="~/App_Data/banners.xml" runat="server" />
        
<BannerRotator:FlashControl ID="FlashControl1" runat="server" />
    </div>
   </form>
</body>
</html>

