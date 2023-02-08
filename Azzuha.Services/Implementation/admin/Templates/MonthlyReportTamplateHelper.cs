using Azzuha.DataViewModels.Enum;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azzuha.Services.Implementation.admin.Templates
{
    public class MonthlyReportTamplateHelper
    {
        private string finalHTML = "";
        private DateTime startDate = new DateTime();
        private DateTime endDate = new DateTime();
        private Guid userId = Guid.Empty;
        private Guid profileTypeId = Guid.Empty;

        private IFitnessTrainerService trainerService;
        private IFitnessNutritionService nutritionService;
        private IFitnessGymService gymService;
        private IFitnessClassService classService;
        private IFitnessAppService appService;
        private IMonthlyReport monthlyReport;

        public MonthlyReportTamplateHelper(DateTime startDate,
            DateTime endDate,
            Guid userId,
            Guid profileTypeId,
            IFitnessTrainerService trainerService,
            IFitnessNutritionService nutritionService,
            IFitnessGymService gymService,
            IFitnessClassService classService,
            IFitnessAppService appService,
            IMonthlyReport monthlyReport)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.userId = userId;
            this.profileTypeId = profileTypeId;
            this.trainerService = trainerService;
            this.nutritionService = nutritionService;
            this.gymService = gymService;
            this.classService = classService;
            this.appService = appService;
            this.monthlyReport = monthlyReport;
            finalHTML = "";
        }

        public async Task<string> GetMonthlyReportHtml()
        {
            GetStatsRequest countRequest = new GetStatsRequest { StartDate = startDate, EndDate = endDate, Frequency = 2 };
            GetStatsRequest graphRequest = new GetStatsRequest { StartDate = endDate.AddMonths(-11), EndDate = endDate, Frequency = 2 };

            finalHTML = @"<!DOCTYPE html><html lang='en'><head> <meta charset='UTF-8'> <meta name='viewport' content='width=device-width, initial-scale=1.0'> <title>Monthly Report</title> <link rel='shortcut icon' href='img/logo.svg'> <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'> <script src='https://www.chartjs.org/dist/2.9.3/Chart.min.js'></script></head><style>img{max-width: 100%;}body{margin: 0; padding: 0; font-family: Arimo, Helvetica Neue, Roboto, sans-serif;}main{width: 100%; height: auto; display: inline-block; margin: 0; padding: 0;}nav{height: auto; background-color: #F3F3F3; border-bottom: 1px solid #FFC013; float: left;}.nav-left{width: 50%; float: left; display: inline-block; position: relative;}.nav-right{width: 50%; float: left; display: inline-block; position: relative;}nav p{margin: 0; font-size: 16px; color: #333333; text-align: right; line-height: 100px;}.logo{display: inline-block; max-width: 80px; line-height: 0; margin-top: 15px; margin-bottom: 15px;}nav, .user-panel, .total-panel, .graph-panel, footer{width: calc(100% - 60px); height: auto; display: inline-block; padding-left: 30px; padding-right: 30px;}.user-panel{background-color: #F3F3F3; padding-top: 20px; padding-bottom: 20px; float: left;}ul{padding-left: 0;}.user-panel .img-pnl{display: inline-block; width: 300px; height: 300px; display: inline-block; float: left; border-radius: 5px;}.user-panel .img-pnl img{border-radius: 5px;}.user-panel .txt-pnl{display: inline-block; width: calc(100% - 320px); display: inline-block; float: left; padding-left: 20px;}.user-panel .txt-pnl h1{text-transform: capitalize; color: #333333; font-size: 20px; font-weight: bold; margin-top: 5px; margin-bottom: 15px;}.user-panel .txt-pnl h2{text-transform: capitalize; color: #333333; font-size: 16px; font-weight: bold; margin-top: 0px; margin-bottom: 10px;}.user-panel .txt-pnl p{color: #333333; font-size: 16px; margin-bottom: 20px;}.user-panel .txt-pnl ul{display: inline-block; width: 100%; position: relative; margin: 0; margin-bottom: 15px;}.user-panel .txt-pnl ul li{display: inline-block; position: relative; float: left; margin-right: 15px;}.user-panel .txt-pnl ul li:not(:first-child):before{position: absolute; content: ''; left: -8px; top: 2px; height: 15px; width: 1.5px; background-color: #707070;}.user-panel .txt-pnl ul li, .user-panel .txt-pnl ul li a{font-size: 16px; color: #707070; text-decoration: none;}.user-panel .txt-pnl ul.grey-list li, .user-panel .txt-pnl ul.grey-list li a{color: #9E9E9E;}.user-panel .txt-pnl ul.grey-list li::before{background-color: #9E9E9E;}.graph-half-panel{width: calc(50% - 60px); height: auto; display: inline-block; padding-left: 30px; padding-right: 30px; float: left; padding-bottom: 30px;}.graph-panel, .total-panel{padding-bottom: 15px;}.graph-half-panel p, .graph-panel p{font-size: 16px; font-weight: bold; color: #333333; margin-bottom: 20px; border-bottom: 2px solid #DCDCDC; padding-bottom: 5px; margin-top: 10px;}.total-list{display: inline-block; float: right; position: relative; margin: 0; margin-top: 15px;}.total-list li{display: inline-block; position: relative; float: left; background-color: #F3F3F3; border-radius: 8px; padding: 10px; margin-left: 20px; padding: 0px 10px 15px 10px; min-width: 100px;}.total-list li p{font-size: 12px; color: #333333; margin-bottom: 5px;}.total-list li h6{font-weight: bold; font-size: 20px; color: #333333; margin: 0; text-align: center;}footer{height: 80px; line-height: 80px; background-color: #F3F3F3; border-top: 1px solid #DFDFDF;}.footer-left{display: inline-block; width: 50%; float: left;}.footer-right{display: inline-block; width: 50%; float: right; text-align: right;}.footer-right p, .footer-left p{color: #707070; font-size: 16px; margin: 0;}.start-common{font-size: 16px; -webkit-text-stroke: 1px orange; -webkit-background-clip: text; -webkit-text-fill-color: transparent;} .user-panel .txt-pnl ul.grey-list{max-height: 63px; overflow: hidden;}</style><body style='background-color: #fff'> <main> <nav> <div class='nav-left'> <a class='logo' target='_blank' href='https://fitcentrapp.stagingdesk.com/'><img src='http://fitcentr.stagingdesk.com/img/logo.svg' alt='Logo'></a> </div><div class='nav-right'> <p> <b>Monthly Report </b> - " + startDate.ToString("MMM-yyyy") + "</p></div></nav>";

            if (profileTypeId.Equals(Guid.Parse(EProfileType.FitnessTrainner)))
            {
                finalHTML += TrainerPersonalBlock();
            }
            else if (profileTypeId.Equals(Guid.Parse(EProfileType.FitnessApp)))
            {
                finalHTML += AppPersonalBlock();
            }
            else if (profileTypeId.Equals(Guid.Parse(EProfileType.BusinessFitnessGym)))
            {
                finalHTML += GymPersonalBlock();
            }
            else if (profileTypeId.Equals(Guid.Parse(EProfileType.BusinessFitnessClass)))
            {
                finalHTML += ClassPersonalBlock();
            }
            else if (profileTypeId.Equals(Guid.Parse(EProfileType.BusinessFitnessNeutrationApp)))
            {
                finalHTML += NeutrationAppPersonalBlock();
            }

            finalHTML += TotalCountBlock();

            var viewGraph = await monthlyReport.ProfileVisitChartStats(graphRequest, false, userId);

            finalHTML += @" <div class='graph-panel'> <p>Monthly Views</p><div id='canvas-holder-bar-view' style='width:100%'> <canvas id='chart-area-bar-view'></canvas> </div></div><div class='graph-half-panel'> <p>June Views by Countries</p><div id='canvas-holder-pie-country' style='width:100%'> <canvas id='chart-area-pie-country'></canvas> </div></div><div class='graph-half-panel'> <p>June Views by USA States</p><div id='canvas-holder-pie-state' style='width:100%'> <canvas id='chart-area-pie-state'></canvas> </div></div><div class='graph-panel' style='margin-top:100px;'> <p>Monthly " + (profileTypeId.Equals(Guid.Parse(EProfileType.FitnessTrainner)) ? "Messages" : "Clicks") + "</p><div id='canvas-holder-bar-message' style='width:100%'> <canvas id='chart-area-bar-message'></canvas> </div></div><div class='graph-panel'> <p>Monthly Followers</p><div id='canvas-holder-bar-follower' style='width:100%'> <canvas id='chart-area-bar-follower'></canvas> </div></div><div class='graph-panel' style='margin-top:350px;'> <p>Monthly Marketing Material </p><div id='canvas-holder-bar-market' style='width:100%'> <canvas id='chart-area-bar-market'></canvas> </div></div><footer> <div class='footer-left'> <p><b>Monthly Report</b> - June-2020</p></div><div class='footer-right'> <p><b>Report Generated By:</b> Fitcentr</p></div></footer> </main> <script>var pieCountryLabel='Countries'; var pieStateLabel='States'; var barViewLabel='Views'; var barMessageLabel='Messages'; var barFollowerLabel='Followers'; var barMarketUploadLabel='Upload'; var barMarketUsedLabel='Used'; var randomColor=function(){r=Math.floor(Math.random() * 200); g=Math.floor(Math.random() * 200); b=Math.floor(Math.random() * 200); return 'rgb(' + r + ', ' + g + ', ' + b + ')';}; var configBarView={type: 'bar', data:{datasets: [{data: [";

            foreach (var view in viewGraph)
            {
                finalHTML += view.VisitCount + " ,";
            }

            finalHTML += @" ], backgroundColor: '#034978', label: barViewLabel}], labels: [";

            foreach (var view in viewGraph)
            {
                finalHTML += "'" + view.StartDate + "', ";
            }

            if (profileTypeId.Equals(Guid.Parse(EProfileType.FitnessTrainner)))
            {
                var messageGraph = await monthlyReport.ProfileMessageChartStats(graphRequest, userId);

                finalHTML += @"]}, options:{responsive: true,}}; var configBarMessage={type: 'bar', data:{datasets: [{data: [";

                foreach (var message in messageGraph)
                {
                    finalHTML += message.VisitCount + ", ";
                }

                finalHTML += @"], backgroundColor: '#3B97FF', label: barMessageLabel}], labels: [";

                foreach (var message in messageGraph)
                {
                    finalHTML += "'" + message.StartDate + "', ";
                }
            }
            else
            {
                var clickedGraph = await monthlyReport.ProfileVisitChartStats(graphRequest, true, userId);

                finalHTML += @"]}, options:{responsive: true,}}; var configBarMessage={type: 'bar', data:{datasets: [{data: [";

                foreach (var click in clickedGraph)
                {
                    finalHTML += click.VisitCount + ", ";
                }

                finalHTML += @"], backgroundColor: '#3B97FF', label: barMessageLabel}], labels: [";

                foreach (var click in clickedGraph)
                {
                    finalHTML += "'" + click.StartDate + "', ";
                }
            }


            var followerGraph = await monthlyReport.ProfileFollowerChartStats(graphRequest, userId);
            finalHTML += @"]}, options:{responsive: true,}}; var configBarFollower={type: 'bar', data:{datasets: [{data: [";

            foreach (var follower in followerGraph)
            {
                finalHTML += follower.VisitCount + ", ";
            }

            finalHTML += @"], backgroundColor: '#4BC0C0', label: barFollowerLabel}], labels: [";

            foreach (var follower in followerGraph)
            {
                finalHTML += "'" + follower.StartDate + "', ";
            }

            var marketGraph = await monthlyReport.ProfileMarketingMetirialChartStats(graphRequest, userId);
            finalHTML += @"]}, options:{responsive: true,}}; var configBarMarket={type: 'bar', data:{datasets: [{data: [";

            foreach (var market in marketGraph)
            {
                finalHTML += market.Total + ", ";
            }

            finalHTML += @"], backgroundColor: '#FF9F40', label: barMarketUploadLabel},{data: [";

            foreach (var market in marketGraph)
            {
                finalHTML += market.Used + ", ";
            }

            finalHTML += @"], backgroundColor: '#FFCD56', label: barMarketUsedLabel}], labels: [";

            foreach (var market in marketGraph)
            {
                finalHTML += "'" + market.StartDate + "', ";
            }

            countRequest.Frequency = 1;
            var countryGraph = await monthlyReport.ProfileVisitPieChartStats(countRequest, false, userId);
            finalHTML += @"]}, options:{responsive: true,}}; var configPieCountry={type: 'pie', data:{datasets: [{data: [";

            foreach (var country in countryGraph)
            {
                finalHTML += country.Count + ", ";
            }

            finalHTML += @"], backgroundColor: [";

            foreach (var country in countryGraph)
            {
                finalHTML += "randomColor(), ";
            }

            finalHTML += @"], label: pieCountryLabel}], labels: [";

            foreach (var country in countryGraph)
            {
                finalHTML += "'" + country.Country + "', ";
            }

            countRequest.Frequency = 2;
            var stateGraph = await monthlyReport.ProfileVisitPieChartStats(countRequest, false, userId);
            finalHTML += @"]}, options:{responsive: true, plugins:{datalabels:{display: true,},}, legend:{position: 'right',},}}; var configPieState={type: 'pie', data:{datasets: [{data: [";

            foreach (var state in stateGraph)
            {
                finalHTML += state.Count + " ,";
            }

            finalHTML += @"], backgroundColor: [";

            foreach (var state in stateGraph)
            {
                finalHTML += "randomColor(), ";
            }

            finalHTML += @"], label: pieStateLabel}], labels: [";

            foreach (var state in stateGraph)
            {
                finalHTML += "'" + state.Country + "', ";
            }

            finalHTML += @"]}, options:{responsive: true, plugins:{datalabels:{display: true,},}, legend:{position: 'right',},}}; window.onload=function(){var ctxBarView=document.getElementById('chart-area-bar-view').getContext('2d'); var ctxPieCountry=document.getElementById('chart-area-pie-country').getContext('2d'); var ctxPieState=document.getElementById('chart-area-pie-state').getContext('2d'); var ctxBarMessage=document.getElementById('chart-area-bar-message').getContext('2d'); var ctxBarFollower=document.getElementById('chart-area-bar-follower').getContext('2d'); var ctxBarMarket=document.getElementById('chart-area-bar-market').getContext('2d'); new Chart(ctxBarView, configBarView); new Chart(ctxPieCountry, configPieCountry); new Chart(ctxPieState, configPieState); new Chart(ctxBarMessage, configBarMessage); new Chart(ctxBarFollower, configBarFollower); new Chart(ctxBarMarket, configBarMarket);}; </script></body></html>";

            return finalHTML;
        }

        public string TrainerPersonalBlock()
        {
            try
            {
                TrainerProfileResponse profile = trainerService.GetProfile(userId, false, Guid.Empty);
                string[] rating = profile.Personal.Rating.ToString().Split('.');
                int fullStars = Convert.ToInt32(rating[0]);
                int emptyStars = 5 - (Convert.ToInt32(rating[0]) + 1);
                int realRating = rating.Count() >= 2 ? Convert.ToInt32(rating[1]) : 0;

                string personalblockHtml = " <div class='user-panel'> <div class='img-pnl'> <img src='" +
                    profile.Personal.ImageUrl
                    + "' alt='user'> </div><div class='txt-pnl'> <h1>" +
                    profile.Personal.Name
                    + "</h1> <p>Fitness Trainer</p>";

                //"<ul class='grey-list'> <li> ";

                //Starts 
                //for (int i = 0; i < fullStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 100%, #F3F3F3 100%);'>";
                //}

                //if (fullStars < 5)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange " + realRating + "0%, #F3F3F3 " + realRating + "0%);'>";
                //}

                //for (int i = 0; i < emptyStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 0%, #F3F3F3 0%);'>";
                //}

                //personalblockHtml += "</li><li><a href='javascript:void(0);'>" +
                //    profile.Personal.ratingCount
                //    + "</a></li></ul>" +
                personalblockHtml += " <h2>Fitness Facilities: </h2> <ul>";

                foreach (var service in profile.Services)
                {
                    personalblockHtml += "<li>" + service + "</li>";
                }

                personalblockHtml += "</ul> <h2>Areas of Expertise: </h2> <ul class='grey-list'>";

                foreach (var experty in profile.Expertise)
                {
                    personalblockHtml += "<li>" + experty + "</li>";
                }

                personalblockHtml += "</ul> </div></div>";



                return personalblockHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AppPersonalBlock()
        {
            try
            {
                FitnessAppProfileResponse profile = appService.GetProfile(userId, false, Guid.Empty);
                string[] rating = profile.Personal.Rating.ToString().Split('.');
                int fullStars = Convert.ToInt32(rating[0]);
                int emptyStars = (Convert.ToInt32(rating[0]) + 1) - 5;
                int realRating = rating.Count() >= 2 ? Convert.ToInt32(rating[1]) : 0;

                string personalblockHtml = " <div class='user-panel'> <div class='img-pnl'> <img src='" +
                    profile.Personal.ImageUrl
                    + "' alt='user'> </div><div class='txt-pnl'> <h1>" +
                    profile.Personal.AppName
                    + "</h1> <p>Fitness App</p>";
                //"<ul class='grey-list'> <li> ";

                //Starts 
                //for (int i = 0; i < fullStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 100%, #F3F3F3 100%);'>";
                //}

                //personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange " + realRating + "0%, #F3F3F3 " + realRating + "0%);'>";

                //for (int i = 0; i < emptyStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 0%, #F3F3F3 0%);'>";
                //}

                //personalblockHtml += "</li><li><a href='javascript:void(0);'>" +
                //    profile.Personal.ratingCount
                //    + "</a></li> </ul> " +
                personalblockHtml += "<h2>Services: </h2> <ul>";

                foreach (var service in profile.Services)
                {
                    personalblockHtml += "<li>" + service + "</li>";
                }

                personalblockHtml += "</ul> <h2>Workouts: </h2> <ul class='grey-list'>";

                foreach (var experty in profile.Workouts)
                {
                    personalblockHtml += "<li>" + experty + "</li>";
                }

                personalblockHtml += "</ul> </div></div>";



                return personalblockHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GymPersonalBlock()
        {
            try
            {
                GymProfileResponse profile = gymService.GetProfile(userId, false, Guid.Empty);
                string[] rating = profile.Personal.Rating.ToString().Split('.');
                int fullStars = Convert.ToInt32(rating[0]);
                int emptyStars = (Convert.ToInt32(rating[0]) + 1) - 5;
                int realRating = rating.Count() >= 2 ? Convert.ToInt32(rating[1]) : 0;

                string personalblockHtml = " <div class='user-panel'> <div class='img-pnl'> <img src='" +
                    profile.Personal.ImageUrl
                    + "' alt='user'> </div><div class='txt-pnl'> <h1>" +
                    profile.Personal.GymName
                    + "</h1> <p>Gym</p>";
                //"<ul class='grey-list'> <li> ";

                //Starts 
                //for (int i = 0; i < fullStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 100%, #F3F3F3 100%);'>";
                //}

                //personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange " + realRating + "0%, #F3F3F3 " + realRating + "0%);'>";

                //for (int i = 0; i < emptyStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 0%, #F3F3F3 0%);'>";
                //}

                //personalblockHtml += "</li><li><a href='javascript:void(0);'>" +
                //    profile.Personal.ratingCount
                //    + "</a></li></ul>"+
                personalblockHtml += " <h2>Amenities: </h2> <ul>";

                foreach (var experty in profile.Amenities)
                {
                    personalblockHtml += "<li>" + experty + "</li>";
                }

                personalblockHtml += "</ul> </div></div>";

                return personalblockHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ClassPersonalBlock()
        {
            try
            {
                FitnessClassProfileResponse profile = classService.GetProfile(userId, false, Guid.Empty);
                string[] rating = profile.Personal.Rating.ToString().Split('.');
                int fullStars = Convert.ToInt32(rating[0]);
                int emptyStars = (Convert.ToInt32(rating[0]) + 1) - 5;
                int realRating = rating.Count() >= 2 ? Convert.ToInt32(rating[1]) : 0;

                string personalblockHtml = " <div class='user-panel'> <div class='img-pnl'> <img src='" +
                    profile.Personal.ImageUrl
                    + "' alt='user'> </div><div class='txt-pnl'> <h1>" +
                    profile.Personal.Name
                    + "</h1> <p>Classes & Studios</p>";
                //    "<ul class='grey-list'> <li> ";

                ////Starts 
                //for (int i = 0; i < fullStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 100%, #F3F3F3 100%);'>";
                //}

                //personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange " + realRating + "0%, #F3F3F3 " + realRating + "0%);'>";

                //for (int i = 0; i < emptyStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 0%, #F3F3F3 0%);'>";
                //}

                //personalblockHtml += "</li><li><a href='javascript:void(0);'>" +
                //    profile.Personal.ratingCount
                //    + "</a></li></ul> " +
                personalblockHtml += " <h2>Services: </h2> <ul>";

                foreach (var service in profile.Services)
                {
                    personalblockHtml += "<li>" + service + "</li>";
                }

                personalblockHtml += "</ul> <h2>Workouts: </h2> <ul class='grey-list'>";

                foreach (var experty in profile.WorkOut)
                {
                    personalblockHtml += "<li>" + experty + "</li>";
                }

                personalblockHtml += "</ul> </div></div>";



                return personalblockHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string NeutrationAppPersonalBlock()
        {
            try
            {
                FitnessNutritionProfileResponse profile = nutritionService.GetProfile(userId, false, Guid.Empty);
                string[] rating = profile.Personal.Rating.ToString().Split('.');
                int fullStars = Convert.ToInt32(rating[0]);
                int emptyStars = (Convert.ToInt32(rating[0]) + 1) - 5;
                int realRating = rating.Count() >= 2 ? Convert.ToInt32(rating[1]) : 0;

                string personalblockHtml = " <div class='user-panel'> <div class='img-pnl'> <img src='" +
                    profile.Personal.ImageUrl
                    + "' alt='user'> </div><div class='txt-pnl'> <h1>" +
                    profile.Personal.AppName
                    + "</h1> <p>Health & Nutrition Apps</p>";
                //"<ul class='grey-list'> <li> ";

                ////Starts 
                //for (int i = 0; i < fullStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 100%, #F3F3F3 100%);'>";
                //}

                //personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange " + realRating + "0%, #F3F3F3 " + realRating + "0%);'>";

                //for (int i = 0; i < emptyStars; i++)
                //{
                //    personalblockHtml += "<span class='fa fa-star start-common' style='background-image: linear-gradient(90deg, orange 0%, #F3F3F3 0%);'>";
                //}

                //personalblockHtml += "</li><li><a href='javascript:void(0);'>" +
                //    profile.Personal.ratingCount
                //    + "</a></li></ul> " +
                personalblockHtml += "<h2>Services: </h2> <ul>";

                foreach (var service in profile.Services)
                {
                    personalblockHtml += "<li>" + service + "</li>";
                }

                personalblockHtml += "</ul> <h2>Allergies: </h2> <ul class='grey-list'>";

                foreach (var allergy in profile.Allergies)
                {
                    personalblockHtml += "<li>" + allergy + "</li>";
                }

                personalblockHtml += "</ul> <h2>Diets: </h2> <ul class='grey-list'>";

                foreach (var diet in profile.Diets)
                {
                    personalblockHtml += "<li>" + diet + "</li>";
                }

                personalblockHtml += "</ul> </div></div>";



                return personalblockHtml;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string TotalCountBlock()
        {
            try
            {
                GetStatsRequest request = new GetStatsRequest { StartDate = startDate, EndDate = endDate, Frequency = 2 };
                var vists = monthlyReport.ProfileTotalVisitCount(request, false, userId);
                var clicks = monthlyReport.ProfileTotalVisitCount(request, true, userId);
                var messags = monthlyReport.ProfileTotalMessageCount(request, userId);
                var followers = monthlyReport.ProfileTotalFollowerCount(request, userId);

                return @" <div class='total-panel'> <ul class='total-list'> <li> <p>Total Views</p><h6>" +
                    vists +
                    (profileTypeId.Equals(Guid.Parse(EProfileType.FitnessTrainner))
                     ? "</h6> </li><li> <p>Messages</p><h6>" + messags
                    : "</h6> </li><li> <p>Clicks</p><h6>" + clicks)
                    + "</h6> </li><li> <p>New Followers</p><h6>" +
                    followers
                    + "</h6> </li></ul> </div>";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
