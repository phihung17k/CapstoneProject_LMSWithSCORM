namespace LMS.Core.Common
{
    public class SCORMConstants
    {
        public static class CMIVocabularyTokens
        {
            public const string Version = "1.0";

            public static class CompletionStatus
            {
                public const string Completed = "completed";
                public const string InComplete = "incomplete";
                public const string NotAttempted = "not attempted";
                public const string Unknown = "unknown";
            }
            public static class Credit
            {
                public const string IsCredit = "credit";
                public const string NoCredit = "no-credit";
            }
            public static class Entry
            {
                public const string AbInitio = "ab-initio";
                public const string Resume = "resume";
                public const string Empty = "";
            }
            public static class Mode
            {
                public const string Browse = "browse";
                public const string Normal = "normal";
                public const string Review = "review";
            }
            public static class SuccessStatus
            {
                public const string Passed = "passed";
                public const string Failed = "failed";
                public const string Unknown = "unknown";
            }

            //if the <adlcp:timeLimitAction> is not defined, LMS use this value to initialize
            public const string DefaultTimeLimitAction = "continue,no message";

            //If the SCO has not set any session times (cmi.session_time), then the LMS cannot determine the cmi.total_time value
            public const string DefaultTotalTime = "PT0H0M0S";

            public static class LearnerPreference
            {
                public const string DefaultAudioLevel = "1";
                public const string DefaultLanguage = "";
                public const string DefaultDeliverySpeed = "1";
                public const string DefaultAudioCaptioning = "0";
               
            }

            public static class LearnerPreferenceAudioCaptioning
            {
                public const string Off = "-1";
                public const string NoChange = "0";
                public const string On = "1";
            }

            public static class Exit
            {
                public const string TimeOut = "time-out";
                public const string Suspend = "suspend";
                public const string Logout = "logout";
                public const string Normal = "normal";
                public const string Empty = "";
            }

            public static class InteractionType
            {
                public const string TrueFalse = "true-false";
                public const string Choice = "choice";
                public const string FillIn = "fill-in";
                public const string LongFillIn = "long-fill-in";
                public const string Likert = "likert";
                public const string Matching = "matching";
                public const string Performance = "performance";
                public const string Sequencing = "sequencing";
                public const string Numeric = "numeric";
                public const string Other = "other";
            }

            public static class InteractionLearnerResponse
            {
                public static class TrueFalse
                {
                    public const string True = "true";
                    public const string False = "false";
                }
            }

            public static class InteractionResult
            {
                public const string Correct = "correct";
                public const string InCorrect = "incorrect";
                public const string UnAnticipated = "unanticipated";
                public const string Neutral = "neutral";
            }

            //SCORM 1.2
            public static class CoreLessonStatus
            {
                public const string Passed = "passed";
                public const string Failed = "failed";
                public const string Completed = "completed";
                public const string Incomplete = "incomplete";
                public const string Browsed = "browsed";
                public const string NotAttempted = "not attempted";
            }
        }

        public static class NavigationVocabularyTokens
        {
            public static class Request
            {
                public const string Continue = "continue";
                public const string Previous = "previous";
                public const string Choice = "choice";
                public const string Jump = "jump";
                public const string Exit = "exit";
                public const string ExitAll = "exitAll";
                public const string Abandon = "abandon";
                public const string AbandonAll = "abandonAll";
                public const string SuspendAll = "suspendAll";
                public const string None = "_none_";
            }

            public static class ValidState
            {
                public const string True = "true";
                public const string False = "false";
                public const string Unknown = "unknown";
            }
        }

        public const string SCORMVersion12 = "1.2";

        public const string CmiVersion = "cmi._version";

        public const string Count = "_count";
        public const string Children = "_children";

        public const string Comment = "comment";
        public const string Location = "location";
        public const string Timestamp = "timestamp";

        public const string CmiCommentsFromLearner = "cmi.comments_from_learner";
        public const string CmiCommentsFromLearnerChildren = "cmi.comments_from_learner._children";
        public const string CmiCommentsFromLearnerCount = "cmi.comments_from_learner._count";

        public const string CmiCommentsFromLms = "cmi.comments_from_lms";
        public const string CmiCommentsFromLmsChildren = "cmi.comments_from_lms._children";
        public const string CmiCommentsFromLmsCount = "cmi.comments_from_lms._count";

        public const string CmiCompletionStatus = "cmi.completion_status";
        public const string CmiCompletionThreshold = "cmi.completion_threshold";
        public const string CmiCredit = "cmi.credit";
        public const string CmiEntry = "cmi.entry";
        public const string CmiExit = "cmi.exit";


        public const string Id = "id";
        public const string Type = "type";
        public const string ObjectivesCount = "objectives._count";
        public const string Objectives = "objectives";
        public const string CorrectResponsesCount = "correct_responses._count";
        public const string CorrectResponses = "correct_responses";
        public const string Pattern = "pattern";
        public const string Weighting = "weighting";
        public const string LearnerResponse = "learner_response";
        public const string Result = "result";
        public const string Latency = "latency";
        public const string Description = "description";
        public const string CmiInteractions = "cmi.interactions";
        public const string CmiInteractionsChildren = "cmi.interactions._children";
        public const string CmiInteractionsCount = "cmi.interactions._count";

        public const string CmiLaunchData = "cmi.launch_data";

        public const string CmiLearnerId = "cmi.learner_id";
        public const string CmiLearnerName = "cmi.learner_name";
        public const string CmiLearnerPreference = "cmi.learner_preference";
        public const string CmiLearnerPreferenceChildren = "cmi.learner_preference._children";
        public const string CmiLearnerPreferenceAudioLevel = "cmi.learner_preference.audio_level";
        public const string CmiLearnerPreferenceLanguage = "cmi.learner_preference.language";
        public const string CmiLearnerPreferenceDeliverySpeed = "cmi.learner_preference.delivery_speed";
        public const string CmiLearnerPreferenceAudioCaptioning = "cmi.learner_preference.audio_captioning";
        public const string AudioLevel = "audio_level";
        public const string Language = "language";
        public const string DeliverySpeed = "delivery_speed";
        public const string AudioCaptioning = "audio_captioning";

        public const string CmiLocation = "cmi.location";
        public const string CmiMaxTimeAllowed = "cmi.max_time_allowed";
        public const string CmiMode = "cmi.mode";

        public const string CmiObjectives = "cmi.objectives";
        public const string Score = "score";
        public const string ScoreChildren = "score._children";
        public const string ScoreScaled = "score.scaled";
        public const string Scaled = "scaled";
        public const string ScoreRaw = "score.raw";
        public const string Raw = "raw";
        public const string ScoreMin = "score.min";
        public const string Min = "min";
        public const string ScoreMax = "score.max";
        public const string Max = "max";
        public const string SuccessStatus = "success_status";
        public const string CompletionStatus = "completion_status";
        public const string ProgressMeasure = "progress_measure";
        public const string CmiObjectivesChildren = "cmi.objectives._children";
        public const string CmiObjectivesCount = "cmi.objectives._count";

        public const string CmiProgressMeasure = "cmi.progress_measure";
        public const string CmiScaledPassingScore = "cmi.scaled_passing_score";
        public const string CmiScoreChildren = "cmi.score._children";
        public const string CmiScoreScaled = "cmi.score.scaled";
        public const string CmiScoreRaw = "cmi.score.raw";
        public const string CmiScoreMin = "cmi.score.min";
        public const string CmiScoreMax = "cmi.score.max";
        public const string CmiSessionTime = "cmi.session_time";
        public const string CmiSuccessStatus = "cmi.success_status";
        public const string CmiSuspendData = "cmi.suspend_data";
        public const string CmiTimeLimitAction = "cmi.time_limit_action";
        public const string CmiTotalTime = "cmi.total_time";

        public const string ADLNavigation = "adl.nav";
        public const string ADLNavigationRequest = "adl.nav.request";
        public const string ADLNavRequestValidContinue = "adl.nav.request_valid.continue ";
        public const string ADLNavRequestValidPrevious = "adl.nav.request_valid.previous";

        //addition cmi in SCORM 1.2
        public const string CmiCore = "cmi.core";
        public const string CmiCoreChildren = "cmi.core._children";
        public const string CmiCoreStudentId = "cmi.core.student_id";
        public const string CmiCoreStudentName = "cmi.core.student_name";
        public const string CmiCoreLessonLocation = "cmi.core.lesson_location";
        public const string CmiCoreCredit = "cmi.core.credit";
        public const string CmiCoreLessonStatus = "cmi.core.lesson_status";
        public const string CmiCoreEntry = "cmi.core.entry";
        public const string CmiCoreScoreChildren = "cmi.core.score_children";
        public const string CmiCoreScoreRaw = "cmi.core.score.raw";
        public const string CmiCoreScoreMax = "cmi.core.score.max";
        public const string CmiCoreScoreMin = "cmi.core.score.min";
        public const string CmiCoreTotalTime = "cmi.core.total_time";
        public const string CmiCoreLessonMode = "cmi.core.lesson_mode";
        public const string CmiCoreExit = "cmi.core.exit";
        public const string CmiCoreSessionTime = "cmi.core.session_time";

        public const string CmiComments = "cmi.comments";
        //cmi.comments_from_lms
        //cmi.objectives.n.status
        public const string CmiStudentData = "cmi.student_data";
        public const string CmiStudentDataChildren = "cmi.student_data._children";
        public const string CmiStudentDataMasteryScore = "cmi.student_data.mastery_score";
        public const string CmiStudentDataMaxTimeAllowed = "cmi.student_data.max_time_allowed";
        public const string CmiStudentDataTimeLimitAction = "cmi.student_data.time_limit_action";

        public const string CmiStudentPreference = "cmi.student_preference";
        public const string CmiStudentPreferenceChildren = "cmi.student_preference._children";
        public const string CmiStudentPreferenceAudio = "cmi.student_preference.audio";
        public const string CmiStudentPreferenceLanguage = "cmi.student_preference.language";
        public const string CmiStudentPreferenceSpeed = "cmi.student_preference.speed";
        public const string CmiStudentPreferenceText = "cmi.student_preference.text";

        //cmi.interactions.n.student_response
        //"cmi.interactions.n.time";

        public const string StudentId = "student_id";
        public const string StudentName = "student_name";
        public const string LessonLocation = "lesson_location";
        public const string Credit = "credit";
        public const string LessonStatus = "lesson_status";
        public const string Entry = "entry";
        //public const string Score = "score";
        public const string TotalTime = "total_time";
        public const string Exit = "exit";
        public const string SessionTime = "session_time";
        public const string Status = "status";
        public const string MasteryScore = "mastery_score";
        public const string TimeLimitAction = "time_limit_action";
        public const string MaxTimeAllowed = "max_time_allowed";
        public const string Audio = "audio";
        //public const string Language = "language";
        public const string Speed = "speed";
        public const string Text = "text";
        public const string Time = "time";
        public const string StudentResponse = "student_response";
    }
}
