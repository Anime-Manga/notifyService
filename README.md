## 📨Notify Service
### Information general:
- `not` require volume mounted on Docker
```sh
example:
    #--- rabbit ---
    USERNAME_RABBIT: "guest" #guest [default]
    PASSWORD_RABBIT: "guest" #guest [default]
    ADDRESS_RABBIT: "localhost" #localhost [default]

    #--- API ---
    ADDRESS_API: "localhost" #localhost [default]
    PORT_API: "33333" #5000 [default]
    PROTOCOL_API: "http" or "https" #http [default]
    
    #---Webhook---
    WEBHOOK_DISCORD: "url" [require]
    
    #---logger---
    LOG_LEVEL: "Debug|Info|Error" #Info [default]
    WEBHOOK_DISCORD_DEBUG: "url" [not require]
```