using System;
using System.Linq;
using AutoMerge.Events;
using AutoMerge.Prism.Events;
using Microsoft.TeamFoundation.VersionControl.Client;

namespace AutoMerge
{
	public class MergeInfoViewModel
	{
		private readonly IEventAggregator _eventAggregator;
		internal bool _checked;

		public MergeInfoViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

		public bool Checked
		{
			get
			{
				return _checked;
			}
			set
			{
				_checked = value;
				_eventAggregator.GetEvent<BranchSelectedChangedEvent>().Publish(this);
			}
		}

		public string SourcePath { get; set; }

		public string TargetPath { get; set; }

		public string SourceBranch { get; set; }

		public string TargetBranch { get; set; }

		public ChangesetVersionSpec ChangesetVersionSpec { get; set; }


		public string DisplayBranchName
		{
			get
			{
                var shortName = BranchHelper.GetShortBranchName(TargetBranch);
                var pathName = string.Join("/", TargetBranch.Split('/').Where(x => x.Contains('.')));
                var result = shortName;
                if (shortName != pathName)
                {
                    result += " " + pathName;
                }
                return result;
			}
		}

		public BranchValidationResult ValidationResult { get; set; }

		public string ValidationMessage { get; set; }

		public bool IsSourceBranch
		{
			get
			{
				return string.Equals(SourceBranch, TargetBranch, StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
