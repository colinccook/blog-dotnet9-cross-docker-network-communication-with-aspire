# [Title Placeholder]

[Description placeholder]

## Project Structure

```
/docker-compose               # Files for docker-compose and containers
├── /nginx
│   └── nginx.conf            # Maps hostname to mockserver
└── /mockserver
    └── expectation.json      # Expecation for mockserver to respond
└── docker-compose.yaml       # Run the two containers on special network
/ColinCCook.AppHost.AppHost   # Aspire project to run a single Csharp service
/ColinCCook.AspireRunService  # The CSharp service that wants to hit mockserver
/README.md                    # You are here!
```

## Diagram

[Placeholder diagram]

## Getting Started

### Running the prototype

- Make sure your hosts file is set up to route requests for "barservice.colin", see below for instructions
- Run docker-compose up /docker-compose/docker-compose.yaml
- Run the Aspire hosted ColinCCook.AppHost.AppHost. 

### Observations

## Setting hosts file macOS

1. Open Terminal
2. Edit the hosts file using nano (recommended for beginners):
   ```bash
   sudo nano /etc/hosts
   ```

3. Add this line at the end of the file:
   ```
   127.0.0.1    barservice.colin
   ```

4. Save and exit:
   - Press `Ctrl+X`, then `Y`, then `Enter`

5. Flush DNS cache:
   ```bash
   sudo dscacheutil -flushcache
   sudo killall -HUP mDNSResponder
   ```

6. Verify the entry was added. It should output the line you've just added:
   ```bash
   cat /etc/hosts | grep barservice.colin
   ```

## Setting hosts file Windows

1. Open Command Prompt as Administrator:
   - Press `Win + X` and select "Command Prompt (Admin)" or "Windows PowerShell (Admin)"
   - Or search for "cmd" in Start menu, right-click and select "Run as administrator"

2. Edit the hosts file using notepad:
   ```cmd
   notepad C:\Windows\System32\drivers\etc\hosts
   ```

3. Add this line at the end of the file:
   ```
   127.0.0.1    barservice.colin
   ```

4. Save and exit:
   - Press `Ctrl+S` to save
   - Close Notepad

5. Flush DNS cache:
   ```cmd
   ipconfig /flushdns
   ```

6. Verify the entry was added:
   ```cmd
   findstr "barservice.colin" C:\Windows\System32\drivers\etc\hosts
   ```