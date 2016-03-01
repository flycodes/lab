﻿using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class StringParameterNode : AParameterNode<string> {

        public enum StringCondition {
            Equal,
            NotEqual
        };

        [SerializeField]
        private StringCondition _condition;

        public StringCondition Condition {
            get { return _condition; }
            set { _condition = value; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            switch (_condition) {
                case StringCondition.Equal:
                    if (Blackboard.StringParameters[Key].Equals((DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
                case StringCondition.NotEqual:
                    if (!Blackboard.StringParameters[Key].Equals((DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value))) {
                        return true;
                    }
                    break;
            }
            return false;
		}
		#if UNITY_EDITOR
		public override bool DebugRun(int level, int nodeIndex) {
			var result = false;
			switch (_condition) {
				case StringCondition.Equal:
					if (Blackboard.StringParameters[Key] == (DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value)) {
						result = true;
					}
					break;
				case StringCondition.NotEqual:
					if (Blackboard.StringParameters[Key] != (DynamicValue ? Blackboard.StringParameters[DynamicValueKey] : Value)) {
						result = true;
					}
					break;
			}
			Debug.Log(string.Format("{0}{1}. String Parameter Node. Result: <b><color={2}>{3}</color></b>", new string('\t', level), nodeIndex, result ? "green" : "red", result));
			OnDebugResult(this, result);
			return result;
		}
		#endif
    }
}
