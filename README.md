## 📨Notify Service
### Information general:
> Note: `not` require volume mounted on Docker

### Dependencies
| Services | Required |
| ------ | ------ |
| Api | ✅  |
| RabbitMQ | ✅  |

```sh
example:
    #--- rabbit ---
    USERNAME_RABBIT: "guest" #guest [default]
    PASSWORD_RABBIT: "guest" #guest [default]
    ADDRESS_RABBIT: "localhost" #localhost [default]
    
    #--- Webhook ---
    WEBHOOK_DISCORD_BOOK: "url" [require]
    WEBHOOK_DISCORD_BOOK_REQUEST: "url" [require]
    WEBHOOK_DISCORD_VIDEO: "url" [require]
    WEBHOOK_DISCORD_VIDEO_REQUEST: "url" [require]

    #--- Bot Telegram ---
    TOKEN_BOT: "token" [require]
    CHAT_ID: "@channelusername" [require]
    CHAT_ID_REQUEST: "@channelusername" [require]
    
    #--- logger ---
    LOG_LEVEL: "Debug|Info|Error" #Info [default]
    WEBHOOK_DISCORD_DEBUG: "url" [not require]

    #--- General ---
    SELECT_SERVICE: "Discord|Telegram|Any"
```