using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekShow.Test.Mocks
{
    public abstract class MockBase
    {
        #region Members

        private readonly Dictionary<string, List<object[]>> _recordedCalls;
        private readonly Dictionary<string, object> _returnsForAnyArgs;
        private readonly Dictionary<string, Action> _methodActions;

        #endregion

        #region Constructor

        public MockBase()
        {
            _recordedCalls = new Dictionary<string, List<object[]>>();
            _returnsForAnyArgs = new Dictionary<string, object>();
            _methodActions = new Dictionary<string, Action>();
        }

        #endregion

        #region Public Methods

        public void ClearRecords()
        {
            _recordedCalls.Clear();
            _returnsForAnyArgs.Clear();
            _methodActions.Clear();
        }

        public int GetNumberOfRecordedCalls(string functionName) => 
            _recordedCalls.ContainsKey(functionName) ? _recordedCalls[functionName].Count : 0;

        public IEnumerable<object[]> GetCallArguments(string functionName) => 
            _recordedCalls.ContainsKey(functionName) ? _recordedCalls[functionName] : Enumerable.Empty<object[]>();

        public object[] GetFirstCallArguments(string functionName) => _recordedCalls.ContainsKey(functionName) ? _recordedCalls[functionName][0] : new object[0];

        public void ReturnsForAnyArgs<T>(string functionName, T returnValue)
        {
            if(_returnsForAnyArgs.ContainsKey(functionName))
            {
                _returnsForAnyArgs[functionName] = returnValue;
            }
            else
            {
                _returnsForAnyArgs.Add(functionName, returnValue);
            }
        }

        public void RegisterMethodAction(string functionName, Action actionToCall)
        {
            if(_methodActions.ContainsKey(functionName))
            {
                _methodActions[functionName] = actionToCall;
            }
            else
            {
                _methodActions.Add(functionName, actionToCall);
            }
        }

        #endregion

        #region Protected Methods

        protected void RecordCall(string functionName, params object[] arguments)
        {
            if(_recordedCalls.ContainsKey(functionName))
            {
                _recordedCalls[functionName].Add(arguments == null ? new object[0] : arguments);
            }
            else
            {
                _recordedCalls.Add(functionName, new List<object[]> { arguments == null ? new object[0] : arguments });
            }
        }

        protected T ReturnValueForAnyArgs<T>(string functionName)
        {
            if(_returnsForAnyArgs.ContainsKey(functionName))
            {
                return (T)_returnsForAnyArgs[functionName];
            }
            else if(typeof(T) == typeof(Task))
            {
                return (T)(object)Task.Delay(0);
            }
            else
            {
                return default(T);
            }
        }

        protected void ProcessMethodAction(string functionName)
        {
            if(_methodActions.ContainsKey(functionName))
            {
                _methodActions[functionName]();
            }
        }

        #endregion
    }
}
