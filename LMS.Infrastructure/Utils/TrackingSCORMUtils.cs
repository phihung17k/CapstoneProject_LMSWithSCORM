using LMS.Infrastructure.Exceptions;
using LMS.Core.Models.RequestModels;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static LMS.Core.Common.SCORMConstants;

namespace LMS.Infrastructure.Utils
{
    public class TrackingSCORMUtils
    {
        private static readonly List<string> WriteOnlyCMIDataModels = new()
        {
            CmiExit,
            CmiSessionTime,
            //SCORM 1.2
            CmiCoreExit,
            CmiCoreSessionTime
        };

        public static readonly List<string> ReadOnlyCMIDataModels = new()
        {
            CmiVersion,
            CmiCommentsFromLearnerChildren,
            CmiCommentsFromLearnerCount,
            //CmiCommentsFromLmsChildren,
            //CmiCommentsFromLmsCount,
            //"cmi.comments_from_lms.n.comment",
            //"cmi.comments_from_lms.n.location",
            //"cmi.comments_from_lms.n.timestamp",
            CmiCompletionThreshold,
            CmiCredit,
            CmiEntry,
            CmiInteractionsChildren,
            CmiInteractionsCount,
            //"cmi.interactions.n.objectives._count",
            //"cmi.interactions.n.correct_responses._count",
            CmiLaunchData,
            CmiLearnerId,
            CmiLearnerName,
            CmiLearnerPreferenceChildren,
            CmiMaxTimeAllowed,
            CmiMode,
            CmiObjectivesChildren,
            CmiObjectivesCount,
            //"cmi.objectives.n.score._children",
            CmiScaledPassingScore,
            CmiScoreChildren,
            CmiTimeLimitAction,
            CmiTotalTime,
            ADLNavRequestValidContinue,
            ADLNavRequestValidPrevious,
            //SCORM 1.2
            CmiCoreStudentId,
            CmiCoreStudentName,
            CmiCoreCredit,
            CmiCoreEntry,
            CmiCoreTotalTime,
            CmiCoreLessonMode,
            CmiCommentsFromLms,
            CmiStudentDataMasteryScore,
            CmiStudentDataMaxTimeAllowed,
            CmiStudentDataTimeLimitAction
        };

        //keyword must not set value in SCORM 1.2
        public static readonly List<string> KeywordCMIDataModels = new()
        {
            CmiCoreChildren,
            CmiCoreScoreChildren,
            CmiObjectivesChildren,
            CmiObjectivesCount,
            CmiStudentDataChildren,
            CmiStudentPreferenceChildren,
            CmiInteractionsChildren,
            CmiInteractionsCount
        };

