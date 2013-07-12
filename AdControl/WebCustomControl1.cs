using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AdControl
{

    [ToolboxData("<{0}:BannerRotator runat=server></{0}:BannerRotator>"), DefaultProperty("AdvertisementFile")]
    public class BannerRotator : AdRotator
    {
        // Fields
        private string flashFileExtension = ".swf";
        private string heightKeyPropertyName = "Height";
        private bool? isFlashBanner = null;
        private string navigateUrlBase;
        private AdCreatedEventArgs selectedAdvertArgs;
        private string widthKeyPropertyName = "Width";
        private string wmode = "opaque";
        private const string wmodeDefault = "opaque";

        // Methods
        protected override void OnAdCreated(AdCreatedEventArgs e)
        {
            base.OnAdCreated(e);
            this.selectedAdvertArgs = e;
            this.ResolveTrackingUrl();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (base.DesignMode)
            {
                base.Render(writer);
            }
            else if (this.IsFlashBanner)
            {
                this.RenderFlashBanner(writer);
            }
            else
            {
                base.Render(writer);
            }
        }

        private void RenderFlashBanner(HtmlTextWriter writer)
        {
            if ((this.selectedAdvertArgs != null) && !string.IsNullOrEmpty(this.selectedAdvertArgs.ImageUrl))
            {
                FlashControl flash = new FlashControl
                {
                    FlashUrl = this.selectedAdvertArgs.ImageUrl + "?clickTAG=" +  this.selectedAdvertArgs.NavigateUrl.Replace("~/Advertiser/",string.Empty)
                };
                if (!string.IsNullOrEmpty(this.wmode))
                {
                    flash.WMode = this.wmode;
                }
                if (!string.IsNullOrEmpty(this.ID))
                {
                    flash.ID = this.ClientID;
                }
                if (!this.Enabled)
                {
                    flash.Enabled = false;
                }
                if (!this.SetDimensions(flash))
                {
                    flash.Width = this.Width;
                    flash.Height = this.Height;
                }
                flash.RenderControl(writer);
            }
        }

        private void ResolveTrackingUrl()
        {
            if (((!string.IsNullOrEmpty(this.navigateUrlBase) && (this.selectedAdvertArgs != null)) && !string.IsNullOrEmpty(this.selectedAdvertArgs.NavigateUrl)) && !this.IsFlashBanner)
            {
                this.selectedAdvertArgs.NavigateUrl = string.Format(this.navigateUrlBase, HttpContext.Current.Server.UrlEncode(this.selectedAdvertArgs.NavigateUrl));
            }
        }

        private bool SetDimensions(FlashControl flash)
        {
            int width;
            int height;
            if (this.selectedAdvertArgs.AdProperties == null)
            {
                return false;
            }

            string widthProperty = (string)this.selectedAdvertArgs.AdProperties[this.widthKeyPropertyName].ToString();
            string heightProperty = (string)this.selectedAdvertArgs.AdProperties[this.heightKeyPropertyName].ToString();
            if (!int.TryParse(widthProperty, out width))
            {
                return false;
            }
            if (!int.TryParse(heightProperty, out height))
            {
                return false;
            }
            flash.Width = Math.Abs(width);
            flash.Height = Math.Abs(height);
            return true;
        }

        // Properties
        protected bool IsFlashBanner
        {
            get
            {
                if (!this.isFlashBanner.HasValue)
                {
                    this.isFlashBanner = false;
                    if (this.selectedAdvertArgs == null)
                    {
                        return this.isFlashBanner.Value;
                    }
                    if (this.selectedAdvertArgs.ImageUrl == null)
                    {
                        return this.isFlashBanner.Value;
                    }
                    if (this.selectedAdvertArgs.ImageUrl.EndsWith(this.flashFileExtension, true, CultureInfo.InvariantCulture))
                    {
                        this.isFlashBanner = true;
                        return true;
                    }
                }
                return this.isFlashBanner.Value;
            }
        }

        [DefaultValue(""), Browsable(true), Category("Behavior")]
        public string NavigateUrlBase
        {
            get
            {
                return this.navigateUrlBase;
            }
            set
            {
                this.navigateUrlBase = value;
            }
        }

        [DefaultValue("opaque"), Category("Behavior"), Browsable(true)]
        public string WMode
        {
            get
            {
                return this.wmode;
            }
            set
            {
                this.wmode = value;
            }
        }
    }
















    [ToolboxData("<{0}:FlashControl runat=server></{0}:FlashControl>"), DefaultProperty("ID")]
    public class FlashControl : WebControl
    {
        // Fields
        private string classid = "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
        private const string classidDefault = "clsid:D27CDB6E-AE6D-11cf-96B8-444553540000";
        private string codebase = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0";
        private const string codebaseDefault = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0";
        private string flashUrl;
        private string mimeType = "application/x-shockwave-flash";
        private const string mimeTypeDefault = "application/x-shockwave-flash";
        private string pluginspage = "http://www.macromedia.com/go/getflashplayer";
        private const string pluginspageDefault = "http://www.macromedia.com/go/getflashplayer";
        private string quality = "high";
        private const string qualityDefault = "high";
        private bool renderXhtmlValid;
        private const bool renderXhtmlValidDefault = false;
        private string wmode = "window";
        private const string wmodeDefault = "window";

        // Methods
        private void addParamTag(HtmlTextWriter output, string name, string value, bool fEncodeValue)
        {
            output.Write("<param");
            output.WriteAttribute("name", name);
            output.WriteAttribute("value", value, fEncodeValue);
            output.Write(">");
        }

        protected override ControlCollection CreateControlCollection()
        {
            return new EmptyControlCollection(this);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (base.DesignMode)
            {
                output.RenderBeginTag(HtmlTextWriterTag.Span);
                output.Write(this.ClientID);
                output.RenderEndTag();
            }
            else if (this.renderXhtmlValid)
            {
                this.RenderXHtmlValidHtml(output);
            }
            else
            {
                this.RenderLegacyHtml(output);
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        protected virtual void RenderLegacyHtml(HtmlTextWriter output)
        {
            if (!string.IsNullOrEmpty(this.flashUrl))
            {
                int width = (int)this.Width.Value;
                int height = (int)this.Height.Value;
                bool nonZeroSize = (width > 0) && (height > 0);
                string flashUrlResolved = base.ResolveUrl(this.flashUrl);
                output.AddAttribute("classid", this.classid, true);
                output.AddAttribute("codebase", this.codebase, true);
                output.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
                if (nonZeroSize)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Width, width.ToString());
                    output.AddAttribute(HtmlTextWriterAttribute.Height, height.ToString());
                }
                output.RenderBeginTag(HtmlTextWriterTag.Object);
                this.addParamTag(output, "movie", flashUrlResolved, true);
                this.addParamTag(output, "wmode", this.wmode, false);
                output.Write("<embed");
                if (nonZeroSize)
                {
                    output.WriteAttribute("width", width.ToString());
                    output.WriteAttribute("height", height.ToString());
                }
                output.WriteAttribute("name", "movie");
                output.WriteAttribute("wmode", this.wmode);
                output.WriteAttribute("quality", this.Quality);
                output.WriteAttribute("type", this.MimeType, true);
                output.WriteAttribute("pluginspage", this.Pluginspage, true);
                output.WriteAttribute("src", flashUrlResolved, true);
                output.Write(">");
                output.RenderEndTag();
            }
        }

        protected virtual void RenderXHtmlValidHtml(HtmlTextWriter output)
        {
            if (string.IsNullOrEmpty(this.flashUrl))
            {
            }
        }

        // Properties
        [DefaultValue("clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"), Browsable(true), Category("Behavior")]
        public string Classid
        {
            get
            {
                return this.classid;
            }
            set
            {
                this.classid = value;
            }
        }

        [DefaultValue("http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0"), Category("Behavior"), Browsable(true)]
        public string Codebase
        {
            get
            {
                return this.codebase;
            }
            set
            {
                this.codebase = value;
            }
        }

        [DefaultValue(""), Category("Appearance"), Browsable(true)]
        public string FlashUrl
        {
            get
            {
                return this.flashUrl;
            }
            set
            {
                this.flashUrl = value;
            }
        }

        [Category("Behavior"), Browsable(true), DefaultValue("application/x-shockwave-flash")]
        public string MimeType
        {
            get
            {
                return this.mimeType;
            }
            set
            {
                this.mimeType = value;
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue("http://www.macromedia.com/go/getflashplayer")]
        public string Pluginspage
        {
            get
            {
                return this.pluginspage;
            }
            set
            {
                this.pluginspage = value;
            }
        }

        [DefaultValue("high"), Category("Appearance"), Browsable(true)]
        public string Quality
        {
            get
            {
                return this.quality;
            }
            set
            {
                this.quality = value;
            }
        }

        [Category("Appearance"), DefaultValue(false), Browsable(true)]
        public bool RenderXhtmlValid
        {
            get
            {
                return this.renderXhtmlValid;
            }
            set
            {
                this.renderXhtmlValid = value;
            }
        }

        [Browsable(true), Category("Behavior"), DefaultValue("window")]
        public string WMode
        {
            get
            {
                return this.wmode;
            }
            set
            {
                this.wmode = value;
            }
        }
    }





}


