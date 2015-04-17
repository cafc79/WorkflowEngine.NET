﻿using OptimaJet.Workflow.Core.Builder;
using OptimaJet.Workflow.Core.Bus;
using OptimaJet.Workflow.Core.Model;
using OptimaJet.Workflow.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WorkflowApp
{
    public class WorkflowInit
    {
        private static volatile WorkflowRuntime _runtime;
        private static readonly object _sync = new object();

        public static WorkflowRuntime Runtime
        {
            get
            {
                if (_runtime == null)
                {
                    lock (_sync)
                    {
                        if (_runtime == null)
                        {
                            //TODO CONNECTION STRING TO DATABASE
                            var connectionString = "Data Source=(local);Initial Catalog=WFTemp;Integrated Security=True;User ID=sa;Password=1";

                            var builder = new WorkflowBuilder<XElement>(
                                new OptimaJet.Workflow.DbPersistence.DbXmlWorkflowGenerator(connectionString),
                                new OptimaJet.Workflow.Core.Parser.XmlWorkflowParser(),
                                new OptimaJet.Workflow.DbPersistence.DbSchemePersistenceProvider(connectionString)
                                ).WithDefaultCache();

                            _runtime = new WorkflowRuntime(new Guid("{8D38DB8F-F3D5-4F26-A989-4FDD40F32D9D}"))
                                .WithBuilder(builder)
                                .WithActionProvider(new ActionProvider())
                                .WithRuleProvider(new RuleProvider())
                                .WithPersistenceProvider(new OptimaJet.Workflow.DbPersistence.DbPersistenceProvider(connectionString))
                                .WithTimerManager(new TimerManager())
                                .WithBus(new NullBus())
                                .SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOn()
                                .Start();
                        }
                    }
                }

                return _runtime;
            }
        }
    }

    public class RuleProvider : IWorkflowRuleProvider
    {
        public bool Check(ProcessInstance processInstance, string identityId, string ruleName, string parameter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetIdentities(ProcessInstance processInstance, string ruleName, string parameter)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.List<string> GetRules()
        {
            //LIST YOUR RULES NAMES HERE
            return new List<string>() {  };
        }
    }

    public class ActionProvider : IWorkflowActionProvider
    {
        public void ExecuteAction(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            
        }

        public bool ExecuteCondition(string name, ProcessInstance processInstance, WorkflowRuntime runtime, string actionParameter)
        {
            return true;
        }

        public List<string> GetActions()
        {
            //LIST YOUR ACTIONS NAMES HERE
            return new List<string>()
            {
   
            };
        }
    }

}