        private static readonly string[,] vocabulary = {
            {CmiCompletionStatus, CMIVocabularyTokens.CompletionStatus.Completed},
            {CmiCompletionStatus, CMIVocabularyTokens.CompletionStatus.InComplete},
            {CmiCompletionStatus, CMIVocabularyTokens.CompletionStatus.NotAttempted},
            {CmiCompletionStatus, CMIVocabularyTokens.CompletionStatus.Unknown},
            {CmiExit, CMIVocabularyTokens.Exit.TimeOut},
            {CmiExit, CMIVocabularyTokens.Exit.Suspend},
            {CmiExit, CMIVocabularyTokens.Exit.Logout},
            {CmiExit, CMIVocabularyTokens.Exit.Normal},
            {CmiExit, CMIVocabularyTokens.Exit.Empty},
            {CmiSuccessStatus, CMIVocabularyTokens.SuccessStatus.Passed},
            {CmiSuccessStatus, CMIVocabularyTokens.SuccessStatus.Failed},
            {CmiSuccessStatus, CMIVocabularyTokens.SuccessStatus.Unknown},
            {CmiLearnerPreferenceAudioCaptioning, CMIVocabularyTokens.LearnerPreferenceAudioCaptioning.Off},
            {CmiLearnerPreferenceAudioCaptioning, CMIVocabularyTokens.LearnerPreferenceAudioCaptioning.NoChange},
            {CmiLearnerPreferenceAudioCaptioning, CMIVocabularyTokens.LearnerPreferenceAudioCaptioning.On},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.TrueFalse},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Choice},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.FillIn},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.LongFillIn},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Likert},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Matching},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Performance},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Sequencing},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Numeric},
            {CmiInteractions, CMIVocabularyTokens.InteractionType.Other},
            {LearnerResponse, CMIVocabularyTokens.InteractionLearnerResponse.TrueFalse.True},
            {LearnerResponse, CMIVocabularyTokens.InteractionLearnerResponse.TrueFalse.False},
            {Result, CMIVocabularyTokens.InteractionResult.Correct},
            {Result, CMIVocabularyTokens.InteractionResult.InCorrect},
            {Result, CMIVocabularyTokens.InteractionResult.UnAnticipated},
            {Result, CMIVocabularyTokens.InteractionResult.Neutral},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.Passed},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.Failed},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.Completed},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.Incomplete},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.Browsed},
            {CmiCoreLessonStatus, CMIVocabularyTokens.CoreLessonStatus.NotAttempted},
            {CmiCoreExit, CMIVocabularyTokens.Exit.TimeOut},
            {CmiCoreExit, CMIVocabularyTokens.Exit.Suspend},
            {CmiCoreExit, CMIVocabularyTokens.Exit.Logout},
            {CmiCoreExit, CMIVocabularyTokens.Exit.Empty}
        };

        private static readonly string[,] navVocabulary = {
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.Continue},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.Previous},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.Exit},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.ExitAll},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.Abandon},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.AbandonAll},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.SuspendAll},
            {ADLNavigationRequest, NavigationVocabularyTokens.Request.None}
        };

        // provides a fast check to see if the DataValue they passed to LMSGetValue is WriteOnly
        // if so we are supposed to return an error
        public static bool IsWriteOnly(string DataItem)
        {
            return WriteOnlyCMIDataModels.Contains(DataItem);
        }

        public static bool IsReadOnly(string DataItem)
        {
            if (DataItem.Contains(CmiCommentsFromLms))
            {
                return true;
            }
            else if (DataItem.Contains(CmiInteractions))
            {
                //list of read-only
                //cmi.interactions.n.objectives._count
                //cmi.interactions.n.correct_responses._count
                if (DataItem.Contains(Count))
                {
                    return true;
                }
                return false;
            }
            else if (DataItem.Contains(CmiObjectives))
            {
                if (DataItem.Contains(Children))
                {
                    return true;
                }
                return false;
            }
            return ReadOnlyCMIDataModels.Contains(DataItem);
        }

        public static bool IsKeyword(string dataItem)
        {
            return KeywordCMIDataModels.Contains(dataItem);
        }

        public static void InitializeReturnValue(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E0;
            lms.ErrorString = ScormErrorStrings.E0;
            lms.ReturnValue = ScormReturnValues.True;
        }

        public static void SetArgumentError(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E201;
            lms.ErrorString = ScormErrorStrings.E201;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetGeneralSetFailure(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E351;
            lms.ErrorString = ScormErrorStrings.E351;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetUndefinedDataModelElement(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E401;
            lms.ErrorString = ScormErrorStrings.E401;
            lms.ReturnValue = ScormReturnValues.False;
        }

        //SCORM 1.2
        public static void SetKeywordDataModelElement12(ref LMSModel lms)
        {
            lms.ErrorCode = Scorm12ErrorCodes.E402;
            lms.ErrorString = Scorm12ErrorStrings.E402;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void CheckNullOrEmpty(ref LMSModel lms)
        {
            if (string.IsNullOrEmpty(lms.ReturnValue))
            {
                lms.ErrorCode = ScormErrorCodes.E403;
                lms.ErrorString = ScormErrorStrings.E403;
                lms.ReturnValue = ScormReturnValues.Empty;
            }
        }

        //SCORM 1.2
        public static void SetElementIsReadOnlyError12(ref LMSModel lms)
        {
            lms.ErrorCode = Scorm12ErrorCodes.E403;
            lms.ErrorString = Scorm12ErrorStrings.E403;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetDataModelElementIsReadOnlyError(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E404;
            lms.ErrorString = ScormErrorStrings.E404;
            lms.ReturnValue = ScormReturnValues.False;
        }

        //SCORM 1.2
        public static void SetElementIsWriteOnlyError12(ref LMSModel lms)
        {
            lms.ErrorCode = Scorm12ErrorCodes.E404;
            lms.ErrorString = Scorm12ErrorStrings.E404;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetDataModelElementTypeMismatch(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E406;
            lms.ErrorString = ScormErrorStrings.E406;
            lms.ReturnValue = ScormReturnValues.False;
        }

        //SCORM 1.2
        public static void SetIncorrectDataType12(ref LMSModel lms)
        {
            lms.ErrorCode = Scorm12ErrorCodes.E405;
            lms.ErrorString = Scorm12ErrorStrings.E405;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetDataModelElementValueOutOfRange(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E407;
            lms.ErrorString = ScormErrorStrings.E407;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static void SetDataModelDependencyNotEstablished(ref LMSModel lms)
        {
            lms.ErrorCode = ScormErrorCodes.E408;
            lms.ErrorString = ScormErrorStrings.E408;
            lms.ReturnValue = ScormReturnValues.False;
        }

        public static bool IsCMIVocabulary(string type, string dataValue)
        {
            for (int i = 0; i < vocabulary.GetLength(0); i++)
            {
                if (vocabulary[i, 0] == type)
                {
                    if (vocabulary[i, 1] == dataValue)
                        return true;
                }
            }
            return false;
        }

        public static bool IsNavVocabulary(string type, string dataValue)
        {
            for (int i = 0; i < navVocabulary.GetLength(0); i++)
            {
                if (navVocabulary[i, 0] == type)
                {
                    if (navVocabulary[i, 1] == dataValue || 
                        Regex.IsMatch(dataValue, "{target=\\w+}" + $"({NavigationVocabularyTokens.Request.Choice}|{NavigationVocabularyTokens.Request.Jump})"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsCMIString250(string dataValue)
        {
            if (dataValue.Length > 250)
            {
                return false;
            }
            return true;
        }
        
        public static bool IsCMIString255(string dataValue)
        {
            if (dataValue.Length > 255)
            {
                return false;
            }
            return true;
        }

        public static bool IsCMIString1000(string dataValue)
        {
            if (dataValue.Length > 1000)
            {
                return false;
            }
            return true;
        }

        public static bool IsCMIString4000(string dataValue)
        {
            if (dataValue.Length > 4000)
            {
                return false;
            }
            return true;
        }

        public static bool IsCMIString4096(string dataValue)
        {
            if (dataValue.Length > 4096)
            {
                return false;
            }
            return true;
        }

        //use for choice, fillin and sequencing

        //ex choice:   set of short_identifier_type
        //      format <short_identifier_type>[,]<short_identifier_type>
        //      Ex: choice1[,]choice2[,]choice3
        //      The LMS is only responsible for managing the SPM of 36 short_identifier_types
        //      Each short_identifier_type shall occur in the set only once

        //ex fillin:   set of localized_string_type
        //      format <localized_string_type>[,]<localized_string_type>
        //      Ex: {lang=en}car[,]mobile
        //      The LMS is only responsible for managing the SPM of 10 short_identifier_types
        //      Each localized_string_type shall occur in the set only once

        //ex sequecing: correspond above
        //          difference: Each short_identifier_type may occur more than once in the array
        public static bool CheckValidInLearnerResponse(string learnerResponse, int maxSupportedValue,
            bool isOnceElementInArray = false)
        {
            if (learnerResponse.Contains("[,]"))
            {
                string[] responses = learnerResponse.Split("[,]");
                if(responses.Length > maxSupportedValue)
                {
                    return false;
                }
                //key: choice
                //value: count the choice
                //value is insignificant because this is check the choice only once in set
                var dictionary = new Dictionary<string, int>();
                for(int i = 0; i < responses.Length; i++)
                {
                    string response = responses[i];
                    if (isOnceElementInArray)
                    {
                        if (!IsCMIString250(response))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //for localized_string_type, remove {lang=type} to get actual string
                        if (maxSupportedValue == 10)
                        {
                            response = RemoveLocalizedString(response);
                        }
                        if (!IsCMIString250(response))
                        {
                            return false;
                        }
                        bool isExisted = dictionary.TryGetValue(response, out int count);
                        if (isExisted)
                        {
                            return false;
                        }
                        else
                        {
                            dictionary.Add(response, count);
                        }
                    }
                }
                //when all of choice is only once
                return true;
            }
            else
            {
                if (maxSupportedValue == 10)
                {
                    learnerResponse = RemoveLocalizedString(learnerResponse);
                }
                if (IsCMIString250(learnerResponse))
                {
                    return true;
                }
            }
            return false;
        }

        //remove {lang=type} in string
        public static string RemoveLocalizedString(string value)
        {
            int startIndex = value.IndexOf("{");
            int endIndex = value.LastIndexOf("}");
            //check "}" at the last char in choice
            //ex: "{lang=en}" => the last char = "" empty
            //then remove {lang=type} in choice string => get actual string
            //ex: "{lang=en}car" => return "car"
            if(startIndex > -1 && endIndex > -1)
            {
                if (endIndex + 1 <= value.Length)
                {
                    value = value.Remove(startIndex, endIndex + 1);
                }
            }
            return value;
        }

        //use for matching
        //format: <short_identifier_type>[.]<short_identifier_type>[,]<short_identifier_type>[. ]<short_identifier_type> 
        //Each record shall include both a ‘source’ and a ‘target’ value separated by the special reserved token “[.]”
        //If the bag contains more than one record, they shall be separated by the special reserved token “[,]”
        //The ‘source’ and ‘target’ values are short_identifier_types
        //There is no restriction on the number of times a short_identifier_type occurs in a given characterstring
        //ex: “2[.]c[,]1[.]a[,]3[.]b”
        //meaning: sources are 2, 1, 3
        //          targets are c, a, b
        // 2 matching c
        // 1 matching a
        // 3 matching b
        //The SCO is responsible for understanding this
        public static bool CheckValidMatchingInLearnerResponse(string learnerResponse)
        {
            if (learnerResponse.Contains("[,]"))
            {
                string[] responses = learnerResponse.Split("[,]");
                if (responses.Length > 36)
                {
                    return false;
                }
                foreach (string response in responses)
                {
                    //a bag = "<short_identifier_type>[.]<short_identifier_type>"
                    //ex: a bag = "2[.]c"
                    string[] bags = response.Split("[.]");
                    foreach(string bag in bags)
                    {
                        if (!IsCMIString250(bag))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                string[] bags = learnerResponse.Split("[.]");
                foreach (string bag in bags)
                {
                    if (!IsCMIString250(bag))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //use for performance
        //format: <step_name>[.]<step_answer>[,]<step_name>[.]<step_answer> 
        //The ‘step name’ – A short_identifier_type
        //The ‘step answer’ – Either a characterstring or a numerical value.
        //  - if it is characterstring, LMS shall support at least 250 (the required SPM) characters
        //  - if it is numberical, its format shall be real(10, 7)
        //Each record in the array is separated by a special reserved token (“[,]”)
        //Each of the array records shall include either a ‘step_name’ or a ‘step answer’, separated by “[.]”
        //The ‘step name’ value is optional. If the array record does not include a ‘step name’,
        //      then the array record must include a ‘step answer’ preceded by “[.]”
        //There is no restriction on the number of times a short_identifier_type occurs in a given characterstring
        //The ‘step answer’ value is optional. If the array record does not include a ‘step answer’,
        //      then the array record must include a ‘step name’ succeeded by “[.]”.
        //There is no restriction on the number of times a ‘step name’ occurs in a given characterstring
        //ex: ”step_1[.]inspect wound[,]step_2[.]clean wound[,]step_3[.]apply bandage”
        //meaning: 
        //      step_1 -> inspect wound
        //      step_2 -> clean wound 
        //      step_3 -> apply bandage
        public static bool CheckValidPerformanceInLearnerResponse(string learnerResponse)
        {
            if (learnerResponse.Contains("[,]"))
            {
                string[] responses = learnerResponse.Split("[,]");
                if (responses.Length > 250)
                {
                    return false;
                }
                foreach (string response in responses)
                {
                    //a record = "<step_name>[.]<step_answer>"
                    //ex: a record = "step_2[.]clean wound"
                    string[] record = response.Split("[.]");
                    //check step_answer is a numerical
                    bool isRealNumber = float.TryParse(record[1], out float stepAsnwer);
                    if (!isRealNumber)
                    {
                        //step answer is characterstring
                        if (!IsCMIString250(record[1]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                string[] record = learnerResponse.Split("[.]");
                //check step_answer is a numerical
                bool isRealNumber = float.TryParse(record[1], out float stepAsnwer);
                if (!isRealNumber)
                {
                    //step answer is characterstring
                    if (!IsCMIString250(record[1]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        //use for fillin in correct response 
        //  set of localized_string_type
        //  format {case_matters=<boolean>}{order_matters=<boolean>}<localized_string_ty pe>[,]<localized_string_type> 
        //  Ex: “{case_matters=true}{lang=en}car”
        //  Each record is separated by a special reserved token (“[,]”)
        //  Each localized_string may occur more than once
        //  {case_matters=<boolean>} delimiter:
        //      - Indicates whether or not the case matters for evaluation of the correct response pattern.
        //          Applied to all of the localized_string_types in the array
        //      - {case_matters=<boolean>} is optional
        //      - Default is false
        //   {order_matters=<boolean>} :
        //      - Indicates whether or not the order matters for evaluation of the correct response pattern
        //          Applied to all of the localized_string_types in the array
        //      - {order_matters=<boolean>}is optional
        //      - Default is true
        //  The LMS is only responsible for managing the SPM of 10 localized_string_type
        public static bool CheckValidFillInCorrectResponse(string correctResponse)
        {
            correctResponse = RemoveLocalizedString(correctResponse);
            if (correctResponse.Contains("[,]"))
            {
                string[] responses = correctResponse.Split("[,]");
                if (responses.Length > 10)
                {
                    return false;
                }
                for (int i = 0; i < responses.Length; i++)
                {
                    string response = responses[i];
                    if (!IsCMIString250(response))
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                if (IsCMIString250(correctResponse))
                {
                    return true;
                }
            }
            return false;
        }

        //use for performance in correct response
        //format: {order_matters}<step_name>[.]<step_answer>[,]<step_name>[.]<step_answer>
        //{order_matters=<boolean>} :
        //      - Indicates whether or not the order matters for evaluation of the correct response pattern
        //          Applied to all of the localized_string_types in the array
        //      - {order_matters=<boolean>}is optional
        //      - Default is true
        //The ‘step name’ – A short_identifier_type
        //The ‘step answer’ – Either a characterstring or a numerical value.
        //  - if it is characterstring, LMS shall support at least 250 (the required SPM) characters
        //  - if it is numberical, its format shall be real(10, 7). Format:
        //         + <min>[:]<max>: Indicates that the correct response is greater than or equal to
        //              the minimum value supplied and less than or equal to the maximum value supplied.
        //              If the <min> and the <max> are identical numbers, then the correct response is exactly that number.
        //         + [:]<max>: Indicates that there is no lower bound for the correct response, only an upper bound.
        //              The value must be less than or equal to the maximum value supplied.
        //         + <min>[:]: Indicates that there is no upper bound for the correct response, only a lower bound.
        //              The value must be greater than or equal to the minimum value supplied.
        //         + [:] – Indicates that there is no upper or lower bound
        //Each record in the array is separated by a special reserved token (“[,]”)
        //Each of the array records shall include either a ‘step_name’ or a ‘step answer’, separated by “[.]”
        //The ‘step name’ value is optional. If the array record does not include a ‘step name’,
        //      then the array record must include a ‘step answer’ preceded by “[.]”
        //There is no restriction on the number of times a short_identifier_type occurs in a given characterstring
        //The ‘step answer’ value is optional. If the array record does not include a ‘step answer’,
        //      then the array record must include a ‘step name’ succeeded by “[.]”.
        //There is no restriction on the number of times a ‘step name’ occurs in a given characterstring
        //ex: “{order_matters=false}[.]drink coffee[,][.]eat cereal”
        public static bool CheckValidPerformanceInCorrectResponse(string correctResponse)
        {
            correctResponse = RemoveLocalizedString(correctResponse);
            if (correctResponse.Contains("[,]"))
            {
                string[] responses = correctResponse.Split("[,]");
                if (responses.Length > 125)
                {
                    return false;
                }
                foreach (string response in responses)
                {
                    //a record = "<step_name>[.]<step_answer>"
                    //ex: a record = "step_2[.]clean wound"
                    string[] record = response.Split("[.]");
                    //check step_answer is a numerical format <min>[:]<max>
                    if (record[1].Contains("[:]"))
                    {
                        string[] stepAnswerValues = record[1].Split("[:]");
                        //check min max format real number
                        foreach(string value in stepAnswerValues)
                        {
                            bool isRealNumer = float.TryParse(value, out float realNumber);
                            if (!isRealNumer)
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //step answer is characterstring
                        if (!IsCMIString250(record[1]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                string[] record = correctResponse.Split("[.]");
                //check step_answer is a numerical
                if (record[1].Contains("[:]"))
                {
                    string[] stepAnswerValues = record[1].Split("[:]");
                    //check min max format real number
                    foreach (string value in stepAnswerValues)
                    {
                        bool isRealNumer = float.TryParse(value, out float realNumber);
                        if (!isRealNumer)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //step answer is characterstring
                    if (!IsCMIString250(record[1]))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
