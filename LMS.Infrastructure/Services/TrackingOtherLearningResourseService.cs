using AutoMapper;
using LMS.Core.Application;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    internal class TrackingOtherLearningResourseService : ITrackingOtherLearningResourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtherLearningResourceTrackingRepository _otherLearningResourceTrackingRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITopicOtherLearningResourceRepository _topicOLRRepo;

        public TrackingOtherLearningResourseService(IUnitOfWork unitOfWork, 
            IOtherLearningResourceTrackingRepository otherLearningResourceTrackingRepository, 
            IMapper mapper, ICurrentUserService currentUserService, ITopicOtherLearningResourceRepository topicOLRRepo)
        {
            _unitOfWork = unitOfWork;
            _otherLearningResourceTrackingRepository = otherLearningResourceTrackingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _topicOLRRepo = topicOLRRepo;
        }

        public async Task<OtherLearningResourceTrackingViewModel> Tracking(OtherLearningResourceTrackingRequestModel trackingRequestModel)
        {
            Guid userId = _currentUserService.UserId;
            var trackingRecord = _otherLearningResourceTrackingRepository.Get(ot => ot.LearnerId == userId && ot.TopicOtherLearningResourceId == trackingRequestModel.TopicOtherLearningResourceId).FirstOrDefault();
            if (trackingRecord == null)
            {
                var trackingRequest = _mapper.Map<OtherLearningResourceTracking>(trackingRequestModel);
                trackingRequest.LearnerId = userId;
                await _otherLearningResourceTrackingRepository.AddAsync(trackingRequest);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<OtherLearningResourceTrackingViewModel>(trackingRequest);
            }
            _mapper.Map(trackingRequestModel, trackingRecord);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<OtherLearningResourceTrackingViewModel>(trackingRecord);
        }

        public async Task<OtherLearningResourceUpdateProgressViewModel> UpdateLearningProgress(int OLRTrackingId, LearningProgressUpdateRequestModel updateRequestModel)
        {
            Guid userId = _currentUserService.UserId;
            //users are only allowed to update their learning progrss
            var trackingRecord = _otherLearningResourceTrackingRepository.Get(tr => tr.Id == OLRTrackingId && tr.LearnerId == userId).FirstOrDefault();
            if (trackingRecord == null)
            {
                throw new RequestException(HttpStatusCode.BadRequest, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }
            bool WasCompleted = trackingRecord.IsCompleted;
            trackingRecord.IsCompleted = updateRequestModel.IsCompleted;
            await _unitOfWork.SaveChangeAsync();

            var topic = _topicOLRRepo.Get(to => to.OLRTrackings.Select(olrt => olrt.Id).Contains(OLRTrackingId))
                    .Include(to => to.Topic).ThenInclude(t => t.TopicTrackings.Where(tt => tt.UserId == userId))
                    .Select(to => to.Topic).First();
            var topicTracking = topic.TopicTrackings.First();

            if (updateRequestModel.IsCompleted && !WasCompleted)
            {              
                //update CompletedLearningResourses
                topicTracking.CompletedLearningResourses++;
                await _unitOfWork.SaveChangeAsync();
                //update topicTracking status
                bool isCompleteAllLearningResourse = topicTracking.CompletedLearningResourses == topic.NumberOfLearningResources;
                bool isCompleteAllQuizzes = topicTracking.CompletedQuizzes == topic.NumberOfQuizzes;
                bool isCompleteAllSurvey = topicTracking.CompletedSurveys == topic.NumberOfSurveys;
                if (isCompleteAllLearningResourse && isCompleteAllQuizzes && isCompleteAllSurvey)
                {
                    topicTracking.IsCompleted = true;
                }
                await _unitOfWork.SaveChangeAsync();
            }
            var result = _mapper.Map<OtherLearningResourceUpdateProgressViewModel>(trackingRecord);
            result.TopicTracking = _mapper.Map(topicTracking, result.TopicTracking);
            return result;
        }
    }
}
