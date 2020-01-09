using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace TB2.UIStore
{
    public static class TalentBrewUI
    {

        //TB 2.0 Home Page
        //public static By jobsByCategoryLink = By.XPath("//div[contains(@class,'job-category')]");
        public static By jobsByCategoryLink = By.XPath("//*[contains(@class,'drop-list__list') or contains(@class,'job-category dropdown') or contains(@class,'job-category') or contains(@class,'head-office-categories') or contains(@class,'job-category hide') or contains(@id,'jobsByCategory') or contains(@class,'category-search') or contains(@class,'job-container') or contains(@id,'category-section') or contains(@data-selector-name,'jobcategory') or contains(@id,'jobsByCategory')] | //*[@id='category-list-selector']/option");
        public static By jobsByCategoryLink1 = By.XPath("//main[@id='content']/section[2]/div/ul");
        public static By jobsByCategoryLink2 = By.XPath("//div[@id='page']/div[2]/div/ul");
        public static By jobsByCategoryLink3 = By.XPath("//div[contains(@class,'job-category')]/div");
        public static By jobsByCategoryLink4 = By.XPath("//*[contains(@data-selector-name,'jobcategory')//[contains(@class,'toggled')]]");
        public static By jobsByCategoryLink5 = By.XPath("//*[@id='page']/header/nav/div/div/div[3]/ul/li/a");
        public static By jobsByCategoryLinkNew = By.XPath("//div[contains(@class,'job-keyword')]//ul");
        //public static By searchJobsByCategory = By.XPath("//div[contains(@class,'job-category')]/child::*[contains(translate(text(), 'CLG', 'clg'),'categ')]");
        //public static By searchJobsByCategory1 = By.XPath("//div[contains(@class,'job-category')]/child::*[contains(text(),'categ')]");

        //public static By jobsByCategoryLink = By.XPath("//div[contains(@class,'job-category')]/descendant::ul[position()=1]");
        //public static By jobsByCategoryInnerLink = By.XPath("//div[contains(@class,'job-category')]/ul");
        //public static By searchJobsByCategory = By.XPath("//div/h2[contains(text(),'Category')]");
        public static By searchJobsByCategoryDiv = By.XPath("//div[contains(@class,'job-category')]");
        public static By searchJobsByCategory = By.XPath("//*[contains(@class,'job-category')]/child::*[contains(translate(text(), 'CLG', 'clg'),'categ')]");
        public static By searchJobsByLinkCategory19 = By.XPath("//a[@title='JOB CATEGORIES']");

        public static By searchJobsByCategoryLinkAchor = By.XPath("//a[contains(text(),'Jobs By Category')or contains(@href,'/sitemap#category') or contains(text(),'Jobs by Category') or contains(text(),'Browse by Jobs') or contains(text(),'Category') or contains(text(),'category')]");
        public static By searchJobsByCategoryLinkAchor1 = By.XPath("//a[contains(@class,'full-container')]");
        public static By searchJobsByCategoryHref = By.XPath("//a[contains(@href,'category')]");

        public static By searchJobsByLinkCategory = By.XPath("//div[(contains(@class,'dropdown2')) and (contains(text(),'Jobs by Job Type'))]");
        public static By searchJobsByLinkCategory1 = By.XPath("//*[contains(@id,'cat-list') or contains(text(),'Or Search By Category') or contains(text(),'Search by Category') or contains(@class,'job-category') or contains(text(),'Browse Job Groups') or contains(@href,'/jobs-by-category') or contains(@class,'category-link') or contains(text(),'Browse Job Categories') or contains(@class,'dropdown-category') or contains(@href, '/job-categories') or contains(@class,'job-category expandable')]");
        public static By searchJobsByLinkCategory2 = By.XPath("//a/h2[contains(text(),'Or Search By Category') or contains(text(),'Ou Procure por Categoria') or contains(text(),'Búsqueda por Categoría')or contains(text(),'Jobs By Category')]");
        public static By searchJobsByLinkCategory3 = By.XPath("//div/h2[contains(text(),'Search Jobs by Category') or contains(text(),'Search by Category') or contains(text(),'Search By Category') or contains(text(),'Search Jobs By Category') or contains(text(),'Jobs by Category') or contains(text(),'Browse our job areas') or contains(text(),'See More Jobs by Category') or contains(text(),'Browse by Category') or contains(text(),'emploi par fonction') or contains(text(),'emploi Par Fonction') or contains(text(),'Stellen nach Kategorie') or contains(@id,'browse-job-category') or contains(text(),'Ofertas de empleo por función') or contains(text(),'par fonction')]");
        public static By searchJobsByLinkCategory4 = By.XPath("//div/h2/b[contains(text(),'Search Jobs by Category')]");
        public static By searchJobsByLinkCategory5 = By.XPath("//div[2]/h2[contains(text(),'Browse by Category')]");
        public static By searchJobsByLinkCategory6 = By.XPath("//a[contains(text(),'by category') or contains(text(),'Empleos por categoría') or contains(@href, 'jobcategories')or contains(text(),'Browse by category') or contains(text(),'Job Categories') or contains(text(),'Catégories de postes') or contains(text(),'Functiecategorieën') or contains(text(),'Stellenkategorien') or contains(text(),'Categorías de empleos') or contains(text(),'In-Store') or contains(text(),'Categorias de emprego') or contains(text(),'Browse by category')] | //*[@id='category-list-selector']"); 
        public static By searchJobsByLinkCategory7 = By.LinkText("Jobs by Category");
        public static By searchJobsByLinkCategory8 = By.XPath("//div[@id='page']/footer/div/section/article[3]/div/h2");
        public static By searchJobsByLinkCategory9 = By.XPath("//div/h2[contains(text(),'CATEGORY') or contains(text(),'Job by Function')  or contains(text(),'Rechercher par catégorie') or contains(text(), 'Browse by function')]");
        public static By searchJobsByLinkCategory10 = By.CssSelector("h2.expandable-parent.metier-button");
        public static By searchJobsByLinkCategory11 = By.XPath("//a/span[contains(text(),'Search Jobs by Category')]");
        public static By searchJobsByLinkCategory12 = By.XPath("//main[@id='content']/section[4]/div/h2");
        public static By searchJobsByLinkCategory13 = By.XPath("//main[@id='content']/div/div/h2/span");
        public static By searchJobsByLinkCategory14 = By.XPath("//div[@id='page']/div/section/div/h2");
        public static By searchJobsByLinkCategory15 = By.XPath("//div//h2[contains(@id,'ExpandCategory')]");
        public static By searchJobsByLinkCategory16 = By.XPath("//div/div[1]/div[1]/h2/span[contains(text(),'Category')]");
        public static By searchJobsByLinkCategory17 = By.XPath("//div[contains(@class,'job-category')]/h2[contains(text(),'Offres d')]");
        public static By searchJobsByLinkCategory18 = By.XPath("//div[contains(@class,'job-category')]/h2");
        //public static By searchJobsByLinkCategory17 = By.XPath("//div[contains(@class,'job-category') or contains(@class,'job-category dropdown')]/h2[contains(text(),'Offres d')]");
        //careers.bbcworldwide.com
        //public static By searchJobsByLinkCat1 = By.XPath("//div[1]/h2//[contains(@data-selector-name,'jobcategory')]");
        public static By searchJobsByLinkCat1 = By.XPath("//div[@data-selector-name ='jobcategory']/h2");

        //public static By jobsByLocationLink = By.XPath("//div[contains(@class,'job-location')]");

        public static By jobsByLocationLink = By.XPath("//*[contains(@class,'job-container') or contains(@class,'job-location collapseme active') or contains(@class,'job-location') or contains(@class,'job-location hide') or contains(@class,'expandable-parent job-group-heading active') or contains(@data-selector-name,'joblocation') or contains(@class,'expandable-childlist-open') or contains(@id,'jobsByLocation') or contains(text(),'勤務地別の求人情報リスト')]");
        public static By jobsByLocationLink1 = By.CssSelector("div.job-container.location-list");
        public static By jobsByLocationLink_NEW = By.XPath("//*[contains(text(),'勤務地別の求人情報リスト')]/..//ul");
        public static By JobsByLocationLink_NEW2 = By.XPath("//div[@data-selector-name = 'joblocation']//h2/..//ul");
        public static By JobsByLocationLink8 = By.XPath("//*[@id='content']/div[2]/div/div/div[2]/div/ul");
        public static By jobsByLocationLink2 = By.CssSelector("div.jobdropdown-location > div.job-container.category-list");
        public static By jobsByLocationLink3 = By.CssSelector("div.job-location > div.job-container > ul");
        public static By jobsByLocationLink4 = By.ClassName("open");
        public static By jobsByLocationLink5 = By.CssSelector("div.job-lieu.droplist-cat > ul");
        public static By jobsByLocationLink6 = By.XPath("//main[@id='content']/div[2]/div/div/div[2]/div");
        public static By jobsByLocationLink7 = By.XPath("//div[contains(@class,'job-location')]/ul | //*[@id='content']/div/div/section/div/div[2]/div");
        public static By jobsByLocationLink10 = By.XPath("//*[@id='content']/div/div/section/div/div[1]/div");

        //public static By jobsByLocationLink2 = By.XPath("//div[contains(@class,'dropdown-location')");
        //public static By jobsByLocationLink = By.XPath("//div[(contains(@class,'job-location'))]/descendant::ul[(position()=1]");
        //public static By jobsByLocationLink = By.XPath("//div[(contains(@class,'job-location'))]/descendant::ul[(position()=1)]/descendant::ul[not(contains(@style,'display: none'))]");
        //public static By searchJobsByLocation = By.XPath("//h2[contains(text(),'Location')]");
        //public static By searchJobsByLocation = By.XPath("//div[contains(@class,'job-location')]/child::*[contains(text(),'oca')]");
        public static By searchJobsByLocation = By.XPath("//*[contains(@class,'job-location')]/child::*[contains(translate(text(), 'CLG', 'clg') ,'loca')] | //*[contains(@class,'job-location')] | //*[contains(text(),'Recherche par lieu de travail')]");
        //public static By searchJobsByGroup = By.XPath("//div[contains(@class,'job-location')]/child::*[contains(translate(text(), 'CLG', 'clg') ,'loca')]");
        //div/h2[contains(text(),'CATEGORY') or contains(text(),'Job by Function')  or contains(text(),'Rechercher par catégorie')]"
        public static By searchJobsByLocationLinkAchor = By.XPath("//a[contains(text(),'Jobs by Location') or  contains(@data-target,'Jobs by Location') or contains(text(),'Search by Location') or contains(@href,'location-section') or contains(text(),'Jobs By Location') or contains(text(),'Browse By Location')  or contains(text(),'Browse by location') or contains(text(),'Browse by Location') or contains(text(),'Browse Job Locations') or contains(@href,'/sitemap#location') or contains(@href,'jobs-by-location')]");
        public static By searchJobslistByLocation = By.XPath("//*[contains(text(),'勤務地別の求人情報リスト') or contains(text(), 'Offres d'emploi par lieu')]");
        public static By searchJobslistByLocation2 = By.XPath("//div[@data-selector-name = 'joblocation']//h2");
        // or contains(text(),'Location') or contains(text(),'location')
        //||com_IsElementPresent(By.XPath("//*[contains(@href,'location-section')]"))
        public static By btn_Explore = By.CssSelector("button.scroller.white");
        public static By btn_BrowseJobs1 = By.XPath("//nav[@id='mainMenu']/ul/li/span");
        public static By btn_BrowseJobs = By.XPath("//*[text()='Explore the available locations'] | //a[contains(text(),'Browse jobs')] | //button[contains(text(),'Browse jobs') or contains(text(),'Parcourir postes') or contains(text(),'Door vacatures bladeren') or contains(text(),'Jobs durchsuchen') or contains(text(),'Buscar empleos') or contains(text(),'Procurar empregos') or contains(text(),'Esplora le posizioni disponibili')] | //div[3]/div/div/a");
        public static By searchJobsByLinkLocation = By.XPath("//div[(contains(@class,'dropdown3')) and (contains(text(),'Jobs by Location'))] | //a[contains(text(),'Browse by country') or contains(text(),'Location') or contains(text(),'Lieu') or contains(text(),'Locatie') or contains(text(),'Standort') or contains(text(),'Ubicación') or contains(text(),'Sede')  or contains(text(),'Localização')]");
        public static By searchJobsByLinkLocation1 = By.XPath("//*[contains(@id,'loc-list') or contains(text(),'Search by Location') or contains(@class,'location-link') or contains(@class,'dropdown-location') or contains(@href,'#jobsByLocation')]");
        public static By searchJobsByLinkLocation2 = By.XPath("//a/h2[contains(text(),'Jobs By Location')]");
        public static By searchJobsByLinkLocation3 = By.XPath("//div/h2[contains(text(),'Search Jobs By Location') or contains(text(),'Search by Location') or contains(text(),'Jobs by Location') or contains(text(),'Browse by Location')]");
        public static By searchJobsByLinkLocation4 = By.XPath("//div/h2/b[contains(text(),'Search Jobs by Location')]");
        public static By searchJobsByLinkLocation5 = By.XPath("//div/h2/span[contains(text(),'Locations')]");
        public static By searchJobsByLinkLocation6 = By.XPath("//div/h2[contains(text(),'LOCATION') or contains(text(),'Recherche par lieu') or contains(text(), 'Location') or contains(text(), 'Browse by location')]");//changed by ramya
        public static By searchJobsByLinkLocation7 = By.CssSelector("h2.expandable-parent.region-button");
        public static By searchJobsByLinkLocation8 = By.XPath("//a[2]/span[contains(text(),'Search Jobs by Location')]");
        public static By searchJobsByLinkLocation9 = By.XPath("//div/div[2]/h2/span[contains(text(),'Location')]");
        public static By searchJobsByLinkLocation10 = By.XPath("//div[@id='page']/div/section/div[2]/h2[contains(text(),'Recherche par emplacement')]");
        public static By searchJobsByLinkLocation11 = By.XPath("//div[@id='page']/div/div/div/div/div[3]/h2");
        public static By searchJobsByLinkLocation12 = By.XPath("//div[contains(@class,'job-location')]/h2");
        public static By searchJobsByLinkLocation13 = By.XPath("//div/h2[contains(text(),'Rechercher par ville')]");
        public static By searchJobsByLinkLocation14 = By.XPath("//*[@id='content']//div[2]/div[2]/h2");
        public static By SearchJobsLocationNEW = By.XPath("//h2[text()='Search Jobs By Location']");
        public static By SearchJobsByLocationLink = By.XPath("//div[@class='browse-by job-location']");
        public static By Selectlocation = By.XPath("//div[@class='browse-by job-location']//li//a");

        //public static By jobsByGroupLink = By.XPath("//div[contains(@class,'job-keyword')]");
        //public static By jobsByGroupLink = By.XPath("//*[contains(@class,'job-keyword')]");
        public static By jobsByGroupLink = By.XPath("//*[contains(@class,'job-keyword') or contains(@data-selector-name,'jobkeyword') or contains(@class,'job-groups') or contains(@class,'job-hierarchy') or contains(@id,'ui-id-6') or contains(@class,'col-1')]");
        public static By jobsByGroupLink1 = By.XPath("//div[contains(@class,'job-keyword')]//ul");

        //public static By jobsByGroupLink = By.XPath("//div[contains(@class,'job-keyword')]/descendant::ul[position()=1]");
        //public static By searchJobsByGroup = By.XPath("//h2[contains(text(),'Group')]");
        //public static By searchJobsByGroup = By.XPath("//div[contains(@class,'job-keyword')]/child::*[contains(text(),'rou')]");
        public static By searchJobsByGroup1 = By.XPath("//*[contains(text(),'Browse Job Groups')]");
        public static By searchJobsByGroup = By.XPath("(//*[contains(@class,'job-keyword')]/child::*[contains(translate(text(), 'CLG', 'clg') ,'grou')])");
        //public static By searchJobsByGroup = By.XPath("//div[contains(@class,'job-keyword')]/child::*[contains(translate(text(), 'CLG', 'clg') ,'grou')]");

        public static By searchJobsByGroupLinkAchor = By.XPath("//*[contains(text(),'Jobs By Group') or contains(@href,'/sitemap#group') or contains(text(),'Jobs by Group') or contains(text(),'Browse Job Groups') or contains(@class,'job-category-location') or contains(text(),'Group') or contains(@href, 'jobs-by-group') or contains(@href,'/browse-by-job-group') or contains(@class,'job-keyword') or contains(text(), 'グループ別の求人情報リスト')]");
        public static By secJobByGroup = By.XPath("//div/h2[contains(text(),'Browse Job Groups') or contains(text(),'Browse Job groups') or contains(text(),'emploi par fonction et par lieu') or contains(text(),'emploi Par Fonction Et Par Lieu') or contains(text(),'Gruppen')]");
        public static By searchJobsByLinkGroup = By.XPath("//div[(contains(@class,'dropdown4')) and (contains(text(),'Jobs by Group'))]");
        public static By searchJobsByLinkGroup1 = By.XPath("//a/h2[contains(text(),'Jobs by Group')]");
        public static By searchJobsByLinkGroup2 = By.XPath("//a[contains(@href,'/browse-by-groups') or contains(text(),'Jobs by Category + Location')]");
        public static By searchJobsLinkGroup3 = By.XPath("//div[contains(@class,'job-keyword')]/h2");
        //public static By searchJobsByLinkGroup3 = By.XPath("//div/h2[contains(text(),'Offres d'emploi par fonction et par lieu')]");

        public static By categoryJobs = By.XPath("//a[contains(@href, '/category/')]");

        public static By SearchByGroupsLinkNew = By.XPath("//*[@class='jobdropdown-keyword']");

        public static By DropDownPrimark = By.XPath("//*[contains(@class,'icon-triangle toggle')]");

        //public static By jobsByCategory_Link1 = By.XPath("//div/ul/li[2]/a");

        public static By l2_jobTitle = By.XPath("//section[@id='search-results']/h1");
        public static By l2_jobtitle1 = By.XPath("//section[@id='search-results']/div/h1");
        public static By l2_jobTitle2 = By.XPath("//section[@id='search-results']/h3/span");
        public static By l2_jobTitle3 = By.Id("search-results");

        public static By nextPagination = By.LinkText("Next");
        public static By previousPagination = By.LinkText("Prev");

        public static By searchResultLink = By.XPath("//section[@id='search-results-list']//ul/li/a | //*[@id='search-results-list']/ul/li[1]/a");
        public static By searchResultLink1 = By.XPath("//section[@id='search-results-list']/ul/li/a/h2");
        public static By searchResultLink2 = By.XPath("//section[@id='search-results-list']/ol/li");
        public static By searchResultLink3 = By.XPath("//section[@id='search-results-list']/table/tbody/tr/*/a");
        public static By searchResultLink4 = By.XPath("(//section[@id='search-results-list']/table/tbody/tr/*/a)[1]");
        //maniAdded
        //deluxe-Advanced search first job link--New change --08/10
        public static By searchResultJobLink = By.XPath("(//section[@id='search-results-list']//ul)[2]//li//a");
        
        public static By Noresults_Link = By.XPath("//*[@id = 'no-results']");
        public static By l3_jobTitle = By.XPath("//h1[@itemprop='title']");
        public static By SearchResultLink4 = By.XPath("//section[@id='search-results-list']//ul/li[2]/a");

        public static By header = By.XPath("//header/h1");

        //for disneycareers location toggle//section[@id='search-results-list']/table/tbody/tr/th/a
        public static By LocationToggle = By.XPath("//p[@class='search-toggle']");




        public static By paginationNumber = By.Id("pagination-current-bottom");

        //Social Media
        //public static By socialMediaModule = By.ClassName("social-media");
        //public static By socialMediaModule = By.XPath("//*[contains(@class,'social-media') or contains(@class,'social-links')]");
        //rajesh added
        public static By btn_ExploreSocialMedia = By.XPath("//button[contains(@class,'menu-button')]");
        public static By socialMediaModule = By.XPath("//*[contains(@id,'social-menu')" +
                                               "or contains(@class,'social-media')" +
                                               "or contains(@id,'social-nav')" +
                                               "or contains(@class,'social')" +
                                               "or contains(@id,'social-connect')" +
                                               "or contains(@id,'footer-social')" +
                                               "or contains(@class,'social-share')" +
                                               "or contains(@class,'socialBtnA')" +
                                               "or contains(@class,'footer-icons')" +
                                               "or contains(@class,'social-links')" +
                                               "or contains(@class,'share_container noLinking')" +
                                               "or contains(@class,'footer-info')" +
                                               "or contains(@id,'top-social')" +
                                               "or contains(@id,'followus')" +
                                               "or contains(@data-selector-name,'socialmedia')" +
                                               "or contains(@class,'wrapper clearfix')" +
                                               "or contains(@id,'socail-links')" +
                                               "or contains(@id,'follow-us')" +
                                               "or contains(@class,'footer-contact')" +
                                               "or contains(@id,'social')]");
        //public static By socialMediaModule1 = By.XPath("//footer/div/div[2]/ul");
        public static By socialMediaModule1 = By.XPath("//footer//ul");
        public static By socialMediaModule2 = By.XPath("//*[contains(@class,'wrapper clearfix') or contains(@class,'social-share')]");
        public static By socialMediaModule3 = By.CssSelector("ul.social-media, div.social-media");
        public static By socialMediaModule4 = By.XPath("(//form[@id='social-share']/section)[2]");
        public static By socialMediaModule5 = By.XPath("//footer[@id='footer']/div/nav[2]");
        public static By socialMediaModule6 = By.XPath("//section[6]//ul//ul");

        //public static By facebook = By.LinkText("Facebook");
        // or contains(@id,'facebook')" + "or contains(@class,'facebook')" + "or contains(@class,'btn-facebook')" + "or contains(@href,'facebook')
        public static By facebook = By.XPath("//*[contains(@id,'facebook') or contains(@class,'fa-facebook') or contains(@class,'facebook') or contains(@class,'btn-facebook') or contains(@href,'facebook')]");
        public static By facebook1 = By.XPath("(//a[contains(text(),'Facebook')])[2]");
        public static By facebook2 = By.XPath("//i[contains(@class,'fa-facebook')]");

        //public static By linkedIn = By.LinkText("LinkedIn");
        //public static By linkedIn1 = By.LinkText("Visit us on LinkedIn");        
        public static By linkedIn = By.XPath("//*[contains(@class,'linkedin') or contains(@id,'linkedin') or contains(@href,'linkedin') or contains(text(),'follow us on LinkedIn')]");
        public static By linkedIn1 = By.XPath("(//a[contains(text(),'LinkedIn')])[2]");
        public static By linkedIn2 = By.XPath("//ul[@id='social-nav']/li[2]/a/span/i[2]");

        public static By twitter = By.XPath("//*[contains(@id,'twitter') or contains(@class,'twitter') or contains(@href,'twitter')]");
        public static By twitter1 = By.XPath("(//a[contains(text(),'Twitter')])[2]");
        public static By twitter2 = By.LinkText("Twitter");

        public static By youTube = By.XPath("//*[contains(@class,'youtube') or contains(@id,'youtube') or contains(@href,'youtube')]");
        //public static By youTube = By.LinkText("YouTube");

        public static By pInterest = By.XPath("//*[contains(@id,'pinterest') or contains(@class,'pinterest') or contains(@href,'pinterest')]");
        //public static By pInterest = By.LinkText("Pinterest");

        public static By Instagram = By.XPath("//a[contains(text(),'Instagram') or contains(@class,'instagram')]");
        public static By eMail = By.LinkText("Email");

        //Meet Us
        public static By meetUs = By.LinkText("Meet Us");

        //Recent Jobs
        public static By recentJobModule = By.ClassName("job-list");
        public static By recentJobModule1 = By.CssSelector("section.dropdowns.openings-dropdown > h2.expandable-parent");
        //public static By recentJobLink = By.XPath("//section[contains(@class,'job-list')]");
        public static By recentJobLink = By.XPath("//section[contains(@class,'job-list')]/ul");
        //public static By recentJobLink1 = By.XPath("//section/div/ul");
        //IWebElement x = driver.FindElementByXPath("//section[contains(@class,'job-list')]");
        //IList<IWebElement> xx = x.FindElements(By.TagName("a"));

        //SiteMap

        //        public static By siteMap = By.LinkText("Sitemap");
        // public static By siteMap = By.XPath("//a[contains(text(),'Site')]");
        // public static By siteMap = By.XPath("//*[contains(text(),'Site')]");

        //ToDo:
        //*[contains(@href,'site')]
        //public static By siteMap = By.XPath("//a[contains(@href,'/site')]");
        //public static By siteMap2 = By.XPath("//a[contains(@href,'/Site')]");

        public static By siteMap = By.XPath("//a[contains(text(),'map') or contains(text(),'Map') or contains(text(),'Sitemap')]");
        public static By siteMap2 = By.XPath("//a[contains(@href,'/site') or contains(@href,'/plan-du-site')]");
        public static By siteMap1 = By.XPath("(//a[contains(@href, '/sitemap')])[2]");
        public static By SiteMap3 = By.XPath("//a[contains(@href,'/Sitemap') or contains(text(),'Site Map') or contains(@href,'/sitemap')]");
        public static By SiteMap4 = By.XPath("//footer[@id='footer']/div/section/ul/li[9]/a");
        public static By SiteMap5 = By.LinkText("Sitemap");

        // public static By siteMap = By.XPath("//a[contains(text(),'site')]");
        //public static By siteMap2 = By.XPath("//*[contains(text(),'Site')]");


        public static By joinOurTalent_Popup = By.CssSelector("div.fancybox-skin");
        public static By closeButton_TalentPop = By.CssSelector("a.fancybox-item.fancybox-close");

        public static By careerHomePage = By.LinkText("Career Home Page");
        //public static By HomePage = By.Id("home");
        //public static By HomePage = By.XPath("//body[@id='home']/div");
        public static By HomePage = By.XPath("//*[contains(@id,'home') or contains(@id, 'main-wrapper') or contains(@id, 'mainContainer') or contains(@id,'content')]");
        public static By Homepage1 = By.XPath("//div[@id='primary-header-container']");
        public static By Homepage2 = By.XPath("//main[contains(@id,'content')]");
        public static By Homepage3 = By.XPath("//div[@id='body-bg']");
        public static By Homepage4 = By.XPath("//div[@class='cs_content cs_cfix' or @class='video-container']");
        public static By Homepage5 = By.XPath("//div[@id='page-main-content']");
        public static By Homepage6 = By.XPath("//*[@id='ctl00_Html1']");


        //public static By searchResultPage = By.Id("search");
        public static By lilly = By.XPath("//div[@id='primary']/div[3]/ul/li/a");
        public static By searchResultPage = By.XPath("//body[contains(@id,'search')] | //body[contains(@id,'search-results-list')]");
        public static By searchResultPage1 = By.XPath("//section[contains(@id,'search')] | //*[@id='search-results-list']");
        public static By searchResultPage2 = By.XPath("//table[@id='searchresults']");
        
        //*************************************************************************************************************

        //basic search
        public static By search_Apply = By.XPath("//div[@id='page']/header/div/div/div");
        public static By btn_searchJobs = By.XPath("//div[@id='search-button']/a/span | //button[contains(@class,'search-toggle')]");
        public static By btn_searchJobs1 = By.XPath("//h2[@id='ExpandSearch']");
      //  public static By btn_searchJobs2 = By.XPath("//a[contains(@href, 'search-form-fields') or contains(@href, '/search-jobs')]");
       // public static By btn_SearchJobs3 = By.XPath("//ul[@id='menu-toggles']/li[4]/span");
      //  public static By btn_SearchJobs4 = By.XPath("//*[@data-tab-name='search']");

        //yet to verify
        public static By txt_keywordSearch = By.XPath("//input[(@name='k') or (@id='edit-keywords--2') or contains(@id,'advanced-search-keyword') or contains(@id,'search-keyword') or contains(@class,'job-search-line') or contains(@class,'search') or starts-with(@type,'search') or contains(@id,'search')]");
        public static By txt_keywordSearch1 = By.XPath("//form[2]/div/p/input");
        public static By txt_keywordSearch2 = By.XPath("//input[(@type='text') or (@type='search')]");
        public static By txt_keywordsearch3 = By.XPath("//input[@name='k']");
        public static By txt_keywordsearch4 = By.XPath("//div[@class='search-ourjob-home']//input[@name='k']");
        public static By txt_keywordsearch5 = By.XPath("//button[@class='search-anchor']");
        public static By txt_keywordSearch6 = By.XPath("//input[(@type='text')]");


        public static By btn_Search = By.XPath("//button[contains(@id,'search-submit') or contains(@class,'job-search-submit') or contains(@id,'edit-submit--3') or contains(text(), 'Search Jobs')]");
        public static By btn_Search1 = By.XPath("//button[contains(@id,'advanced-search-submit')]");
        public static By btn_search2 = By.XPath("//input[@id='edit-submit--3']");
        public static By btn_search3 = By.CssSelector("button");
        public static By btn_search4 = By.XPath("//div[@class='search-ourjob-home']//button[contains(@id,'search-submit')]");
        public static By btn_search5 = By.XPath("//div[@class='inner']//button[contains(@id,'advanced-search-submit')]");
        public static By btn_search6 = By.XPath("//button[@class='search-button']");
        //Ramya Added
        public static By btn_Search3 = By.XPath("//button[contains(@id,'search-submit')]");
        
        public static By btn_Search5 = By.XPath("(//button[contains(@id,'advanced-search-submit')])[2]");


       // public static By btn_searchjobs = By.XPath("//a[contains(text(), 'Search Jobs')]");
        public static By btn_searchjobs1 = By.XPath("//button[(@class='search-anchor') or (@class='btn-global')]");
        public static By btn_searchjobs2 = By.XPath("//div[@class='external']//h3");
        public static By btn_searchjobs3 = By.XPath("//a[@href='/search-jobs']");
  
        //Advance search
        public static By txt_LocationSearch = By.Name("l");
        public static By lst_Radius = By.Name("r");
        public static By section_Searchresults = By.Id("search-results");
        public static By btn_AdvanceSearch = By.CssSelector("a.advanced-search-toggle");
        public static By btn_AdvanceSearch1 = By.XPath("//a[contains(@href,'advanced-search-form-fields')]");
        public static By lnk_LocationLink = By.XPath("//body/ul/li/a");
        public static By lnk_LocationLink1 = By.XPath("//a[contains(@data-lt,'4')]");
        public static By advanceSearch_Button = By.CssSelector("button.advanced-search-button");

        public static By all_Location_lnk = By.XPath("//ul[contains(@id,'search-location')]");
        public static By drp_Category = By.ClassName("advanced-search-category");
        public static By drp_Country = By.ClassName("advanced-search-country");
        public static By drp_State = By.ClassName("advanced-search-state");
        public static By drp_City = By.ClassName("advanced-search-city");

        //public static By advanceSearchForm = By.XPath("//div[contains(@class,'custom-facets')]");
        public static By advanceSearchForm = By.XPath("//div[contains(@class,'advanced-search-form-fields')]");
        public static By advancesearchform1 = By.XPath("//*[contains(@id,'language-redirector')]");

        //l3
        //public static By applyButton1 = By.XPath("//a[contains(text(),'Apply')]");
        public static By rss_L3page = By.XPath("//main[@id='content' or @id='contentwrapper']");
        public static By applyButton1 = By.XPath("//*[contains(@class,'button job-apply bottom') or contains(@class,'job-apply')] | //*[@id='content']/section[4]//a | //*[@data-selector-name='job-apply-link']");


        public static By applyButtonwithCaps = By.XPath("//a[contains(text(),'APPLY')]");
        //public static By applyButton2 = By.XPath("(//a[contains(text(),'Apply')])[2]");
        //public static By applyButton2 = By.XPath("(//a[contains(@class,'job-apply')])[2]");
        public static By applyButton2 = By.XPath("(//a[(contains(@class,'job-apply')) and not (contains(@class,'later'))])[2]");
        public static By Btn_SaveJob = By.Id("save-job");
        public static By Btn_RemoveJob = By.ClassName("saved");
        public static By sec_SavedJobs = By.CssSelector("section.recently-viewed-job-list");
        public static By lnk_savedJobs = By.CssSelector("section.recently-viewed-job-list > ul > li > a");
        public static By lnk_noJobs = By.CssSelector("section.recently-viewed-job-list > p");
        public static By applyLater = By.LinkText("Apply Later");

        //Ramya Added
        public static By Keyword_txt_new = By.XPath("(//div[@class='fields keyword-field']//input)[2]");
        public static By advancesearchform2 = By.XPath("(//div[contains(@class,'advanced-search-form-fields')])[2]");
        public static By Searchopportunities_btn = By.Id("hero-search");

        //Ramya Added
        public static By btn_searchJobs2 = By.XPath("//a[contains(@href, 'search-form-fields') or contains(@href, '/search-jobs') or contains(@href, '#search-form')]");
        public static By btn_SearchJobsNew = By.XPath("//a[contains(@href, 'search-form-fields')]");
        public static By btn_SearchJobs3 = By.XPath("//ul[@id='menu-toggles']/li[4]/span");
        public static By btn_SearchJobs4 = By.XPath("//*[@data-tab-name='search']");
        public static By btn_searchjobs = By.XPath("//*[contains(text(), 'Search Jobs')]");

        //Filter
        public static By Filter_button = By.XPath("//section[@id='search-results']/div/div/*[contains(text(),'Filter')]");
        public static By Filter_button1 = By.XPath("//button[contains(@class,'open-filter-button')] | //div[contains(@class,'results-area')]//button[contains(@class,'filter-toggle')]");
        public static By Filter_button2 = By.XPath("//a[contains(text(),'Filter results')]");
        public static By Filter_button3 = By.XPath("//section[@id='search-results']/div/span");
        public static By Filter_button4 = By.CssSelector("div.toggle-filter-button");
        public static By Filter_button5 = By.XPath("//span[contains(@class,'open-filter')]");
        public static By Filter_button6 = By.XPath("//div[@class='search-filter-wrapper']//h2");
        public static By Filter_button7 = By.XPath("//button[contains(@id,'filters')]");
        public static By Filter_button8 = By.XPath("//button[contains(@class,'filter-open-button')]");
        //boobalan added new
        public static By Filter_button9 = By.XPath("//*[contains(@class,'filter-toggle')]"); 
        public static By Filter_toggle = By.Id("filter-toggle");
        public static By Accept_cookie = By.Id("accept-cookie");
        public static By section_Filter = By.Id("search-filters");
        public static By Section_filter1 = By.Id("filter-box");
        public static By chk_categoryToggle = By.Id("category-toggle");
        public static By chk_categoryToggle1 = By.Id("job_status-toggle");
        public static By categoryToggleNew = By.Id("job_level-toggle");
        public static By categoryToggle2 = By.XPath("//a[@aria-controls='category-toggle']");
        public static By chk_countryToggle = By.Id("country-toggle");
        public static By chk_regionToggle = By.Id("region-toggle");
        public static By chk_regionToggle1 = By.XPath("//a[@aria-controls='region-toggle']");
        public static By chk_cityToggle = By.Id("city-toggle");
        public static By chk_cityToggle1 = By.XPath("//a[@aria-controls='city-toggle']");
        
        public static By chk_institutionToggle = By.Id("industry-toggle");
        public static By chk_campusToggle = By.Id("job_status-toggle");

        public static By chk_category1 = By.Id("category-filter-0");
        public static By chk_category2 = By.Id("category-filter-1");
        public static By chk_category3 = By.Id("job_status-filter-0");
        public static By chk_category4 = By.XPath("//section[@id='search-filters']/div/section[3]/ul/li/label/b");
        public static By chk_category_novan = By.XPath("//*[@for='category-filter-0']");

        public static By newToggleCat1 = By.Id("job_level-filter-0");
        public static By newToggleCat2 = By.Id("job_level-filter-1");
        public static By chk_country1 = By.Id("country-filter-0");
        public static By chk_Country2 = By.XPath("//section[@id='search-filters']/div/section/ul/li/label/b");
        public static By chk_region1 = By.Id("region-filter-0");
        public static By chk_city1 = By.Id("city-filter-1");
        public static By chk_city2 = By.XPath("//label[contains(@for,'city-filter-0')]/b");
        public static By chk_city3 = By.XPath("//section[@id='search-filters']/div/section[3]/ul/li/label/b");
        public static By chk_city4 = By.Id("city-filter-0");

        public static By chk_institution1 = By.Id("industry-filter-0");
        public static By chk_campus1 = By.Id("job_status-filter-0");
        public static By expand_Filtertype = By.CssSelector("ul.search-filter-list.expandable-childlist-open");
        public static By Search_Error = By.Id("search-ajax-error");
        public static By section_appliedFilter = By.Id("applied-filters");
        public static By appliedFilter_Link = By.CssSelector("a.filter-button");
        public static By btn_searchfilterclear = By.Id("search-filter-clear");
        public static By lst_Filterradius = By.Id("filter-distance-radius");
        public static By lst_Filterradius1 = By.XPath("//*[contains(@id , 'search-radius') or contains(@class , 'search-radius')]");
        //*****************************************************************

        //job alert
        //public static By txt_keywordSearch = By.Name("k");
        //public static By btn_Search = By.XPath("//button[contains(@id,'search-submit')]");
        public static By txt_emailAddress = By.Name("EmailAddress");
        public static By txt_alertCategory = By.Name("Category");
        public static By txt_alertLocation = By.Name("Location");
        public static By txt_MobilePhone = By.Name("MobilePhone");
        public static By txt_Company = By.Name("Company");
        public static By txt_title = By.Name("Title");
        public static By txt_FirstName = By.Name("FirstName");
        public static By txt_LastName = By.Name("LastName");

        //public static By categoryToggle = By.Id("category-toggle");
        public static By filterCategory = By.ClassName("filter");
        public static By btn_alertSubmit = By.CssSelector("input[type='submit']");
        public static By btn_add = By.ClassName("keyword-add");
        public static By addedKeyword = By.CssSelector("span.keyword-text");
        public static By SuccessMessage = By.CssSelector("p.form-field.form-message > b");
        public static By alertCategoryList = By.XPath("//ul[contains(@id,'category-mindreader')]");
        public static By removePrevious = By.LinkText("Remove");

        public static By jobAlertKeyword = By.Name("JoybAlertKeyword");
        // public static By jobAlertAutoPopulateLocation = By.Id("form-field-11fadc1285-location-mindreader");
        public static By jobAlertAutoPopulateLocation = By.XPath("//ul[contains(@id,'location-mindreader')]");

        //RSS Feed

        public static By rssFeedHeader = By.Id("feedHeader");
        public static By rssJobsList = By.Id("feedContent");

        //JobMatching

        //public static By link_JobMatching = By.XPath("//*[contains(@data-callout-action,'job matching') or contains(@class,'matchingbutton')]");
        //public static By link_JobMatching1 = By.CssSelector("button.limatch");
        //public static By link_JobMatching2 = By.XPath("//a[contains(@href, 'job-match') or contains(@href,'linkedin')]");
        //public static By link_JobMatching3 = By.XPath("//a[contains(@href,'job-match')]");
        //public static By txt_LinkedInEmailAddress = By.Id("session_key-oauthAuthorizeForm");
        //public static By txt_LinkedInEmailAddress1 = By.Id("session_key-oauth2SAuthorizeForm");
        //public static By txt_LinkedInPassword = By.Id("session_password-oauthAuthorizeForm");
        //public static By txt_LinkedInPassword1 = By.Id("session_password-oauth2SAuthorizeForm");
        //public static By txt_LinkedInEmailAddress2 = By.Id("session_key-login");
        //public static By txt_LinkedInPassword2 = By.Id("session_password-login");
        //public static By btn_LinkedInLogin2 = By.Name("signin");
        //public static By btn_LinkedInLogin = By.Name("authorize");
        //public static By Edit_btn = By.XPath("//main[@id='content']/section/div/button");
        //public static By btn_Filter = By.Id("ui-id-1");

        ////Rajesh
        ////  public static By txt_AddLocation = By.Id("job-matching-add-location");
        //public static By txt_AddLocation = By.XPath("//input[contains(@id,'job-matching-add-location')]");
        ////----
        //public static By Searchbutton = By.XPath("//*[@class = 'search-tab search-button' or  @class = 'advanced-search-button' or @class= 'search-toggle']");
        //public static By autopopulate_Location = By.Id("job-matching-add-location-mindreader");
        //public static By autopopulate_Location2 = By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]");
        ////span[text()='Search jobs']
        ////public static By btn_AddLocation = By.XPath("//button[contains(text(),'Add')]");
        //public static By btn_AddLocation = By.XPath("//button[contains(text(),'Ajouter') or contains(@class,'location-add') or contains(text(),'Add')]");
        ////  public static By LocationFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list location-filter-list') or contains(@class,'job-matching-filocation-addlter-list location-filter-list')]");
        //public static By LocationFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list location-filter-list') or contains(@class,'job-matching-filocation-addlter-list location-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open location-filter-list')]");
        ////public static By del_LocationFilterOption = By.XPath("//input[contains(@id,'location-filter-2')]/button");

        //public static By link_EndorsedSkillsFilter = By.XPath("//a[contains(text(),'Skills') or contains(text(),'Erfahrung') or contains(text(),'Habilidade') or contains(text(),'能力')  or contains(text(), 'Qualifikationen')  or contains(text(),'Compétences') or contains(text(),'Habilidades') or contains(text(),'スキル') or contains(text(),'Навыки')]");
        //public static By link_SkillsFilter = By.XPath("//legend//*[contains(text(),'Skills') or contains(text(),'Umiejętności') or contains(text(),'技能') or contains(text(),'Készségek') or contains(text(),'Kompetenzen') or  contains(text(),'Habilidades endosados') or contains(text(),'スキル') or contains(text(), 'Qualifikationen')]");
        //public static By link_Skills = By.XPath("//form[@id='job-matching-filters']/fieldset[2]");
        //public static By link_Skills1 = By.XPath("//form[@id='job-matching-filters']/fieldset[2]/div/ul");
        //public static By link_Skills2 = By.XPath("//form[@data-filter-group-type='Skills']");
        ////Rajesh--
        //public static By link_Skills3 = By.XPath("//*[@id='job-matching-filters']/fieldset[2]/legend/a");
        ////---------
        //public static By SkillFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list skills-filter-list') or contains(@class,'job-matching-filocation-addlter-list skills-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open skills-filter-list')]");
        //// public static By SkillFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list skills-filter-list') or contains(@class,'job-matching-filocation-addlter-list skills-filter-list')]");
        ////public static By link_ExperienceFilter = By.XPath("//a[contains(text(),'Experience')]");
        ////public static By link_ExperienceFilter = By.XPath("//legend/a[contains(text(),'Experience')]");
        ////public static By ExperienceFilter_expand = By.ClassName("job-matching-filter-list experience-filter-list");
        //public static By link_ExperienceFilter = By.XPath("//legend/*[contains(text(),'Experience') or contains(text(),'Erfahrung') or contains(text(), 'posición')]");
        //public static By link_ExperienceFilter1 = By.XPath("//a[contains(text(),'Experience')  or contains(text(),'Expérience') or contains(text(),'Experiência') or contains(text(),'Experiencia')or contains(text(),'Expereincia')or contains(text(),'Expereincia') or contains(text(),'経験') or contains(text(), 'Опыт')]");
        //public static By link_ExperienceFilter2 = By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend");
        ////Rajesh--
        //public static By link_PositonFilter = By.XPath("//a[contains(text(),'Position') or contains(text(),'经验')] | //a[contains(text(),'Erfahrung')]");
        ////----
        //public static By link_ExperienceFilter3 = By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend/a");

        //public static By ExperienceFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list experience-filter-list') or contains(@class,'job-matching-filocation-addlter-list experience-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open experience-filter-list')]");
        ////public static By ExperienceFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list experience-filter-list')or contains(@class,'job-matching-filocation-addlter-list experience-filter-list')]");
        //public static By JobMatchingLogoutLink = By.XPath("//a[contains(@href,'/job-match/logout')]");
        //public static By JobMatchingFrame = By.XPath("//*[contains(@id,'job-matching-filters')]");
        //public static By btn_Location2 = By.XPath("//div[contains(@class,'expandable-parent')]/legend/a[contains(text(),'Location')]");
        //public static By btn_Location = By.XPath("//legend[contains(@class,'expandable-parent')]/*[contains(text(),'Location') or contains(text(),'Standort') or contains(text(),'Ubicacion') or contains(text(), '地域別仕事')]");
        //public static By btn_Location1 = By.XPath("//*[contains(@class,'expandable-parent')]/a[contains(text(),'Location')]");
        //public static By btn_Location3 = By.XPath("//a[contains(text(),'Location') or contains(text(),'Local') or  contains(text(),'ロケーション') or contains(text(),'位置') or contains(text(),'Lokalizacja') or contains(text(),'地区/省份') or contains(text(),'Földrajzi hely') or contains(text(),'地區') or contains(text(),'Ort') or contains(text(),'Lieu') or contains(text(),'地点') or contains(text(), 'Berufliche Erfahrungen') or contains(text(),'Emplacement') or contains(text(),'Localização') or contains(text(),'Ubicación')or contains(text(),'Ubricacion') or contains(text(),'Unidade') or contains(text(),'Localisation') or contains(text(),'勤務地') or contains(text(), 'Établissement') or contains(text(),'Местонахождение')]");
        ////a[contains(text(),'Location')]or contains(text(),'地區') 
        ////Changes for be.emploi.primark.com client ->Rajesh
        //public static By btn_Location4 = By.XPath("//a[contains(text(),'Localisation')] | //a[contains(text(),'Plaats')] | //a[contains(text(),'地區')] | //a[contains(text(),'Localização')] | //a[contains(text(),'Luogo')] | //a[contains(text(),'Localización')] | //a[contains(text(),'Plats')]");
        //public static By Error_Retriving = By.XPath("//main[contains(@id='content')]");
        //public static By Gateway_Timeout = By.XPath("//h1[contains(text(),'504 Gateway Time-out')]");
        ////public static By JobMatchingLogoutLink2 = By.XPath("//a[contains(text(),'logOut')]");

        public static By link_JobMatching = By.XPath("//*[contains(@data-callout-action,'job matching') or contains(@class,'matchingbutton') or contains(@class, 'job-matching-callout')]");
        public static By link_JobMatching1 = By.CssSelector("button.limatch");
        public static By link_JobMatching2 = By.XPath("//a[contains(@href, 'job-match') or contains(@href,'linkedin')]");
        public static By link_JobMatching3 = By.XPath("//a[contains(@href,'job-match')]");
        public static By link_JobMatching4 = By.XPath("//a[@data-callout-action='job matching' or contains(@href,'/job-match/')]");
        // Boobalan added xpath below -
        public static By btn_Location5 = By.XPath("//legend[contains(@class,'expandable-parent')]/a/font[contains(text(),'Location')]");
        //Krishna - IKEA sites - ar , nl, da & lv-jobs.about.ikea.com
        public static By btn_Location6 = By.XPath("//legend/a[contains(text(),'Atrašanās vieta')] | //legend/a[contains(text(),'Plaats')] | //legend/a[contains(text(),'Placering')] | //legend/a[contains(text(),'الموقع')]");

        public static By open_Jobmatching = By.XPath("//button[@id='search-expander']");
        public static By open_Jobmatching1 = By.XPath("//button[@id='hero-search']");
        public static By txt_LinkedInEmailAddress = By.Id("session_key-oauthAuthorizeForm");
        public static By txt_LinkedInEmailAddress1 = By.Id("session_key-oauth2SAuthorizeForm");
        public static By txt_LinkedInPassword = By.Id("session_password-oauthAuthorizeForm");
        public static By txt_LinkedInPassword1 = By.Id("session_password-oauth2SAuthorizeForm");
        public static By txt_LinkedInEmailAddress2 = By.Id("session_key-login");
        public static By txt_LinkedInPassword2 = By.Id("session_password-login");
        public static By btn_LinkedInLogin = By.Name("authorize");
        public static By btn_LinkedInLogin2 = By.Name("signin");
        public static By Edit_btn = By.XPath("//main[@id='content']/section/div/button");
        public static By btn_Filter = By.Id("ui-id-1");

        //Rajesh
        //  public static By txt_AddLocation = By.Id("job-matching-add-location");
        public static By txt_AddLocation = By.XPath("//input[contains(@id,'job-matching-add-location')]");
        //----
        public static By Searchbutton = By.XPath("//*[@class = 'search-tab search-button' or  @class = 'advanced-search-button' or @class= 'search-toggle']");
        public static By autopopulate_Location = By.Id("job-matching-add-location-mindreader");
        public static By autopopulate_Location2 = By.XPath("//ul[contains(@id,'job-matching-add-location-mindreader')]");
        //span[text()='Search jobs']
        //public static By btn_AddLocation = By.XPath("//button[contains(text(),'Add')]");
        public static By btn_AddLocation = By.XPath("//button[contains(text(),'Ajouter') or contains(@class,'location-add') or contains(text(),'Add')]");
        //  public static By LocationFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list location-filter-list') or contains(@class,'job-matching-filocation-addlter-list location-filter-list')]");
        public static By LocationFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list location-filter-list') or contains(@class,'job-matching-filocation-addlter-list location-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open location-filter-list') or contains(@class, 'filters__list job-matching-filter-list js-toggle-this location-filter-list js-toggle-this toggle-open') or contains(@class, 'job-matching-filter-list search-filters__list location-filter-list')]");
        //public static By del_LocationFilterOption = By.XPath("//input[contains(@id,'location-filter-2')]/button");

        public static By link_EndorsedSkillsFilter = By.XPath("//a[contains(text(),'Skills') or contains(text(),'Erfahrung') or contains(text(),'Habilidade') or contains(text(),'能力')  or contains(text(), 'Qualifikationen')  or contains(text(),'Compétences') or contains(text(),'Habilidades') or contains(text(),'スキル') or contains(text(),'Навыки')]");
        public static By link_SkillsFilter = By.XPath("//legend//*[contains(text(),'Skills') or contains(text(),'Umiejętności') or contains(text(),'技能') or contains(text(),'Készségek') or contains(text(),'Kompetenzen') or  contains(text(),'Habilidades endosados') or contains(text(),'スキル') or contains(text(), 'Qualifikationen')]");
        public static By link_Skills = By.XPath("//form[@id='job-matching-filters']/fieldset[2]");
        public static By link_Skills1 = By.XPath("//form[@id='job-matching-filters']/fieldset[2]/div/ul");
        public static By link_Skills2 = By.XPath("//form[@data-filter-group-type='Skills']");
        //Rajesh--
        public static By link_Skills3 = By.XPath("//*[@id='job-matching-filters']/fieldset[2]/legend/a");
        public static By link_Skills4 = By.XPath("//legend/a[contains(text(),'Prasmes')] | //legend/a[contains(text(),'Vaardigheden')] | //legend/a[contains(text(),'Færdigheder')] | //legend/a[contains(text(),'المهارات')]");
        //---------
        public static By SkillFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list skills-filter-list') or contains(@class,'job-matching-filocation-addlter-list skills-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open skills-filter-list') or contains(@class, 'filters__list job-matching-filter-list js-toggle-this skills-filter-list js-toggle-this toggle-open') or contains(@class, 'job-matching-filter-list search-filters__list skills-filter-list')]");
        // public static By SkillFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list skills-filter-list') or contains(@class,'job-matching-filocation-addlter-list skills-filter-list')]");
        //public static By link_ExperienceFilter = By.XPath("//a[contains(text(),'Experience')]");
        //public static By link_ExperienceFilter = By.XPath("//legend/a[contains(text(),'Experience')]");
        //public static By ExperienceFilter_expand = By.ClassName("job-matching-filter-list experience-filter-list");
        public static By link_ExperienceFilter = By.XPath("//legend/*[contains(text(),'Experience') or contains(text(),'Erfahrung') or contains(text(), 'posición')]");
        public static By link_ExperienceFilter1 = By.XPath("//a[contains(text(),'Experience')  or contains(text(),'Expérience') or contains(text(),'Experiência') or contains(text(),'Experiencia')or contains(text(),'Expereincia')or contains(text(),'Expereincia') or contains(text(),'経験') or contains(text(), 'Опыт')]");
        public static By link_ExperienceFilter2 = By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend");
        public static By link_ExperienceFilter3 = By.XPath("//form[@id='job-matching-filters']/fieldset[3]/legend/a");
        public static By link_ExperienceFilter4 = By.XPath("//legend/a[contains(text(),'Pieredze')] | //legend/a[contains(text(),'Ervaring')] | //legend/a[contains(text(),'Erfaring')] | //legend/a[contains(text(),'الخبرة')]");
        public static By link_PositonFilter = By.XPath("//a[contains(text(),'Position') or contains(text(),'经验')] | //a[contains(text(),'Erfahrung')]");
        //----
        

        public static By ExperienceFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list experience-filter-list') or contains(@class,'job-matching-filocation-addlter-list experience-filter-list') or contains(@class,'job-matching-filter-list expandable-childlist-open experience-filter-list') or contains(@class, 'filters__list job-matching-filter-list js-toggle-this experience-filter-list js-toggle-this toggle-open') or contains(@class, 'job-matching-filter-list search-filters__list experience-filter-list')]");
        //public static By ExperienceFilterCheckBox = By.XPath("//ul[contains(@class,'job-matching-filter-list experience-filter-list')or contains(@class,'job-matching-filocation-addlter-list experience-filter-list')]");
        public static By JobMatchingLogoutLink = By.XPath("//a[contains(@href,'/job-match/logout') or contains(@href,'/job_match/logout')]");
        public static By JobMatchingFrame = By.XPath("//*[contains(@id,'job-matching-filters')]");
        public static By btn_Location2 = By.XPath("//div[contains(@class,'expandable-parent')]/legend/a[contains(text(),'Location')]");
        public static By btn_Location = By.XPath("//legend[contains(@class,'expandable-parent')]/*[contains(text(),'Location') or contains(text(),'Standort') or contains(text(),'Ubicacion') or contains(text(), '地域別仕事')]");
        public static By btn_Location1 = By.XPath("//*[contains(@class,'expandable-parent')]/a[contains(text(),'Location')]");
        public static By btn_Location3 = By.XPath("//a[contains(text(),'Location') or contains(text(),'Local') or  contains(text(),'ロケーション') or contains(text(),'位置') or contains(text(),'Lokalizacja') or contains(text(),'地区/省份') or contains(text(),'Földrajzi hely') or contains(text(),'地區') or contains(text(),'Ort') or contains(text(),'Lieu') or contains(text(),'地点') or contains(text(), 'Berufliche Erfahrungen') or contains(text(),'Emplacement') or contains(text(),'Localização') or contains(text(),'Ubicación')or contains(text(),'Ubricacion') or contains(text(),'Unidade') or contains(text(),'Localisation') or contains(text(),'勤務地') or contains(text(), 'Établissement') or contains(text(),'Местонахождение')]");
        //a[contains(text(),'Location')]or contains(text(),'地區') 
        //Changes for be.emploi.primark.com client ->Rajesh
        public static By btn_Location4 = By.XPath("//a[contains(text(),'Localisation')] | //a[contains(text(),'Plaats')] | //a[contains(text(),'地區')] | //a[contains(text(),'Localização')] | //a[contains(text(),'Luogo')] | //a[contains(text(),'Localización')] | //a[contains(text(),'Plats')]");
        public static By Error_Retriving = By.XPath("//main[contains(@id='content')]");
        public static By Gateway_Timeout = By.XPath("//h1[contains(text(),'504 Gateway Time-out')]");
        //public static By JobMatchingLogoutLink2 = By.XPath("//a[contains(text(),'logOut')]");

        public static By btn_close = By.XPath("//button[text()='Close']");
       
        //GA code verification
        public static By link_googleAnalytics = By.XPath("//script[contains(@src,'www.google-analytics.com/analytics.js')]");
        //Advance search latest
        public static By btn_SearchJobsAdvance = By.XPath("//button[@aria-label='Search Job Form Open and Close']");
        public static By btn_filter = By.XPath("//button[contains(@class,'filters-toggle')]");

        //sitemap latest
        public static By siteMap6 = By.XPath("//a[text()='Sitemap' or text()='Site Map' or text()='MAPA DEL SITIO']");

        //Ramya Added New
        public static By searchResultLink5 = By.XPath("//section[@id='search-results-list']");
          //Ramya Added New
       // public static By searchResultLink5 = By.XPath("//section[@id='search-results-list']");
        //Ramya Added New
        public static By btn_Search_New = By.XPath("//button[contains(@id,'search-submit') ]");
        //Ramya Added New 
        public static By btn_searchjobs_new_2 = By.XPath("//button[contains(@class, 'search-toggle')]");

        //explore.lockheedmartinjobs.com
        public static By keywordBoxNew = By.XPath("//input[@name='q']");
        public static By searchButtonnew2 = By.Id("lhm-search-btn");

        public static By SearchResultsArea = By.Id("search-response");
        public static By SearchResultFistJob = By.XPath("(//div[@class='sj-result-list']//div[@class='sj-result']//a)[1]");
        public static By btn_searchJObs = By.XPath("//a[@href='open_search']");
        public static By new_keywordSearch = By.XPath("//input[@class='search-keyword']");

        //Krishna added
        public static By jobmatching_errorPage = By.XPath("//p[contains(text(),'An error has occurred.')]");

        //Mani added----QAA-798---Enhanced advance search feature
        public static By txt_locationSearch = By.XPath("//input[contains(@id,'search-location')]");
        public static By btn_Search7 = By.XPath("(//button[contains(@id,'search-submit')])[2]");
        public static By txt_locSearch = By.XPath("(//input[@name='l'])[2]");
       // public static By radius_box = By.XPath("(//*[@name='r'])[2]");

    }

}
