# Observability-Demo





## References




## Troubleshooting

Sometimes the networking on windows gets messed up, and the app cannot bind to port 5001. To fix this, restart the **Host Network Service** on windows Services, or with the following command:

```
net stop HNS
net start HNS
```

