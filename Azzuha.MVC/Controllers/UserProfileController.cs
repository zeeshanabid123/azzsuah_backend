using System;
using System.Collections.Generic;
using Azzuha.DataViewModels.Common;
using Azzuha.DataViewModels.Enum;
using Azzuha.DataViewModels.Requests;
using Azzuha.DataViewModels.Response;
using Azzuha.Services.Interface.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Azzuha.AdminPanel.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IFitnessAppService fitnessAppService;
        private readonly IFitnessClassService fitnessClassService;
        private readonly IFitnessCustomerService fitnessCustomerService;
        private readonly IFitnessGymService fitnessGymService;
        private readonly IFitnessNutritionService fitnessNutritionService;
        private readonly IFitnessTrainerService fitnessTrainnerService;
        private readonly IMessageService messageService;
        private readonly IMonthlyReport monthlyReport;


        public UserProfileController(IFitnessAppService FitnessAppService,
            IFitnessClassService FitnessClassService,
            IFitnessCustomerService FitnessCustomerService,
            IFitnessGymService FitnessGymService,
            IFitnessNutritionService FitnessNutritionService,
            IFitnessTrainerService FitnessTrainnerService, IMessageService MessageService,
            IMonthlyReport monthlyReport,
            IConfiguration configuration
            )
        {
            fitnessAppService = FitnessAppService;
            fitnessClassService = FitnessClassService;
            fitnessCustomerService = FitnessCustomerService;
            fitnessGymService = FitnessGymService;
            fitnessNutritionService = FitnessNutritionService;
            fitnessTrainnerService = FitnessTrainnerService;
            messageService = MessageService;
            this.monthlyReport = monthlyReport;
            this.configuration = configuration;

        }


        public IActionResult Index(Guid userId, Guid profileTypeId)
        {
            ViewBag.userId = userId;
            ViewBag.profileTypeId = profileTypeId;
            ViewBag.PageTitle = "Users Detail";
            return View();
        }

        public IActionResult GetProfileDetail(Guid userId, Guid profileId)
        {
            switch (profileId)
            {
                case var g when (g == new Guid(EProfileType.FitnessApp)):
                    return StatusCode(StatusCodes.Status200OK, new Response<FitnessAppProfileResponse>() { isError = false, messages = "", data = fitnessAppService.GetProfile(userId, false, profileId) });
                case var g when (g == new Guid(EProfileType.FitnessTrainner)):
                    return StatusCode(StatusCodes.Status200OK, new Response<TrainerProfileResponse>() { isError = false, messages = "", data = fitnessTrainnerService.GetProfile(userId, false, profileId) });
                case var g when (g == new Guid(EProfileType.BusinessFitnessClass)):
                    return StatusCode(StatusCodes.Status200OK, new Response<FitnessClassProfileResponse>() { isError = false, messages = "", data = fitnessClassService.GetProfile(userId, false, profileId) });
                case var g when (g == new Guid(EProfileType.BusinessFitnessGym)):
                    return StatusCode(StatusCodes.Status200OK, new Response<GymProfileResponse>() { isError = false, messages = "", data = fitnessGymService.GetProfile(userId, false, profileId) });
                case var g when (g == new Guid(EProfileType.BusinessFitnessNeutrationApp)):
                    return StatusCode(StatusCodes.Status200OK, new Response<FitnessNutritionProfileResponse>() { isError = false, messages = "", data = fitnessNutritionService.GetProfile(userId, false, profileId) });
                case var g when (g == new Guid(EProfileType.Customers)):
                    return StatusCode(StatusCodes.Status200OK, new Response<FitnessCustomerProfileResponse>() { isError = false, messages = "", data = fitnessCustomerService.GetProfile(userId, false, profileId) });
                default:
                    break;
            }
            return Ok();

        }

        public IActionResult GetMessageThread(Guid userId)
        {
            GetAllThreadRequest data = new GetAllThreadRequest();
            data.Search = "";
            data.Skip = 0;
            data.Take = 10000;
            return StatusCode(StatusCodes.Status200OK, new Response<List<MessageThreadResponse>>() { isError = false, messages = "", data = messageService.GetAllThreads(data, userId) });

        }
        public IActionResult GetMessageAll(Guid thiredId, Guid friendId, int skip, int take, Guid userId)
        {

            skip = 0;
            take = 10000;
            return StatusCode(StatusCodes.Status200OK, new Response<List<GetAllMessageResponse>>() { isError = false, messages = "", data = messageService.GetAllMessage(thiredId, friendId, skip, take, userId) });

        }

    }
}
