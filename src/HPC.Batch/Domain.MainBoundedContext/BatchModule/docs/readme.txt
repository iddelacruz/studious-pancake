-> Batch accounts have a default quota that limits the number of cores in a Batch account.
The number of cores corresponds to the number of compute nodes.
You can find the default quotas and instructions on how to increase a quota in Quotas and limits for the Azure Batch service.
If your pool is not achieving its target number of nodes, the core quota might be the reason.

-> Dedicated nodes: Dedicated compute nodes are reserved for your workloads. They are more expensive than low-priority nodes, but they are guaranteed to never be preempted.
-> Low-priority nodes: Low-priority nodes take advantage of surplus capacity in Azure to run your Batch workloads. Low-priority nodes are less expensive per hour than dedicated nodes, and enable workloads requiring significant compute power