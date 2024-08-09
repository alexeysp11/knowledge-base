# ExecutionContext

Класс System.Threading.ExecutionContext отвечает за передачу контекста выполнения между потоками. Он позволяет сохранять и восстанавливать контекст выполнения, который может содержать информацию о текущем потоке, домене приложения, контексте безопасности и других параметрах. Отличие от System.Threading.SynchronizationContext заключается в том, что System.Threading.ExecutionContext используется для передачи контекста выполнения между потоками, а System.Threading.SynchronizationContext - для управления контекстом синхронизации.
