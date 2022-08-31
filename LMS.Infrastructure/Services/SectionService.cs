using AutoMapper;
using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.SectionRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.IServices;
using LMS.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtherLearningResourceService _olrService;
        private readonly ISCORMService _scormService;

        public SectionService(ISectionRepository sectionRepository, ISubjectRepository subjectRepository, IMapper mapper, 
            IUnitOfWork unitOfWork, IOtherLearningResourceService olrService, ISCORMService scormService)
        {
            _sectionRepository = sectionRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _olrService = olrService;
            _scormService = scormService;
        }

        public async Task<SectionViewModel> CreateSection(SectionCreateRequestModel requestModel)
        {
            ValidateUtils.CheckStringNotEmpty("section name", requestModel.Name);

            var subject = _subjectRepository.Get(s => s.Id == requestModel.SubjectId, s => s.Sections)
                                            .FirstOrDefault();
            if(subject == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SubjectNotFound, 
                    ErrorMessages.SubjectNotFound);
            }

            //check duplicate name
            if (subject.Sections != null && subject.Sections.Any())
            {
                bool isExistedSection = subject.Sections.Any(s => s.Name.Trim().ToLower().
                                    Equals(requestModel.Name.Trim().ToLower()));
                if (isExistedSection)
                {
                    throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SectionIsExisted,
                        ErrorMessages.SectionIsExisted);
                }
            }

            var section = _mapper.Map<Section>(requestModel);
            await _sectionRepository.AddAsync(section);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<SectionViewModel>(section);
        }

        public async Task<SectionViewModel> UpdateSection(int sectionId, SectionUpdateRequestModel requestModel)
        {
            // Validate request data
            ValidateUtils.CheckStringNotEmpty("section name", requestModel.Name);

            var sectionDB = _sectionRepository.Get(s => s.Id == sectionId && s.IsDeleted != true)
                                          .Include(s => s.Subject)
                                          .ThenInclude(su => su.Sections)
                                          .FirstOrDefault();
            if (sectionDB == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SectionNotFound,
                    ErrorMessages.SectionNotFound);
            }
            IEnumerable<Section> listOfSection = sectionDB.Subject.Sections;
            bool isExistedSection = listOfSection.Where(s => s.Id != sectionId
                                && s.Name.Trim().ToLower().Equals(requestModel.Name.Trim().ToLower()))
                                             .Any();
            if (isExistedSection)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.SectionIsExisted,
                        ErrorMessages.SectionIsExisted);
            }
            sectionDB.Name = requestModel.Name;
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<SectionViewModel>(sectionDB);
        }

        //delete section include all learning resourse inside it
        //if there aren't any learning resource in the started course, delete them
        public async Task Delete(int sectionId)
        {
            var section = _sectionRepository.Get(s => s.Id == sectionId)
                                        .Include(s => s.Subject)
                                        .Include(s => s.OtherLearningResourceList)
                                        .ThenInclude(olr => olr.TopicOtherLearningResources)
                                        .Include(s => s.SCORMList)
                                        .AsSplitQuery()
                                        .FirstOrDefault();
            if (section == null)
            {
                throw new RequestException(HttpStatusCode.NotFound, ErrorCodes.NotFound, ErrorMessages.NotFound);
            }

            //delete learning resource in topic
            if (section.OtherLearningResourceList != null && section.OtherLearningResourceList.Any())
            {
                foreach (var olr in section.OtherLearningResourceList)
                {
                    await _olrService.DeleteOtherLearningResourceInSection(olr.Id);
                }
            }
            if (section.SCORMList != null && section.SCORMList.Any())
            {
                foreach (var scorm in section.SCORMList)
                {
                    await _scormService.DeleteSCORMInSection(scorm.Id);
                }
            }
            if(!section.OtherLearningResourceList.Any() && !section.SCORMList.Any())
            {
                await _sectionRepository.Remove(section.Id);
            }
            else
            {
                section.IsDeleted = true;
            }
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
