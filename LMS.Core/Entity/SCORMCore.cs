using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Core.Entity
{
    [Table("scorm_core")]
    public class SCORMCore
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid LearnerId { get; set; }
        [ForeignKey(nameof(LearnerId))]
        public User User { get; set; }
        [Required]
        [Column("topic_scorm_id")]
        public int TopicSCORMId { get; set; }
        [ForeignKey(nameof(TopicSCORMId))]
        public TopicSCORM TopicSCORM { get; set; }
        [Required]
        public string LearnerName { get; set; }
        public string Location { get; set; }
        public string Mode { get; set; }
        public string Credit { get; set; }
        public string CompletionStatus { get; set; }
        public string Entry { get; set; }
        public string Exit { get; set; }
        public string ScoreScaled { get; set; }
        public string ProgressMeasure { get; set; }
        public string ScaledPassingScore { get; set; }
        public string ScoreRaw { get; set; }
        public string ScoreMin { get; set; }
        public string ScoreMax { get; set; }
        public string TotalTime { get; set; }
        public string SessionTime { get; set; }
        //format 2004: P[yY][mM][dD][T[hH][mM][s[.s]S]], ex: P1Y3M2DT3H
        //format 1.2: HHHH:MM:SS.SS
        public string MaxTimeAllowed { get; set; }
        public string TimeLimitAction { get; set; }
        public string SuccessStatus { get; set; }
        public string CompletionThreshold { get; set; }
        public string SuspendData { get; set; }
        public string LaunchData { get; set; }
        public SCORMLearnerPreference LearnerPreference { get; set; }
        public SCORMNavigation Navigation { get; set; }

        //For SCORM 1.2
        public string LessonStatus12 { get; set; }
        public string Comments12 { get; set; }
        public string CommentsFromLMS12 { get; set; }
    }
}
