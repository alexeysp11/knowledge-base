
sc stop KnoledgeBase.Examples.WorkerServiceLogs
sc delete KnoledgeBase.Examples.WorkerServiceLogs

sc create KnoledgeBase.Examples.WorkerServiceLogs binPath="C:\ExampleServices\knowledge-base\WokerServiceXplatform\WokerServiceXplatform.exe" DisplayName= "KnoledgeBase.Examples.WorkerServiceLogs"
