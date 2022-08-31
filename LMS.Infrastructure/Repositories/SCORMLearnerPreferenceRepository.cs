using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using System.Linq;
using System.Threading.Tasks;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMLearnerPreferenceRepository : BaseRepository<SCORMLearnerPreference, int>, ISCORMLearnerPreferenceRepository
    {
        public SCORMLearnerPreferenceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        //SCORM 1.2
        public void GetStudentPreference(ref LMSModel lms)
        {
            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiStudentPreferenceChildren))
            {
                lms.ReturnValue = string.Join(",", Audio, Language, Speed, Text);
                return;
            }
            var learnerPreference = base.Get(lp => lp.SCORMCoreId == coreId).FirstOrDefault();
            switch (dataItem)
            {
                case CmiStudentPreferenceAudio:
                    lms.ReturnValue = learnerPreference.AudioLevel ?? "";
                    break;
                case CmiStudentPreferenceLanguage:
                    lms.ReturnValue = learnerPreference.Language ?? "";
                    break;
                case CmiStudentPreferenceSpeed:
                    lms.ReturnValue = learnerPreference.DeliverySpeed ?? "";
                    break;
                case CmiStudentPreferenceText:
                    lms.ReturnValue = learnerPreference.AudioCaptioning ?? "";
                    break;
            }
        }

        //SCORM 2004
        public void GetLearnerPreference(ref LMSModel lms)
        {
            string dataItem = lms.DataItem;
            int coreId = lms.SCORMCoreId;
            if (dataItem.Equals(CmiLearnerPreferenceChildren))
            {
                lms.ReturnValue = string.Join(",", AudioLevel, Language, DeliverySpeed, AudioCaptioning);
                return;
            }
            var learnerPreference = base.Get(lp => lp.SCORMCoreId == coreId).FirstOrDefault();
            switch (dataItem)
            {
                case CmiLearnerPreferenceAudioLevel:
                    lms.ReturnValue = learnerPreference.AudioLevel;
                    break;
                case CmiLearnerPreferenceLanguage:
                    lms.ReturnValue = learnerPreference.Language;
                    break;
                case CmiLearnerPreferenceDeliverySpeed:
                    lms.ReturnValue = learnerPreference.DeliverySpeed;
                    break;
                case CmiLearnerPreferenceAudioCaptioning:
                    lms.ReturnValue = learnerPreference.AudioCaptioning;
                    break;
            }
        }

        //SCORM 1.2
        public async Task<LMSModel> SetStudentPreference(LMSModel lms, SCORMCore scormCore)
        {
            //cmi.student_preference.audio
            //cmi.student_preference.language
            //cmi.student_preference.speed
            //cmi.student_preference.text
            var scormLearnerPreference = Get(lp => lp.SCORMCoreId == scormCore.Id).FirstOrDefault();
            //check to set error code
            bool flag = true;
            switch (lms.DataItem)
            {
                case CmiStudentPreferenceAudio:
                    bool isInteger = int.TryParse(lms.DataValue, out int audio);
                    if (isInteger)
                    {
                        if (audio >= -1 && audio <= 100)
                        {
                            scormLearnerPreference.AudioLevel = lms.DataValue;
                            flag = false;
                        }
                    }
                    break;
                case CmiStudentPreferenceLanguage:
                    if (TrackingSCORMUtils.IsCMIString255(lms.DataValue))
                    {
                        scormLearnerPreference.Language = lms.DataValue;
                        flag = false;
                    }
                    break;
                case CmiStudentPreferenceText:
                    //space value is the same to cmi.learner_preference.audio_captioning
                    if (TrackingSCORMUtils.IsCMIVocabulary(CmiLearnerPreferenceAudioCaptioning, lms.DataValue))
                    {
                        scormLearnerPreference.AudioCaptioning = lms.DataValue;
                        flag = false;
                    }
                    break;
                case CmiStudentPreferenceSpeed:
                    isInteger = int.TryParse(lms.DataValue, out int speed);
                    if (isInteger)
                    {
                        if (speed >= -100 && speed <= 100)
                        {
                            scormLearnerPreference.AudioLevel = lms.DataValue;
                            flag = false;
                        }
                    }
                    break;
            }
            if (flag)
            {
                TrackingSCORMUtils.SetIncorrectDataType12(ref lms);
            }
            if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
            {
                await applicationDbContext.SaveChangesAsync();
            }
            return lms;
        }

        //SCORM 2004
        public async Task<LMSModel> SetLearnerPreference(LMSModel lms, SCORMCore scormCore)
        {
            //cmi.learner_preference.audio_level
            //cmi.learner_preference.language
            //cmi.learner_preference.audio_captioning
            //cmi.learner_preference.deliverry_speed
            var scormLearnerPreference = Get(lp => lp.SCORMCoreId == scormCore.Id).FirstOrDefault();
            switch (lms.DataItem)
            {
                case CmiLearnerPreferenceAudioLevel:
                    bool isRealNumber = float.TryParse(lms.DataValue, out float audioLevel);
                    if (isRealNumber)
                    {
                        if (audioLevel >= 0)
                        {
                            scormLearnerPreference.AudioLevel = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementValueOutOfRange(ref lms);
                        }
                    }
                    else
                    {
                        TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                    }
                    break;
                case CmiLearnerPreferenceLanguage:
                    if (TrackingSCORMUtils.IsCMIString250(lms.DataValue))
                    {
                        scormLearnerPreference.Language = lms.DataValue;
                    }
                    else
                    {
                        TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                    }
                    break;
                case CmiLearnerPreferenceAudioCaptioning:
                    if (TrackingSCORMUtils.IsCMIVocabulary(CmiLearnerPreferenceAudioCaptioning, lms.DataValue))
                    {
                        scormLearnerPreference.AudioCaptioning = lms.DataValue;
                    }
                    else
                    {
                        TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                    }
                    break;
                case CmiLearnerPreferenceDeliverySpeed:
                    isRealNumber = float.TryParse(lms.DataValue, out float deliverySpeed);
                    if (isRealNumber)
                    {
                        if (deliverySpeed >= 0)
                        {
                            scormLearnerPreference.AudioLevel = lms.DataValue;
                        }
                        else
                        {
                            TrackingSCORMUtils.SetDataModelElementValueOutOfRange(ref lms);
                        }
                    }
                    else
                    {
                        TrackingSCORMUtils.SetDataModelElementTypeMismatch(ref lms);
                    }
                    break;
            }
            if (lms.ErrorCode.Equals(ScormErrorCodes.E0))
            {
                await applicationDbContext.SaveChangesAsync();
            }
            return lms;
        }
    }
}
